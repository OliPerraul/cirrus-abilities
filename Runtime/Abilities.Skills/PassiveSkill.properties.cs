//using Cirrus.Arpg.Actions.Items;
//using Cirrus.Events;

using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public partial class PassiveSkill 
	: SkillBase
	, IPassiveAbility
	, IPassiveAbilityImpl
	{
		[SerializeField]
		private PassiveAbilityImpl _passiveAbility { get; set; }
		//public Range_ Range { get => ((IPassiveAbilityImpl)_passiveAbility).Range; set => ((IPassiveAbilityImpl)_passiveAbility).Range = value; }
		public PassiveAbilityFlags PassiveAbilityFlags { get => ((IPassiveAbilityImpl)_passiveAbility).PassiveAbilityFlags; set => ((IPassiveAbilityImpl)_passiveAbility).PassiveAbilityFlags = value; }
		public List<PassiveAbilityInteraction> Interacts { get => ((IPassiveAbilityImpl)_passiveAbility).Interacts; set => ((IPassiveAbilityImpl)_passiveAbility).Interacts = value; }
		public float Strength { get => ((IPassiveAbilityImpl)_passiveAbility).Strength; set => ((IPassiveAbilityImpl)_passiveAbility).Strength = value; }
		public Range_ Frequency { get => ((IPassiveAbilityImpl)_passiveAbility).Frequency; set => ((IPassiveAbilityImpl)_passiveAbility).Frequency = value; }
		public DurationBase Duration { get => ((IPassiveAbilityImpl)_passiveAbility).Duration; set => ((IPassiveAbilityImpl)_passiveAbility).Duration = value; }
		public List<EffectBase> Effects { get => ((IPassiveAbilityImpl)_passiveAbility).Effects; set => ((IPassiveAbilityImpl)_passiveAbility).Effects = value; }

#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Passive Skill", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<PassiveSkill>();
#endif
	}

	public partial class PassiveSkillInstance 
	: SkillInstanceBase
	, IAbilityInstance
	, IPassiveAbilityImpl
	, IPassiveAbilityInstance
	, IPassiveAbilityImplInstance
	{
		//private PassiveAbilityImplInstance _passiveAbility;

		private PassiveSkill _resource;

		private PassiveAbilityImplInstance _passiveAbility;

		public DurationInstanceBase Duration { get => ((IPassiveAbilityImplInstance)_passiveAbility).Duration; set => ((IPassiveAbilityImplInstance)_passiveAbility).Duration = value; }
		public List<IEffectInstance> EffectInstances { get => ((IPassiveAbilityImplInstance)_passiveAbility).EffectInstances; set => ((IPassiveAbilityImplInstance)_passiveAbility).EffectInstances = value; }
		public Action<float, float> OnRemainingDurationHandler { get => ((IPassiveAbilityImplInstance)_passiveAbility).OnRemainingDurationHandler; set => ((IPassiveAbilityImplInstance)_passiveAbility).OnRemainingDurationHandler = value; }
		public Action OnImplDurationEndedHandler { get => ((IPassiveAbilityImplInstance)_passiveAbility).OnImplDurationEndedHandler; set => ((IPassiveAbilityImplInstance)_passiveAbility).OnImplDurationEndedHandler = value; }
		//public Range_ Range { get => ((IPassiveAbilityImpl)_passiveAbility).Range; set => ((IPassiveAbilityImpl)_passiveAbility).Range = value; }
		public PassiveAbilityFlags PassiveAbilityFlags { get => ((IPassiveAbilityImpl)_passiveAbility).PassiveAbilityFlags; set => ((IPassiveAbilityImpl)_passiveAbility).PassiveAbilityFlags = value; }
		public List<PassiveAbilityInteraction> Interacts { get => ((IPassiveAbilityImpl)_passiveAbility).Interacts; set => ((IPassiveAbilityImpl)_passiveAbility).Interacts = value; }
		public float Strength { get => ((IPassiveAbilityImpl)_passiveAbility).Strength; set => ((IPassiveAbilityImpl)_passiveAbility).Strength = value; }
		public Range_ Frequency { get => ((IPassiveAbilityImpl)_passiveAbility).Frequency; set => ((IPassiveAbilityImpl)_passiveAbility).Frequency = value; }
		public List<EffectBase> Effects { get => ((IPassiveAbilityImpl)_passiveAbility).Effects; set => ((IPassiveAbilityImpl)_passiveAbility).Effects = value; }

		//protected override AbilityBase _AbilityResource => _resource;

		public override IAbility AbilityResource => _resource;

		public Action<IPassiveAbilityInstance> OnDurationEndedHandler { get; set; }

		DurationBase IPassiveAbilityImpl.Duration { get => ((IPassiveAbilityImpl)_passiveAbility).Duration; set => ((IPassiveAbilityImpl)_passiveAbility).Duration = value; }

		public bool DoApply(EntityObjectBase target)
		{
			return ((IPassiveAbilityImplInstance)_passiveAbility).DoApply(target);
		}

		public bool DoConsolidate(EntityObjectBase target, IPassiveAbilityInstance update)
		{
			return ((IPassiveAbilityImplInstance)_passiveAbility).DoConsolidate(target, update);
		}

		public bool DoUnapply(EntityObjectBase target)
		{
			return ((IPassiveAbilityImplInstance)_passiveAbility).DoUnapply(target);
		}

		public PassiveAbilityUpdateFlags DoUpdateDuration(EntityObjectBase entity, PassiveAbilityDelta update)
		{
			return ((IPassiveAbilityImplInstance)_passiveAbility).DoUpdateDuration(entity, update);
		}

		public bool GetInteracts(IPassiveAbility target, out List<PassiveAbilityInteraction> interactions)
		{
			return ((IPassiveAbilityImplInstance)_passiveAbility).GetInteracts(target, out interactions);
		}


		// public override MenuObjectUIBase StoreMenuReference(SlotUI slot)
		// {
		// 	//return _resource.PassiveSkillObject.StoreReference(slot, this);
		// 	return null;

		// }


	}
}
