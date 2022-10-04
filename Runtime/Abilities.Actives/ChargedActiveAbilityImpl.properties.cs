using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using System;
using UnityEditor;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public partial class ChargedActiveAbilityImpl : SustainedActiveAbilityImplBase
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Active Ability Impls/Charged", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<ChargedActiveAbilityImpl>();
#endif
	}

	// TODO: Cooldown should only be part of the charged region

	// https://www.reddit.com/r/dragonage/comments/2mwctn/is_anyone_else_really_missing_sustained_abilities/
	// https://www.google.com/search?sxsrf=ALeKk00rObXHKJwOb8mTK9Nh2hYy-5xuyw:1618893099859&q=Sustained+Active+Ability&spell=1&sa=X&ved=2ahUKEwjmtZXh_ovwAhXJXM0KHRBACgEQBSgAegQIAhAw&biw=1536&bih=662
	//////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	///

	public partial class ChargedActiveAbilityImplInstance : SustainedActiveAbilityImplInstanceBase
	{
		private WaitForSeconds _frequencyTimeWaitForSeconds;

		private ChargedActiveAbilityImpl _resource;
		protected override ActiveAbilityImplBase _ActiveAbilityImpl => _resource;

		protected override AbilityImplBase _AbilityResourceImpl => _resource;

		protected override SustainedActiveAbilityImplBase _SustainedActiveAbilityImpl
		{
			get => _resource;
			set => _resource = (ChargedActiveAbilityImpl)value;
		}

		//public AbilityActionBase SelfInflictedEndAction { get; set; }

		//public AbilityActionBase EndAction { get; set; }
	}
}
