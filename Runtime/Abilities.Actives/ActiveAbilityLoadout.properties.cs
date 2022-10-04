using Cirrus.Collections;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Objects;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cirrus.Arpg.Abilities
{
	public interface IAbilityLoadout
	{
		int SlotsCount { get; set; }

		int SlotsRowSize { get; set; }
	}

	[Serializable]
	public partial class ActiveAbilityLoadout 
	: ListBase<Object>
	, IAbilityLoadout
	{		
		[field: SerializeField]
		public int SlotsCount { get; set; } = 20;

		[field: SerializeField]
		public int SlotsRowSize { get; set; } = 10;
	}

	public partial class ActiveAbilityLoadoutInstance 
		: ListBase<IActiveAbilityInstance>
		,  IAbilityLoadout
	{
		protected ActiveAbilityLoadout _resource;

		private int _selectedSlotIndex = 0;
		
		public int SlotsCount { get => ((IAbilityLoadout)_resource).SlotsCount; set => ((IAbilityLoadout)_resource).SlotsCount = value; }
		public int SlotsRowSize { get => ((IAbilityLoadout)_resource).SlotsRowSize; set => ((IAbilityLoadout)_resource).SlotsRowSize = value; }
	}
}
