using System;

namespace Cirrus.Arpg.Conditions
{
	// e.g. Raise attack while character is full health
	// enable a skill while full health ..
	//
	// TODO : why this over ConditionalDUration
	// because we can lerp remaining on the stat value..
	// 

	[Serializable]
	public class AttributeDuration : DurationBase
	{
		public override DurationInstanceBase CreateInstance()
		{
			return new AttributeDurationInstance(this);
		}
	}

	public class AttributeDurationInstance : DurationInstanceBase
	{
		private AttributeDuration _resource;
		protected override DurationBase _Resource => _resource;

		public AttributeDurationInstance(AttributeDuration asset) : base(asset)
		{
			_resource = asset;
		}

		//public int Flags { get; set; } = 0;
	}
}
