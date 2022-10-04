using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;

using System;
using static Cirrus.Debugging.DebugUtils;

namespace Cirrus.Arpg.Conditions
{
	public abstract partial class DurationBase
	{
		//object ICloneable.Clone()
		//{
		//	return _Clone();
		//}

		//protected virtual DurationBase _Clone()
		//{
		//	var instance = (DurationBase)MemberwiseClone();
		//	instance.OnEndedHandler = null;
		//	instance.OnRemainingHandler = null;			
		//	return instance;
		//}

		public abstract DurationInstanceBase CreateInstance();


		public DurationBase()
		{ 
		}

		//public static implicit operator DurationAssetBase(float total)
		//{
		//	return new ExplicitDurationAsset(total);
		//}
	}

	public abstract partial class DurationInstanceBase
	{
		//object ICopiable.MemberwiseCopy()
		//{
		//	return MemberwiseClone();
		//}

		public DurationInstanceBase(DurationBase resource)
		{
			_current = resource.Length.Random();
			_max = _current;
		}

		//object ICloneable.Clone()
		//{
		//	return _Clone();
		//}

		//protected virtual DurationBase _Clone()
		//{
		//	var instance = (DurationBase)MemberwiseClone();
		//	instance.OnEndedHandler = null;
		//	instance.OnRemainingHandler = null;			
		//	return instance;
		//}


		//public static implicit operator DurationBase(float total)
		//{
		//	return new ExplicitDuration(total);
		//}

		protected virtual void _Start(EntityObjectBase source)
		{
			Assert(!IsReadOnlyObject);
			OnRemainingHandler?.Invoke(this, 1, 0);
		}

		public void Start(EntityObjectBase source)
		{
			_Start(source);
		}

		protected virtual void _Stop()
		{
			OnRemainingHandler?.Invoke(this, 1, 0);
		}

		public void Stop(EntityObjectBase source)
		{
		}

		public virtual void OnEnded()
		{
			OnRemainingHandler?.Invoke(this, 0, 0);
			OnEndedHandler?.Invoke(this);
		}


	}
}