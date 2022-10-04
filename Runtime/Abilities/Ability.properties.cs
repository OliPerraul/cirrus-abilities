//using Cirrus.DH.Actions.Abilities;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cirrus.Arpg.Abilities
{
	public interface IAbility : IIDed
	{
		string Category { get; }
		Color Color { get; }
		string Description { get; }
		Sprite Icon { get; }
		HashSet<Object> References { get; }
	}

	public partial class AbilityBase
	: MonoBehaviourBase
	, IAbility
	, IIDed
	{
		[field: SerializeField]
		public ID Id { get; set; }

		[field: SerializeField]
		public string Name { get; set; }

		[field: SerializeField]
		public string Description { get; set; }

		[field: SerializeField]
		public Color Color { get; set; } = Color.white;

		[field: SerializeField]
		public Sprite Icon { get; set; } = null;

		[field: SerializeField]
		public string Category { get; set; } = "";

		[SerializeField]
		private List<Object> _references;
		public HashSet<Object> _references_;
		public HashSet<Object> References => _references_ == null ?
			_references_ = new HashSet<Object>(_references) :
			_references_;

		// [field: SerializeField]
		// public int AbilityFlags { get; set; } = 0;
	}

	public interface IAbilityInstance
	: IAbility
	{
		IAbility AbilityResource { get; }

		Action<IAbility> OnSelectedHandler { get; set; }
	}

	public abstract partial class AbilityInstanceBase 
	: CopiableBase
	, IAbilityInstance
	{
		public abstract IAbility AbilityResource { get; }

		public ID Id { get => AbilityResource.Id; set => Id = value; }

		public string Category => AbilityResource.Category;
		public Color Color => AbilityResource.Color;
		//public ConditionBase[] Conditions => _ConcreteAbility.Conditions;
		public string Description => AbilityResource.Description;
		public Sprite Icon => AbilityResource.Icon;

		public HashSet<Object> References => AbilityResource.References;

		public Action<IAbility> OnSelectedHandler { get; set; }
	}
}
