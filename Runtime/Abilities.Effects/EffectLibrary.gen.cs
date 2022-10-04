//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cirrus.Unity.Animations;
//using Cirrus.Objects;
//using Cirrus.Arpg.Abilities;

//namespace Cirrus.Arpg.Abilities
//{
//	public partial class EffectLibrary
//	{
//		[System.Serializable]
//		public enum ID : int
//		{
//			Unknown = -1,
//			EffectDetails_Empty,
//		}

//		[System.NonSerialized]
//		private EmptyEffectDetails __effectDetails_empty__ = null;
//		public EmptyEffectDetails __EffectDetails_Empty__ => __effectDetails_empty__ == null ?
//				__effectDetails_empty__ = Get<EmptyEffectDetails>((int)ID.EffectDetails_Empty) :
//				__effectDetails_empty__;
//		public static EmptyEffectDetails EffectDetails_Empty => Instance.__EffectDetails_Empty__;

//		protected override void _CustomLoad()
//		{
//		}
//	}
//}
