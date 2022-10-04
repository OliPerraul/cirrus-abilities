using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Content;
using Cirrus.Source;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public partial class EffectLibrary : LibraryAssetBase<EffectLibrary>
	{
		protected override void LoadInEditor()
		{
			//LoadAssets<EffectDetailsMonoBehaviourBase>(asset => asset.name.FormatIdentifierName());
			LoadAssets<EffectBase>(asset => asset.name.FormatIdentifierName());
		}
	}
}