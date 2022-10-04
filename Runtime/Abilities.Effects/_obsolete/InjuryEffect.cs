//using Cirrus.Collections;
//using Cirrus.Arpg.Abilities;
//using Cirrus.Unity.Objects;
//using Cirrus.Objects;
//using Cirrus.Unity.Animations;
//using Cirrus.Unity.Objects;
//using System;
//using static Cirrus.Debugging.DebugUtils;
//using CharacterTransformID = Cirrus.Arpg.Entities.Characters.CharacterTransformLibrary.ID;
//using AnimationUtils = Cirrus.Animations.AnimationUtils;
//using Cirrus.Arpg.Conditions;
//using Random = UnityEngine.Random;

//namespace Cirrus.Arpg.Abilities
//{
//	public class InjuryEffect : ModifierEffect
//	{
//		public string Name;

//		public float Chance = 0.8f;

//		public AnimCurveBase ChanceCurve = (Func<float, float>)((float val) => AnimationUtils.EaseOutSine(0, 1, val));

//		public int TargetFlags = BitwiseUtils.Full;

//		// NOTE: probability = chance * ratio

//		public InjuryEffect()
//		{
//			this.AddRealizeStep(effect =>
//			{
//				//TargetConditions = new ConditionBase[]
//				//{
//				//	new Condition((source, target) =>
//				//	{
//				//		return target.ContentFlags.Intersects(TargetFlags);
//				//	}),
//				//	new Condition((source, target) =>
//				//	{
//				//		float ratio = 1.0f - (target.Attrs.Attr_Health.Current / target.Attrs.Attr_Health.Total);
//				//		float value = Random.Range(0f, 1f);
//				//		float weight = ChanceCurve.Evaluate(ratio);
//				//		if (value < weight * Chance)
//				//		{
//				//			return true;
//				//		}

//				//		return false;
//				//	})
//				//};
//			});
//		}
//	}
//}
