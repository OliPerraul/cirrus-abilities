//using Cirrus.Arpg.World.Entities.Actions;

using UnityEngine;

using CharacterTransformID = Cirrus.Arpg.Entities.Characters.CharacterTransformLibrary.ID;

namespace Cirrus.Arpg.Abilities
{
	public partial class ProjectileEffector : EffectorBase
	{
		[SerializeField]
		public Projectile Projectile { get; set; }

		[SerializeField]
		public Vector3 PositionOffset = Vector3.zero;

		[SerializeField]
		public float StartSpeed = 1f;

		[SerializeField]
		public float EndSpeed = 1f;

		[SerializeField]
		public float Gravity = 1.0f;

		public AnimationCurve AnimCurve { get; set; }

		public CharacterTransformID CharacterTransform = CharacterTransformID.Unknown;

		private IEffectContext _source;
	}
}