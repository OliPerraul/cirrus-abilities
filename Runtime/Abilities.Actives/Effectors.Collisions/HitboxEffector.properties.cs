
//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Unity.Editor;
using System;
using UnityEditor;
using UnityEngine;
using CharacterColliderID = Cirrus.Arpg.Entities.Characters.CharacterColliderLibrary.ID;

namespace Cirrus.Arpg.Abilities
{
	// TODO animation
	public abstract partial class HitboxEffectorBase
	{		
		public abstract int ColliderAnimation { get; }

		// TODO replace with on animation finished
		[field: SerializeField]
		public float ColliderAnimTime { get; set; } = 2f;

		[field: SerializeField]
		public CharacterColliderID Collider { get; set; }
	}

	// TODO animation
	[Serializable]
	public partial class HitboxEffector : HitboxEffectorBase
	{
		[SerializeField]
		public HitboxAnimationID _colliderAnimation;
		public override int ColliderAnimation => (int)_colliderAnimation;
	}
}