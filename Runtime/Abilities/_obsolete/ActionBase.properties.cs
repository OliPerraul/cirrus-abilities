//using System;
//using System.Collections.Generic;
//using Cirrus.Arpg.Abilities;
//using Cirrus.Arpg.Abilities;
//using Cirrus.Unity.Objects;
////using Cirrus.Objects; using Cirrus.Content;
//using Cirrus.Objects;
//using Cirrus.Unity.Objects;
//using System.Collections.Generic;
//using Cirrus.Unity.Numerics;
//using UnityEngine;

//namespace Cirrus.Arpg.Abilities
//{

//	public abstract partial class Action : IPrototype<Action>
//	{
//		public Action Prototype { get; set; }

//		public virtual Action OnEndedHandler { get; set; }

//		public virtual Range Range { get; set; } = 1;

//		public EntityBase Source { get; set; }

//		public virtual ActionType ActionType { get; set; } = ActionType.Unknown;

//		public virtual ActionFlags ActionFlags { get; set; } = 0;

//		public virtual Vector3 Direction { get; } = new Vector3();

//	}
//}
