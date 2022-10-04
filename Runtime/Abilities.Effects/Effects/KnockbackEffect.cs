using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Animations;
using Cirrus.Unity.Editor;
using System;
using UnityEditor;
using UnityEngine;
using AnimationUtils = Cirrus.Animations.AnimationUtils;
//using System.Numerics;


namespace Cirrus.Arpg.Entities.Characters
{
	// TODO modifier duration could be stacked
	// operator overload add duration

	public partial class CharacterInstanceBase
	{
		public Action<KnockbackEffect> OnKnockbackHandler;
	}
}


namespace Cirrus.Arpg.Abilities
{
	public partial class KnockbackEffect : NonInstancedEffect
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Effects/Knockback", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<KnockbackEffect>();
#endif
		[SerializeField]
		public float strength = 10;

		[SerializeField]
		public float knockbackTime = 2f;

		[SerializeField]
		public AnimationCurve animCurve;

		public override bool DoApply(IEffectContext context)
		{
			if(context.Target == null) return false;

			context.Target.Knockbacks.AddKnockback(new Knockback
			{
				CurveFalloff = animCurve,
				CurveTime = knockbackTime,
				Velocity = context.Direction * strength
			});

			return true;
		}
	}

}



