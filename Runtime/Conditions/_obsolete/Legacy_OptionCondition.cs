//using Cirrus.Arpg.Conditions;
//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.Arpg.AI;
//using System;

//namespace Cirrus.Arpg.Conditions
//{
//	[Obsolete]
//	public class Legacy_OptionCondition : ConditionBase
//	{
//		public int ID { get; set; } = -1;

//		public int Flags { get; set; } = 0;

//		//public override bool EvaluateOneOperand(EntityBase operand)
//		//{
//		//	return operand.EvaluateOneOperand(this);
//		//}

//		public override IConditionedState GetConditionedState(CharacterEntity target)
//		{
//			return target.CharacterObjectInstance.StateMachine;
//			//return base.GetConditionedState( target );
//		}
//	}
//}

////namespace Cirrus.Arpg.Objects
////{
////	public partial class EntityBase
////	{
////		public virtual bool EvaluateOneOperand(Legacy_OptionCondition condition)
////		{
////			return false;
////		}
////	}
////}

////namespace Cirrus.Arpg.Entities.Characters
////{
////	public partial class CharacterEntity
////	{
////		public override bool EvaluateOneOperand(Legacy_OptionCondition condition)
////		{
////			if(CharacterObjectInstance.Control.Agent.Knowledge.Goal == null) return false;
////			if(CharacterObjectInstance.Control.Agent.Knowledge.Goal.Option == null) return false;

////			return condition.Flags == 0 ?
////				Agent.Goal.Option.ID == condition.ID :
////				Agent.Goal.Option.EntityFlags.Intersects(condition.Flags);
////		}
////	}

////}