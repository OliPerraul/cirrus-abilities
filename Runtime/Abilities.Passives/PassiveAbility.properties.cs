//using UnityEngine.InputSystem;
using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Numerics;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using System.Collections.Generic;
using System.Linq;
using Cirrus.Collections;
using System;
using UnityEngine;
using UnityEditor;
using Cirrus.Unity.Editor;
// TODO
// begin duration
// e.g wait until you die until buff is applied
// e.g curse
// e.g hitby fire to get boost in damage

// end duration

//using Cirrus.Arpg.Actions.Modifiers.Cirrus.Arpg.World.Entities.Actions.Modifiers;

// TODO : For stack comparison (determien which is the highest)

// STRENGTH VALUE OF THE MODIFICATION


// TODO self inflicted action during events

namespace Cirrus.Arpg.Abilities
{
	public interface IPassiveAbilityCommon
	{
		PassiveAbilityFlags PassiveAbilityFlags { get; set; }
	}

	public interface IPassiveAbility
	: IAbility
	, IPassiveAbilityCommon
	{
		IPassiveAbilityInstance CreateInstance();
	}

	public abstract partial class PassiveAbilityBase
	: AbilityBase
	, IPassiveAbility
	, IPassiveAbilityImpl
	{
		public Range_ Range { get => _passiveAbilityImpl.Range; set => _passiveAbilityImpl.Range = value; }
		public PassiveAbilityFlags PassiveAbilityFlags { get => _passiveAbilityImpl.PassiveAbilityFlags; set => _passiveAbilityImpl.PassiveAbilityFlags = value; }
		public List<PassiveAbilityInteraction> Interacts { get => _passiveAbilityImpl.Interacts; set => _passiveAbilityImpl.Interacts = value; }
		public float Strength { get => _passiveAbilityImpl.Strength; set => _passiveAbilityImpl.Strength = value; }
		public Range_ Frequency { get => _passiveAbilityImpl.Frequency; set => _passiveAbilityImpl.Frequency = value; }
		public DurationBase Duration { get => _passiveAbilityImpl.Duration; set => _passiveAbilityImpl.Duration = value; }
		public List<EffectBase> Effects { get => _passiveAbilityImpl.Effects; set => _passiveAbilityImpl.Effects = value; }
		//protected IPassiveAbilityImpl _Impl { get; }

		[SerializeField]
		private PassiveAbilityImpl _passiveAbilityImpl;
		public PassiveAbilityImpl PassiveAbilityImpl => _passiveAbilityImpl;
		
		//protected abstract IPassiveAbilityInstance _CreateInstance();
	}

	public partial class PassiveAbility 
	: PassiveAbilityBase	
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Passive Ability", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<PassiveAbility>();
#endif
	}


	public partial class PassiveAbilityInstanceBase
		: AbilityInstanceBase
		, IPassiveAbilityImpl
		, IPassiveAbilityImplInstance
		, IPassiveAbilityInstance		
	{
		protected abstract IPassiveAbilityImpl _PassiveAbilityImpl { get; }

		protected abstract IPassiveAbilityImplInstance _PassiveAbilityImplInstance { get; }


		public Action<IPassiveAbilityInstance> OnDurationEndedHandler { get; set; }

		Action IPassiveAbilityImplInstance.OnImplDurationEndedHandler { get; set; }

		public DurationInstanceBase Duration { get => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).Duration; set => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).Duration = value; }
		
		public List<IEffectInstance> EffectInstances { get => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).EffectInstances; set => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).EffectInstances = value; }
		
		public Action<float, float> OnRemainingDurationHandler { get => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).OnRemainingDurationHandler; set => ((IPassiveAbilityImplInstance)_PassiveAbilityImplInstance).OnRemainingDurationHandler = value; }
		//public Range_ Range { get => _PassiveAbilityImpl.Range; set => _PassiveAbilityImpl.Range = value; }
		public PassiveAbilityFlags PassiveAbilityFlags { get => _PassiveAbilityImpl.PassiveAbilityFlags; set => _PassiveAbilityImpl.PassiveAbilityFlags = value; }
		public List<PassiveAbilityInteraction> Interacts { get => _PassiveAbilityImpl.Interacts; set => _PassiveAbilityImpl.Interacts = value; }
		public float Strength { get => _PassiveAbilityImpl.Strength; set => _PassiveAbilityImpl.Strength = value; }
		public Range_ Frequency { get => _PassiveAbilityImpl.Frequency; set => _PassiveAbilityImpl.Frequency = value; }
		DurationBase IPassiveAbilityImpl.Duration { get => _PassiveAbilityImpl.Duration; set => _PassiveAbilityImpl.Duration = value; }
		public List<EffectBase> Effects { get => _PassiveAbilityImpl.Effects; set => _PassiveAbilityImpl.Effects = value; }

		public PassiveAbilityInstanceBase(PassiveAbilityBase resource) : base(resource)
		{
		}

		public PassiveAbilityInstanceBase(IAbilityInstance resource) : base(resource)
		{
		}
	}

	public interface IPassiveAbilityInstance
	: IPassiveAbilityImplInstance
	, IPassiveAbilityImpl
	, IAbilityInstance
	{
		Action<IPassiveAbilityInstance> OnDurationEndedHandler { get; set; }
	}

	public partial class PassiveAbilityInstance 
		: PassiveAbilityInstanceBase
		, IPassiveAbilityImplInstance
		, IPassiveAbilityInstance
	{
		//private IAbilityInstance _abilityInstance;

		private PassiveAbility _resource;

		private IAbilityInstance _abilityInst;

		public override IAbility AbilityResource => _resource == null ? _abilityInst : _resource;


		protected PassiveAbilityImplInstance _passiveAbilityImpl;

		public PassiveAbilityImplInstance PassiveAbilityImpl => _passiveAbilityImpl;

		protected override IPassiveAbilityImpl _PassiveAbilityImpl => _passiveAbilityImpl;

		protected override IPassiveAbilityImplInstance _PassiveAbilityImplInstance => _passiveAbilityImpl;

		public PassiveAbilityInstance(IAbilityInstance abilityInst, PassiveAbilityImpl ab) : base(abilityInst)
		{
			_abilityInst = abilityInst;
			_passiveAbilityImpl = new PassiveAbilityImplInstance(ab);
			_passiveAbilityImpl.OnImplDurationEndedHandler += _OnImplDurationEndedHandler;
		}

		public PassiveAbilityInstance(PassiveAbility resource) : base(resource)
		{
			_resource = resource;
			_passiveAbilityImpl = new PassiveAbilityImplInstance(resource.PassiveAbilityImpl);
			_passiveAbilityImpl.OnImplDurationEndedHandler += _OnImplDurationEndedHandler;
		}
	}
}
