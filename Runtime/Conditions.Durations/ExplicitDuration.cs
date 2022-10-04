using System;

using UnityEngine;

namespace Cirrus.Arpg.Conditions
{
	// Inspired by Treatment duration, this one can only changed manually through effects
	// Filters can be applied on effect to mimic treatment efficacy
	// TODO: explicit
	[Serializable]
	public class ExplicitDuration : DurationBase
	{
		public override DurationInstanceBase CreateInstance()
		{
			return new ExplicitDurationInstance(this);
		}
	}

	public class ExplicitDurationInstance : DurationInstanceBase
	{
		private ExplicitDuration _resource;
		protected override DurationBase _Resource => _resource;
		

		public override float Current
		{
			get => _current;
			set
			{
				if (value < 0) _current = 0;

				else if (value > _max) _current = _max;

				else _current = value;
			}
		}

		public ExplicitDurationInstance(ExplicitDuration resource) : base(resource)
		{
			_resource = resource;
		}
	}
}
