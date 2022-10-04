using Cirrus.Arpg.Entities;

using System;

using UnityEngine;

namespace Cirrus.Arpg.Conditions
{
	[Serializable]
	public partial class TimeDuration : DurationBase
	{		

		[field: SerializeField]
		public float TimeLimit { get; set; }

		//public override float Current
		//{
		//	get => Total - _timer.Time;
		//	set
		//	{
		//		//if (value < 0) _current = 0;

		//		//else if (value > Total) _current = Total;

		//		//else _current = value;
		//	}
		//}

		public override DurationInstanceBase CreateInstance()
		{
			return new TimeDurationInstance(this);
		}

		//protected override void _Start(ObjectBase entity)
		//{
		//	base._Start(entity);
		//	if (_timer == null)
		//	{
		//		_timer = new Timer(Total, start: false, repeat: false);
		//		_timer.OnTimeoutHandler += OnEnded;
		//		_timer.OnTickHandler += OnTicked;
		//	}
		//	_timer.Reset();
		//}

		// TODO
		//public override bool Consolidate(DurationBase duration)
		//{
		//	if (duration is TimeDuration)
		//	{
		//		TimeDuration timeDuration = (TimeDuration)duration;
		//		_timer.AddTime(timeDuration.Length);
		//		return true;
		//	}

		//	return false;
		//}


		//public override void OnResourceRealized()
		//{
		//	_timeLimit = Total;
		//}
	}
	
	public partial class TimeDurationInstance : DurationInstanceBase
	{
		private Timer _timer;

		//private float _timeLimit;


		protected override DurationBase _Resource => _asset;
		private TimeDuration _asset;

		public TimeDurationInstance(TimeDuration asset) : base(asset)
		{
			_asset = asset;
		}

		public void OnTicked(float value)
		{
			float delta = _timer.DeltaTime / _timer.Limit;
			//OnRemainingHandler?.Invoke(this, _timer.Time / _timeLimit, delta);
		}
	}
}