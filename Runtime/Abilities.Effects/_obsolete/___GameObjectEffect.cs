//using Cirrus.Unity.Objects;
//using Cirrus.Objects;
//using Cirrus.Unity.Objects;
//using System;
//using UnityEngine;
//using CharacterTransformID = Cirrus.Arpg.Entities.Characters.CharacterTransformLibrary.ID;
//using ObjectTransformID = Cirrus.Arpg.Entities.ObjectTransformLibrary.ID;

//namespace Cirrus.Arpg.Abilities
//{
//	public class ___GameObjectEffect : EffectAssetBase
//	{
//		public ObjectTransformID ObjectTransformID = ObjectTransformID.Unknown;

//		public CharacterTransformID CharacterTransformID = CharacterTransformID.Unknown;

//		public GameObject GameObject;

//		// TODO
//		public Action<Transform, GameObject> AttachProcedure;

//		// TODO
//		public Action<Transform, GameObject> DetachProcedure;

//		private GameObject _gameObject;

//		private Transform _transform;

//		protected override bool _GetInstance(ObjectBase source, out EffectAssetBase instance)
//		{
//			instance = this.Realize();
//			instance.IsInstance = true;
//			return true;
//		}

//		protected override bool _Apply(IEffectSource action, ObjectBase target)
//		{
//			if(target != null)
//			{

//				_transform = CharacterTransformID == CharacterTransformID.Unknown ?

//					ObjectTransformID == ObjectTransformID.Unknown ?
//						target.Transform :
//						target.CharacterTransforms.Get(CharacterTransformID) :

//					target.CharacterTransforms.Get(CharacterTransformID);


//				if(_gameObject == null)
//				{
//					_gameObject = GameObject.Create(_transform);
//				}

//				return true;
//			}

//			return false;
//		}

//		protected override bool _Unapply(IEffectSource action, ObjectBase target)
//		{

//			return false;
//		}
//	}
//}
