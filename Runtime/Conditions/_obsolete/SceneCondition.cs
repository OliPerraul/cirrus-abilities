//using UnityEngine;
//using System.Collections;
//using Cirrus.DH.Objects;
//using Cirrus.DH.Entities.Characters;

//namespace Cirrus.DH.Conditions
//{


//	public class SceneCondition : ComponentBase, Condition
//	{
//		public virtual bool Verify()
//		{
//			return false;
//		}

//		public virtual bool Verify(ObjectComponent target)
//		{
//			return Verify(target);
//		}

//		public virtual bool Verify(ObjectComponent source, ObjectComponent target)
//		{
//			return Verify(target);
//		}

//		public virtual bool Verify(ObjectComponent source, CharacterComponent target)
//		{
//			return Verify(source, target as ObjectComponent);
//		}


//		public virtual bool Verify(CharacterComponent subj)
//		{
//			return Verify((ObjectComponent)subj);
//		}


//		public virtual bool Verify(CharacterComponent source, CharacterComponent target)
//		{
//			return Verify(source as ObjectComponent, target);
//		}

//		public virtual bool Verify(CharacterComponent source, ObjectComponent target)
//		{
//			return Verify(source as ObjectComponent, target);
//		}


//		public virtual bool Verify(ObjectBase target)
//		{
//			return Verify();
//		}

//		public virtual bool Verify(Character target)
//		{
//			return Verify((ObjectBase)target);
//		}

//		public virtual bool Verify(ObjectBase source, ObjectBase target)
//		{
//			return Verify(target);
//		}

//		public virtual bool Verify(Character source, ObjectBase target)
//		{
//			return Verify(source as ObjectBase, target);
//		}

//		public virtual bool Verify(Character source, Character target)
//		{
//			return Verify(source as ObjectBase, target);
//		}

//		///

//		public virtual IConditionedState GetConditionedState(ObjectBase target)
//		{
//			return null;
//		}

//		public virtual IConditionedState GetConditionedState(Character target)
//		{
//			return null;
//		}


//		public virtual IConditionedState GetConditionedState(ObjectComponent target)
//		{
//			return null;
//		}

//		public virtual IConditionedState GetConditionedState(CharacterComponent target)
//		{
//			return null;
//		}
//	}
//}
