//using Cirrus.Arpg.Actions.Items;
//using Cirrus.Events;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Arpg.UI.Legacy;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public interface ISkill
	{
		ConditionBase[] Conditions { get; }
	}

	public abstract partial class SkillBase 
	: AbilityBase
	, IAbility
	, ISkill
	{
		[field: SerializeField]
		public ConditionBase[] Conditions { get; set; }

		public bool IsConditional => Conditions != null && Conditions.Length != 0;

		//[field: SerializeField]
		//public bool IsAvailable { get; set; } = true;

		public IAbility AbilityAsset
		{
			set
			{
				Color = value.Color;
				Icon = value.Icon;
				//ContentID = value.ContentID;
			}
		}
	}

	public abstract partial class SkillInstanceBase
	: AbilityInstanceBase
	, IAbilityInstance
	, ISkill
	{
		public bool IsHotbarAbility { get; set; }

		public virtual MenuObjectBase MenuObjectPrefab { get; }

		public bool IsLocked { get; set; }

		public ConditionBase UnlockCondition { get; set; }

		//public bool IsAvailable { get; set; }

		public ConditionBase[] Conditions => throw new NotImplementedException();


		// When switching party members, we set all Item Ability
		// Skills on the other hand dont have to be switched
		protected CharacterObject _source;

		//protected List<ConditionListener> _conditionListeners;

		private SkillBase _resource;

		public SkillInstanceBase(SkillBase resource) : base(resource)
		{
			_resource = resource;
		}
	}
}