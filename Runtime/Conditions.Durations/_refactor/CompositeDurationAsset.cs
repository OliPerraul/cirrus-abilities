using System;
using System.Collections.Generic;

namespace Cirrus.Arpg.Conditions
{
	// TODO remove
	[Serializable]
	public class CompositeDurationAsset : DurationBase
	{
		public enum CompositeMode
		{
			Or,
			And
		}
		public CompositeMode Mode { get; set; }
		public ICollection<DurationBase> Durations { get; set; }

		private int _finished = 0x0000000000;


		public override DurationInstanceBase CreateInstance()
		{
			return new CompositeDuration(this);
		}


		//protected override DurationBase _Clone()
		//{
		//	var instance = (CompositeDuration) base._Clone();

		//	instance.Durations = new List<DurationBase>();
		//	foreach(var duration in Durations)
		//	{
		//		instance.Durations.Add(duration.DeepCopy());
		//	}

		//	return instance;
		//}

	}

	public class CompositeDuration : DurationInstanceBase
	{
		private CompositeDurationAsset _asset;
		protected override DurationBase _Resource => _asset;

		public CompositeDuration(CompositeDurationAsset asset) : base(asset)
		{
			_asset = asset;
		}
	}
}