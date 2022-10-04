//using Cirrus.Arpg.Conditions;
//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.Entities.Characters;
//using System;
//using ControlStateID = Cirrus.Arpg.Entities.Characters.Controls.ControlStateID;

//namespace Cirrus.Arpg.Conditions
//{
//	[Obsolete]
//	public class Legacy_CharacterStateCondition : ConditionBase
//	{
//		public CharacterStateId StateID { get; set; } = (CharacterStateId)(-1);

//		public int StateFlags { get; set; } = 0;

//		//public override bool Evaluate(EntityBase first, EntityBase second)
//		//{
//		//	return operand.Evaluate(this, second);
//		//}

//		public override IConditionedState GetConditionedState( CharacterEntity target )
//		{
//			return target.CharacterObjectInstance.StateMachine;
//			//return base.GetConditionedState( target );
//		}
//	}

//	[Obsolete]
//	public class Legacy_CharacterControlStateCondition : ConditionBase
//	{
//		public ControlStateID StateID { get; set; } = (ControlStateID) (-1);

//		public int StateFlags { get; set; } = 0;

//		//public override bool EvaluateOneOperand(EntityBase operand)
//		//{
//		//	return operand.EvaluateOneOperand(this);
//		//}
//	}
//}

//namespace Cirrus.Arpg.Objects
//{
//	public partial class EntityBase
//	{
//		public virtual bool EvaluateOneOperand(Legacy_CharacterStateCondition condition)
//		{
//			return false;
//		}

//		public virtual bool EvaluateOneOperand(Legacy_CharacterControlStateCondition condition)
//		{
//			return false;
//		}
//	}	
//}

//namespace Cirrus.Arpg.Entities.Characters
//{
//	public partial class CharacterEntity
//	{
//		public override bool EvaluateOneOperand(Legacy_CharacterStateCondition condition)
//		{
//			if ( CharacterObjectInstance.StateMachine.Current == null ) return false;

//			if(condition.StateFlags == 0)
//			{
//				return CharacterObjectInstance.StateMachine.Current.ID == (int)condition.StateID;
//			}
//			else
//			{
//				return (CharacterObjectInstance.StateMachine.Current.StrategyFlags & condition.StateFlags) != 0;
//			}
//		}


//		public override bool EvaluateOneOperand(Legacy_CharacterControlStateCondition condition)
//		{
//			if ( CharacterObjectInstance.Control.StateMachine.Current == null ) return false;

//			if (condition.StateFlags == 0)
//			{
//				return CharacterObjectInstance.Control.State.ID == (int)condition.StateID;
//			}
//			else
//			{
//				return (CharacterObjectInstance.Control.State.StrategyFlags & condition.StateFlags) != 0;
//			}

//		}
//	}

//}