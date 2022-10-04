
//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Editor;
using System;

using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	[Serializable]
	public class DirectEffector : EffectorBase
	{
		public override bool Apply(IEffectContext context)
		{
			context._OnEffectorResult();
			return true;
		}
	}
}