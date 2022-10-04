using Cirrus.Arpg.Abilities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System;
using UnityEngine;
using static Cirrus.Debugging.DebugUtils;

namespace Cirrus.Arpg.Conditions
{
	[Serializable]
	public abstract partial class DurationBase : ScriptableObjectBase
	{
		// TODO float if time
		// binary otherwise (all or nothing)
		// Current and delta

		//  For example 3/4 and 6/8 might b

		// TODO Simple might be enough, otherwise rely on curves..
		[field: SerializeField]
		public float ConsolidationWeight { get; set; } = 1;


		[field: SerializeField]
		public Range_ Length { get; set; }

		//private float _current = 0;
		//public virtual float Current 
		//{
		//	get => _current;
		//	set
		//	{
		//		Assert(!IsReadOnlyObject);
		//		_current = value;
		//	}
		//}

		//public float Ratio => Current / Total;

		//public bool IsReadOnlyObject { get; set; }
	}

	public abstract partial class DurationInstanceBase
	//: ScriptableObjectAssetBase
	//: ICopiable
	//, IRealizablePrototype
	//, IReadOnlyCandidate
	{
		protected abstract DurationBase _Resource { get; }

		// TODO float if time
		// binary otherwise (all or nothing)
		// Current and delta
		public Action<DurationInstanceBase, float, float> OnRemainingHandler;

		//public bool IsRealizable { get; set; } = false;

		//public object ProtoData { get; set; }
		//public object[] RealizeStepCbs { get; set; }

		//public object ProxyCb { get; set; }

		public Action<DurationInstanceBase> OnEndedHandler;


		//  For example 3/4 and 6/8 might b

		// TODO Simple might be enough, otherwise rely on curves..
		//public float ConsolidationWeight = 1;

		//[HideInInspector]
		//public float Total;

		protected float _max = 0;

		protected float _current = 0;
		public virtual float Current
		{
			get => _current;
			set
			{
				Assert(!IsReadOnlyObject);
				_current = value;
			}
		}

		public float Ratio => Current / _Resource.Length;

		public bool IsReadOnlyObject { get; set; }
	}
}
