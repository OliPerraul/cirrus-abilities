//#undef CIRRUS_ARPG_CHARACTER_SHADER_WRAPPER

using Cirrus.Collections;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using UnityEditor;
//using Cirrus.DH.Entities.Characters.Animations;

namespace Cirrus.Arpg.Abilities
{
	// TODO: Sequence of animation tabernoochie
	public class MaterialEffect : NonInstancedEffect, IPriorityEntry
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Effects/Material", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<MaterialEffect>();
#endif


#if CIRRUS_ARPG_OBJECT_SHADER_WRAPPER
		public EntityShaderDelta Delta { get; set; } = ObjectUtils.Empty<EntityShaderDelta>();
#endif
		public float Priority { get; set; } = 1;


		public override bool DoApply(IEffectContext context)
		{
			if(context.Target.Character != null)
			{
#if CIRRUS_ARPG_OBJECT_SHADER_WRAPPER
				context.Target.Materials.Apply(Delta);
#endif
				return true;
			}

			return false;
		}

		public override bool DoUnapply(IEffectContext context)
		{
			return false;
		}
	}
}
