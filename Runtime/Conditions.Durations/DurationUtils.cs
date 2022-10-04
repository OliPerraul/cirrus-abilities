using Cirrus.Objects;
using Cirrus.Unity.Numerics;

using System;

namespace Cirrus.Arpg.Conditions
{
	[Serializable]
	public class Duration : DurationBase 
	{
		public override DurationInstanceBase CreateInstance()
		{
			return new DurationInstance(this);
		}
	}

	public class DurationInstance : DurationInstanceBase
	{
		private Duration _asset;
		protected override DurationBase _Resource => _asset;
		public DurationInstance(Duration asset) : base(asset)
		{
			_asset = asset;
		}
	}

	public static class DurationUtils
	{		
		// Used for explicit removal
		//public static DurationAssetBase Null = new DurationAsset()
		//{
		//	Current = 1,
		//	Total = 1
		//};

		public static float Evaluate(this DurationInstanceBase duration, Operation op)
		{
			duration.Current = op.Evaluate(duration.Current);
			return duration.Current;
		}
	}
}
