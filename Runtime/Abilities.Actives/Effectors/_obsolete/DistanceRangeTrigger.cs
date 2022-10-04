// THIS SHOULD BE HANDLED BY A CONDITION


////using Cirrus.Arpg.World.Entities.Actions;
//using Cirrus.Unity.Objects;
//using Cirrus.Unity.Numerics;

//namespace Cirrus.Arpg.Abilities
//{
//	// Area of effect
//	// TODO : what is an area of effect? Replace strategy with that.

//	public class DistanceRangeTrigger : AbilityTriggerBase
//	{
//		public DistanceRangeTrigger()
//		{ 
//		}
//		public DistanceRangeTrigger(float min, float max)
//		{
//			Range = new Range_(min, max);
//		}

//		public override bool Start(IEffectSource source, ObjectBase target = null)
//		{
//			bool result = false;

//			if(
//				source != null &&
//				target != null &&
//				Range.Contains((source.Entity.Position - target.Position).magnitude)
//				)
//			{
//				source._OnStrategySucceeded(this, source, target);
//				result = true;
//			}

//			source._OnStrategyEnded(this);
//			return result;
//		}
//	}
//}