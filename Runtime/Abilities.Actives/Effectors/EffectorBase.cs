using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class EffectorBase
	{
		//object ICopiable.MemberwiseCopy()
		//{
		//	return MemberwiseClone();
		//}

		//object ICloneable.Clone()
		//{
		//	return _Clone();
		//}

		//protected StrategyBase _Clone()
		//{
		//	var inst = (StrategyBase)MemberwiseClone();
		//	inst.OnEndedHandler = null;
		//	inst.OnSucceededHandler = null;
		//	return inst;
		//}

		public virtual bool Apply(IEffectContext context)
		{
			return Apply(context);
		}
	}
}