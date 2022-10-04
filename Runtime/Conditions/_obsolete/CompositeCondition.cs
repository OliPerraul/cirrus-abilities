//using Cirrus.Unity.Objects;
//using System.Collections.Generic;

//namespace Cirrus.Arpg.Conditions
//{
//	public class CompositeCondition : ConditionBase
//	{
//		public enum CompositeMode
//		{
//			Or,
//			And			
//		}

//		public CompositeMode Mode { get; set; } = CompositeMode.And;

//		public ICollection<ConditionBase> Conditions { get; set; } = new ConditionBase[0];

//		public override bool Evaluate(EntityBase left, EntityBase right)
//		{
//			if (Mode == CompositeMode.And)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (!cond.Evaluate(left, right))
//						return false;
//				}

//				return true;
//			}
//			else if (Mode == CompositeMode.Or)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (cond.Evaluate(left, right))
//						return true;
//				}
//			}

//			return false;
//		}

//		public override bool EvaluateOneOperand(EntityBase operand)
//		{
//			if (Mode == CompositeMode.And)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (!cond.EvaluateOneOperand(operand))
//						return false;
//				}

//				return true;
//			}
//			else if (Mode == CompositeMode.Or)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (cond.EvaluateOneOperand(operand))
//						return true;
//				}
//			}

//			return false;
//		}

//		public override bool Evaluate()
//		{
//			if (Mode == CompositeMode.And)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (!cond.Evaluate())
//						return false;
//				}

//				return true;
//			}
//			else if (Mode == CompositeMode.Or)
//			{
//				foreach (var cond in Conditions)
//				{
//					if (cond.Evaluate())
//						return true;
//				}
//			}

//			return false;
//		}


//	}
//}
