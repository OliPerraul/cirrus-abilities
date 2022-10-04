using System;

using UnityEngine;

namespace Cirrus.Arpg.Conditions
{
	[Serializable]
	public class ConditionalDurationAsset : DurationBase
	{
		[field: SerializeField]
		public ConditionBase Condition { get; set; }

		public override DurationInstanceBase CreateInstance()
		{
			return new ConditionalDuration(this);
		}
	}

	public class ConditionalDuration : DurationInstanceBase
	{
		public ConditionBase Condition;

		private ConditionalDurationAsset _asset;
		protected override DurationBase _Resource => _asset;

		public ConditionalDuration(ConditionalDurationAsset asset) : base(asset)
		{
			_asset = asset;
		}
	}

	//public class ConditionalDurationResource
	//{
	//	// Character, Inventory, etc.
	//	public abstract class Target
	//	{
	//	}

	//	public abstract class CharacterTarget
	//	{

	//	}

	//	public abstract class ObjectTarget
	//	{

	//	}

	//	public abstract class InventoryTarget
	//	{

	//	}
	//}

}