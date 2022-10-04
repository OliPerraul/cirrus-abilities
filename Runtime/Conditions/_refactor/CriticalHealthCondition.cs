using Cirrus.Unity.Numerics;
using UnityEngine;

namespace Cirrus.Arpg.Conditions
{
	// Might be cool to have a buff apply in critical health
	// (about to be injured) sometimes??

	public class CriticalHealthCondition : ConditionBase
	{
		[SerializeField]
		private Comparison _comparison;

		//public override bool Evaluate(CharacterComponent self)
		//{
		//	return comparison.Verify(
		//		self.Attributes.Health.Modified, 
		//		self.Attributes.CriticalHealthThreshold);
		//}

	}

}
