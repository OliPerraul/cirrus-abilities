//using Cirrus.Assets.Cirrus.Unity.Editor.Attributes;
using Cirrus.Arpg.Attributes;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using UnityEditor;
using UnityEngine;
//using Cirrus.Arpg.Content.Attributes;

namespace Cirrus.Arpg.Abilities
{
	public partial class AttributeEffect : NonInstancedEffect
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Effects/Attribute", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<AttributeEffect>();
#endif

		[field: Header(nameof(AttributeEffect))]
		[field: SerializeField]		
		public Operation Operation { get; set; }

		[field: SerializeField]
		public float Amount { get; set; }

		[field: SerializeField]
		public AttributeDescriptor Attribute { get; set; }


		public override bool DoApply(IEffectContext context)
		{
			if (context.Target.Attributes.Get(Attribute, out AttributeInstance attr))
			{
				attr.Update(Operation.EvaluateFirstSecond(attr.Current, Amount));
				return true;
			}

			return false;
		}
	}
}
