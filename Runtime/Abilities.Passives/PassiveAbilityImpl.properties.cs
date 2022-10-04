using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;

using System;
using System.Collections.Generic;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Cirrus.Arpg.Abilities
{
	[Flags]
	public enum PassiveAbilityUpdateFlags
	{
		None = 0,
		Duration = 1 << 0,
		Strength = 1 << 1,
		Removal = 1 << 2,
		Ended = 1 << 3,


		Particles = 1 << 10
	}

	// NOTE: BHeap sorted by type
	public class PassiveAbilityUpdateMap
		: Dictionary<IPassiveAbilityInstance, BHeap<PassiveAbilityDelta>>
	{
	}

	[Serializable]
	public partial struct PassiveAbilityDelta
	{
		[SerializeField]
		private string _name;

		[SerializeField]
		public Operation operation;

		[SerializeField]
		public PassiveAbilityUpdateFlags flags;

		[SerializeField]
		public List<Object> _references;
		private HashSet<Object> _references_;

		public HashSet<Object> References => _references_ == null ?
			_references_ = new HashSet<Object>(_references) :
			_references_;

		[SerializeField]
		public List<EffectBase> effects;

		//public void OnBeforeSerialize()
		//{
		//	if(_name == "" || _name == null) _name = operation._expression;
		//}

		//public void OnAfterDeserialize()
		//{
		//}

		// Apply update to all, otherwise stop when first is found
		//public bool All { get; set; }

		// Same operation across all modifications
		// Otherwise rely on tags		

		// TODO change most ienumerable for arrays


		//public ModUpdate()
		//{
		//	UpdateFlags = ModifierUpdateFlags.None;

		//	ModRefs = HashSetUtils.Empty<object>();

		//	EffectRefs = HashSetUtils.Empty<object>();

		//	Op = new Operation();
		//}


		//object ICloneable.Clone()
		//{
		//	var inst = MemberwiseClone();
		//	return inst;
		//}
	}

	// TODO : different flags Interaction, Update 
	// e.g. Remove cannot coexist with other flags..
	// e.g. Consolidate either?
	public enum PassiveAbilityInteractionFlags
	{
		None = 0,
		Consolidate = 1 << 1,
		IncreaseStrength = 1 << 2,
		IncreaseDuration = 1 << 3,
		DecreaseStrength = 1 << 4,
		DecreaseDuration = 1 << 5,
		Remove = 1 << 6
	}

	[Flags]
	public enum PassiveAbilityFlags
	{
		None = 0,
		Unique = 1 << 0,
		Hidden = 1 << 1,
		StrengthScaled = 1 << 2,
	}

	public enum PassiveAbilityCancelationMode
	{
		None,
		Weak, // Prevents attach
		Strong // Prevents interactions and attach
	}

	public interface IPassiveAbilityImpl
	: IPassiveAbilityCommon
	{
		List<PassiveAbilityInteraction> Interacts { get; set; }

		float Strength { get; set; }

		Range_ Frequency { get; set; }

		DurationBase Duration { get; set; }

		List<EffectBase> Effects { get; set; }
	}

	public partial class PassiveAbilityImpl 
		: AbilityImplBase
		, IPassiveAbilityImpl
	{
		[field: SerializeField]
		public Range_ Range { get; set; } = -1;
		

		[field: SerializeField]
		public PassiveAbilityFlags PassiveAbilityFlags { get; set; } = PassiveAbilityFlags.Hidden | PassiveAbilityFlags.StrengthScaled;

		/// <summary>
		/// unused
		/// </summary>
		//public bool IsUnique = true;

		// IEffectSource
		public Vector3 Direction => Vector3.zero;


		[field: SerializeField]
		public List<PassiveAbilityInteraction> Interacts { get; set; }

		/// <summary>
		/// We use this to prevent maximum strength effect applied when modifier is full;
		/// </summary>
		[field: SerializeField]
		public float Strength { get; set; } = 0.5f;

		[field: SerializeField]
		public Range_ Frequency { get; set; } = -1;


		//public bool IsStrengthScaled = true;

		// Check if before duration is null, simply use duration
		//public DurationResource BeforeDuration { get; set; } = null;

		// NOTE: Perhaps we only need one duration ...
		// e.g. Time duration can also be decreased manually so two durations is redundant..
		// For more sophisticated examples well see when we get there.
		[field: SerializeField]
		[field: SerializeEmbedded]
		public DurationBase Duration { get; set; }


		[field: SerializeField]
		public List<EffectBase> Effects { get; set; }
	}

	// TODO : Refactor similar ModifierUpdate
	// It also contains target tags...
	[Serializable]
	public partial class PassiveAbilityInteraction
	{
		[field: SerializeField]
		private List<Object> _targets;
		private HashSet<Object> _targetsHashset;
		public HashSet<Object> Targets => _targetsHashset == null ?
			_targetsHashset = new HashSet<Object>(_targets) :
			_targetsHashset;

		[field: SerializeField]
		public PassiveAbilityCancelationMode Cancelation { get; set; } = PassiveAbilityCancelationMode.None;

		[field: SerializeField]
		public bool Consolidate { get; set; }

		[field: SerializeField]
		public PassiveAbilityDelta[] Updates { get; set; }
		//public enum 
	}

	public interface IPassiveAbilityImplInstance
	{
		public DurationInstanceBase Duration { get; set; }

		public List<IEffectInstance> EffectInstances { get; set; }

		Action<float, float> OnRemainingDurationHandler { get; set; }

		Action OnImplDurationEndedHandler { get; set; }

		bool GetInteracts(IPassiveAbility target, out List<PassiveAbilityInteraction> interactions);

		bool DoApply(EntityObjectBase target);

		bool DoUnapply(EntityObjectBase target);

		PassiveAbilityUpdateFlags DoUpdateDuration(EntityObjectBase entity, PassiveAbilityDelta update);

		// TODO : Can we increase modifications without increasing the duration??
		bool DoConsolidate(EntityObjectBase target, IPassiveAbilityInstance update);
	}

	public partial class PassiveAbilityImplInstance
	: CopiableBase	
	, IPassiveAbilityImpl
	, IPassiveAbilityImplInstance
	, IEffectContext
	, ICopiable
	{
		protected PassiveAbilityImpl _resource;

		public IAbilityContext AbilityContext { get; set; }
		

		//protected EntityComponentBase _target;

		public Action<float, float> OnRemainingDurationHandler { get; set; }

		public Action OnImplDurationEndedHandler { get; set; }

		
		public DurationInstanceBase Duration { get; set; }


		private Timer _frequencyTimer;

		public List<IEffectInstance> EffectInstances { get; set; }

		/// <summary>
		/// unused
		/// </summary>
		//public bool IsUnique = true;

		// IEffectSource
		public Vector3 Direction => Vector3.zero;

		public Range_ Range => Range_.Infinite;

		public EntityObjectBase Source { get; set; }

		public EntityObjectBase Target { get; set; }

		public List<EntityObjectBase> Targets { get; set; }


		//public EntityComponentBase Entity => Entity;		

		//public CharacterInstance CharacterInst => Entity.CharacterInst;

		//public int StackCapacity { get; set; } = 1;

		// High priority wins
		//public int Priority { get; set; } = 1;

		//public bool IsHidden => Icon == null;

		//public Color Color { get; set; } = Color.white;

		//public Sprite Icon { get; set; } = null;

		//public string Name { get; set; } = "";

		//public string Category { get; set; } = "";

		//public string Description { get; set; } = "";


		/////////////////////////////////				
		public PassiveAbilityFlags PassiveAbilityFlags { get => ((IPassiveAbilityImpl)_resource).PassiveAbilityFlags; set => ((IPassiveAbilityImpl)_resource).PassiveAbilityFlags = value; }
		public List<PassiveAbilityInteraction> Interacts { get => ((IPassiveAbilityImpl)_resource).Interacts; set => ((IPassiveAbilityImpl)_resource).Interacts = value; }
		public float Strength { get => ((IPassiveAbilityImpl)_resource).Strength; set => ((IPassiveAbilityImpl)_resource).Strength = value; }
		public Range_ Frequency { get => ((IPassiveAbilityImpl)_resource).Frequency; set => ((IPassiveAbilityImpl)_resource).Frequency = value; }
		//DurationBase Duration { get => ((IPassiveAbility)_resource).Duration; set => ((IPassiveAbility)_resource).Duration = value; }
		public List<EffectBase> Effects { get => ((IPassiveAbilityImpl)_resource).Effects; set => ((IPassiveAbilityImpl)_resource).Effects = value; }
		DurationBase IPassiveAbilityImpl.Duration { get => ((IPassiveAbilityImpl)_resource).Duration; set => ((IPassiveAbilityImpl)_resource).Duration = value; }
		Action IPassiveAbilityImplInstance.OnImplDurationEndedHandler { get; set; }
		//public List<Object> References { get => ((IPassiveAbility)_resource).References; set => ((IPassiveAbility)_resource).References = value; }

		//public List<ModificationBase> _modifications = new List<ModificationBase>();

	}
}
