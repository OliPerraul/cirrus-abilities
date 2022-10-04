
//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Debugging;
using Cirrus.Unity.Objects;
using static Cirrus.Debugging.DebugUtils;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	// TODO animation

	public partial class HitboxEffector
	{ 	
	}

	public partial class HitboxEffectorBase : EffectorBase
	{

		// Collider should not be tied with character (source)
		// Vs. Use source hit collider??
		private bool _GetCollider(CharacterObject source, out AbilityHitbox collider)
		{
			//if(_collider != null) return _collider;
			collider = null;
			if (source == null) return false;
			collider = source.ColliderLibrary.Get<AbilityHitbox>((int)Collider);
			return true;
		}

		private void _RegisterCollider(AbilityHitbox collider)
		{
			if(!collider.IsDestroyed())
			{
				collider.gameObject.SetActive(true);
				collider.OnTargetHitHandler += _OnTargetHit;
				collider.OnAnimationFinishedHandler += _OnAnimationFinished;
			}
		}


		private void _UnregisterCollider(AbilityHitbox collider)
		{
			if(!collider.IsDestroyed())
			{
				collider.gameObject.SetActive(false);
				collider.OnTargetHitHandler -= _OnTargetHit;
				collider.OnAnimationFinishedHandler -= _OnAnimationFinished;
			}
		}

		private void _OnTargetHit(AbilityHitbox collider, EntityObjectBase target)
		{
			collider.Context._OnEffectorResult();
		}

		private void _OnAnimationFinished(AbilityHitbox collider)
		{
			_UnregisterCollider(collider);
		}


		// TODO unsubscribe once animation is finished
		// We dont care whihc type of actor
		public override bool Apply(IEffectContext context)
		{
			if(!ReturnAssert(!(context.Target != null && context.Source == context.Target),	AssertType.One,	$"{nameof(Apply)} (1)"))
				return false;

			if(!ReturnAssert(context.Source is CharacterObject, AssertType.One, $"{nameof(Apply)} (2)"))
				return false;			

			if (!ReturnAssert(_GetCollider(context.Source.CharacterObject, out AbilityHitbox collider), AssertType.One, $"{nameof(Apply)} (3)"))			
				return false;

			// TODO make sure collider not in use
			_RegisterCollider(collider);
			collider.Animate(context, ColliderAnimation, ColliderAnimTime);


			return true;
		}
	}
}