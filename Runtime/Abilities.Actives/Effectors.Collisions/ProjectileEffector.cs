//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Unity.Objects;
using UnityEngine;

using CharacterTransformID = Cirrus.Arpg.Entities.Characters.CharacterTransformLibrary.ID;

namespace Cirrus.Arpg.Abilities
{
	public partial class ProjectileEffector : EffectorBase
	{
		// Active after timer
		// TODO: source._OnStrategyEnded(this);

		public override bool Apply(IEffectContext context)
		{
			if(
				context.Target != null &&
				context.Source == context.Target)
			{
				return false;
			}

			_source = context;

			Vector3 direction =
				context.Target == null ?
				context.Source.Transform.forward :
				(context.Target.Position - context.Source.Position).normalized;

			
			Transform GetCharacterTransform(CharacterObject chara) => chara.CharacterTransforms.Get(CharacterTransform);

			Projectile.Instantiate(
				CharacterTransform == CharacterTransformID.Unknown ?
					context.Source.Position + PositionOffset :
					GetCharacterTransform(context.Source.CharacterObject).position,
				projectile =>
				{
					projectile.Source = context.Source;
					projectile.Velocity = direction * StartSpeed;
					projectile.StartSpeed = StartSpeed;
					projectile.EndSpeed = EndSpeed;
					projectile.Gravity = Gravity;
					projectile.Curve = AnimCurve;
					projectile.OnTargetHitHandler += OnHit; // TODO: unsub

					foreach(
					var sourceCollider in
					context.Source.Transform.GetComponentsInChildren<Collider>())
					{
						Physics.IgnoreCollision(projectile.Collider, sourceCollider, true);
					}

				});

			//source._OnTriggerEnded(this);
			return true;
		}

		public void OnHit(EntityObjectBase target)
		{
			//_direction = (target.Position - _source.Entity.Position).normalized;
			_source._OnEffectorResult();
		}
	}
}