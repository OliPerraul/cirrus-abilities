
//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Editor;
using System;

using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	[Serializable]
	public class SelfEffector : EffectorBase
	{
		public override bool Apply(IEffectContext context)//BaseObject target)
		{
			context._OnEffectorResult();
			//source._OnTriggerEnded(this);
			return true;
		}
	}
}