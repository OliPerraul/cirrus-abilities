//using System;
//using System.Collections.Generic;
//using Cirrus.Arpg.Abilities;
//using Cirrus.Arpg.Abilities;
//using Cirrus.Unity.Objects;
////using Cirrus.Objects; using Cirrus.Content;
//using Cirrus.Objects;
//using Cirrus.Unity.Objects;
//using System.Collections.Generic;

//namespace Cirrus.Arpg.Abilities
//{
//	public abstract partial class Action : IPrototype<Action>
//	{
//		public virtual Action Clone_()
//		{
//			return (Action)MemberwiseClone();
//		}

//		public virtual void OnCloned() { }

//		public virtual void Use() { }

//		public virtual void Use(EntityBase source) { }

//		public virtual void Use(EntityBase target) { }

//		public virtual void Use(EntityBase source, EntityBase target) { }

//		protected virtual void _OnStrategySucceeded(StrategyBase strat, EntityBase target) { }

//		public virtual void End() { }
//	}
//}
