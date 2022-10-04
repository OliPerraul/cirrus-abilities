using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Unity.Objects;

//using Cirrus.Objects; using Cirrus.Content;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
//using MarkupAttributes;

namespace Cirrus.Arpg.Abilities
{


	public partial class AbilityActionImpl
	: MonoBehaviourBase
	, INameable
	, IAbilityActionData
	{
		[SerializeField]
		private AbilityActionData _data;

		public string Name
		{
			get => _data.Name;
			set => _data.Name = value;
		}

		public float Strength { get => ((IAbilityActionData)_data).Strength; set => ((IAbilityActionData)_data).Strength = value; }
		public EffectBase[] Effects { get => ((IAbilityActionData)_data).Effects; set => ((IAbilityActionData)_data).Effects = value; }
		public EffectBase[] Asyncs { get => ((IAbilityActionData)_data).Asyncs; set => ((IAbilityActionData)_data).Asyncs = value; }
		public ConditionBase[] Conditions { get => ((IAbilityActionData)_data).Conditions; set => ((IAbilityActionData)_data).Conditions = value; }
		public Range_ Range { get => ((IAbilityActionData)_data).Range; set => ((IAbilityActionData)_data).Range = value; }
		public EffectorBase[] Effectors { get => ((IAbilityActionData)_data).Effectors; set => ((IAbilityActionData)_data).Effectors = value; }
	}
}