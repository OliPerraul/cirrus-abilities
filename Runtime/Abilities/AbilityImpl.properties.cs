//using Cirrus.DH.Actions.Abilities;
using Cirrus.Collections;
using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cirrus.Arpg.Abilities
{
	public enum AbilityType
	{
		Skill_Passive,
		Skill_Active_Basic,
		Skill_Active_Special,
		Collectible_Consumable,
		Collectible_Equipment_Armor,
		Collectible_Equipment_Weapon,
	}

	public interface IAbilityImplInstance
	{
		//bool VerifyAvailability(CharacterComponent source);

		//Action<bool> OnAvailableHandler { get; set; }

		//void ForfeitAbility(CharacterComponent source);
	}

	public partial class AbilityImplBase
	: MonoBehaviourBase
	{
	}

	public partial class AbilityImplInstanceBase
	: CopiableBase
	{
		// When switching party members, we set all Item Ability
		// Skills on the other hand dont have to be switched
		protected CharacterObject _source;
		public CharacterObject Source => _source;

		//protected List<ConditionListener> _conditionListeners;

		protected abstract AbilityImplBase _AbilityResourceImpl { get; }


		public AbilityImplInstanceBase(AbilityImplBase asset)
		{
		}
	}

}
