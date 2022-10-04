////using Cirrus.Arpg.World.Entities.Actions.Strategies;
using Cirrus.Collections;
using Cirrus.Events;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Objects;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public partial class AbilityHitbox : ComponentBase
	{
		public AbilityHitbox Create(EntityObjectBase source)
		{
			AbilityHitbox p = this.Instantiate(source.Transform);
			p._source = source;
			return p;
		}

		public override void Awake()
		{
			base.Awake();

			if (_animatorWrapper != null)
			{
				_animatorWrapper = Object.Instantiate(_animatorWrapper);
				_animatorWrapper.Animator = Animator;
				_animatorWrapper.Component = this;
			}
		
			//_animatorWrapper = new AbilityColliderAnimatorWrapper(_animator);
			// TODO replace with on animation timeout
			_animationTimer = new Timer(OnAnimationTimeout);

			EventForwarderComponent forward = Collider.gameObject.GetOrAddComponent<EventForwarderComponent>();
			forward.OnTriggerEnterEvent += OnObjectTriggerEnter;
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

		}

		public void Animate<T>(IEffectContext context, T animation, float time = 2f) where T : System.Enum
		{
			_context = context;
			_entered.Clear();
			if(_animatorWrapper != null) _animatorWrapper.Play(animation);
			if(_animationTimer != null) _animationTimer.Reset(time, true);
		}

		public void Animate(IEffectContext context, int animation, float time = 2f)
		{
			_context = context;
			_entered.Clear();
			if (_animatorWrapper != null) _animatorWrapper.Play(animation);
			if (_animationTimer != null) _animationTimer.Reset(time, true);
		}

		public void OnAnimationTimeout()
		{
			OnAnimationFinishedHandler?.Invoke(this);
			_context = null;
		}

		public void OnObjectTriggerEnter(
			GameObject gameObject,
			Collider otherCollider)
		{
			if(_context == null) return;
			if(
				otherCollider.TryGetComponentInParent(out EntityObjectBase other) &&
				other.Transform.gameObject == otherCollider.gameObject)
			{
				if(other == _Source) return;

				switch(_mode)
				{
					case CollisionMode.Once:
						if(!_entered.IsEmpty())
							return;
						break;

					case CollisionMode.OncePerObject:
						if(_entered.Contains(other))
							return;
						break;
				}

				//Debug.Log("On target hit");
				_entered.Add(other);
				OnTargetHitHandler?.Invoke(this, other);
			}
		}
	}
}
