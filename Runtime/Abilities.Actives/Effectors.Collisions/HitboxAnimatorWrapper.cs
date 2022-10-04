using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cirrus.Unity.Animations;
using Cirrus.Unity.Editor;
using UnityEditor;
namespace Cirrus.Arpg.Abilities
{
    public enum HitboxAnimationID
    {
        Weapon_RightSwing=-1418821887,
        Idle=2081823275,
        Hitbox_HandCombat_RightSwing=620323185,
    }
    public interface IHitboxAnimatorWrapper : IAnimatorWrapper
    {
        float BaseLayerLayerWeight{set;}
        float BaseLayerLayerWeight_Safe{set;}
    }
    public class HitboxAnimatorWrapper : AnimatorWrapperBase, IHitboxAnimatorWrapper
    {

			[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Triggers.Collisions/Ability Hitbox", false, priority = PackageUtils.MenuItemAssetFrameworkPriority)]
			public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<HitboxAnimatorWrapper>();
			        public float BaseLayerLayerWeight_Safe{set { if(_animator != null && _animator.isActiveAndEnabled && _animator.runtimeAnimatorController != null) _animator.SetLayerWeight(0,value);} }
        public float BaseLayerLayerWeight{set { _animator.SetLayerWeight(0,value);} }
        public override Animator Animator {get => _animator; set
        {
            if(value==null)return;
            _animator = value;
            _stateSpeedValues.Add((int)HitboxAnimationID.Weapon_RightSwing, 1);
            _stateLayerValues.Add((int)HitboxAnimationID.Weapon_RightSwing, _animator.GetLayerIndex("Base Layer"));
            _stateSpeedValues.Add((int)HitboxAnimationID.Idle, 1);
            _stateLayerValues.Add((int)HitboxAnimationID.Idle, _animator.GetLayerIndex("Base Layer"));
            _stateSpeedValues.Add((int)HitboxAnimationID.Hitbox_HandCombat_RightSwing, 1);
            _stateLayerValues.Add((int)HitboxAnimationID.Hitbox_HandCombat_RightSwing, _animator.GetLayerIndex("Base Layer"));
        }
        }
        public HitboxAnimatorWrapper()
        {
        }
        public HitboxAnimatorWrapper(Animator animator)
        {
            Animator = animator;
        }
    }
}
