using System;

namespace Cirrus.Arpg.Conditions
{
	//uration until an effect occurs

	[Serializable]
	public class EffectDuration : DurationBase
	{
		//public int Flags { get; set; } = 0;

		public override DurationInstanceBase CreateInstance()
		{
			return new EffectDurationInstance(this);
		}
	}

	public class EffectDurationInstance : DurationInstanceBase
	{
		private EffectDuration _asset;
		protected override DurationBase _Resource => _asset;

		public EffectDurationInstance(EffectDuration asset) : base(asset)
		{
			_asset = asset;
		}

		//public int Flags { get; set; } = 0;
	}
}