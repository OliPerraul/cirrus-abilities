using Cirrus.Events;
using Cirrus.Arpg.Entities;
////using Cirrus.Arpg.World.Entities.Actions.Strategies;
using Cirrus.Unity.Objects;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	// TODO: Pool
	// Spread ( Scatter etc)

	public partial class Projectile : ComponentBase
	{
		public override void Awake()
		{
			base.Awake();
			EventForwarderComponent forward = Collider.gameObject.GetOrAddComponent<EventForwarderComponent>();
			forward.OnCollisionEnterEvent += _OnObjectCollisionEnter;
		}

		public override void Start()
		{
			base.Start();
		}

		// gravity and drag.
		public void FixedUpdate()
		{
			if(_isIdle) return;
			float w = Curve.Evaluate(_animationTime += Time.fixedDeltaTime);
			float speed = w * StartSpeed + (1 - w) * EndSpeed;
			Velocity = Velocity.normalized * speed;
			_Rigidbody.AddForce(Physics.gravity * Gravity * _Rigidbody.mass);
		}

		private void _OnMotionEnded()
		{
			_isIdle = true;
			_Rigidbody.useGravity = true;
		}

		private void _OnObjectCollisionEnter(GameObject source, Collision other)
		{
			if(_isIdle) return;
			_OnMotionEnded();
			var obj = other.gameObject.GetComponentInParent<EntityObjectBase>();
			if(obj != null)
			{
				if(obj != Source)
				{
					OnTargetHitHandler?.Invoke(obj);
					Destroy(gameObject, 0.002f);
				}
			}
		}


	}
}
