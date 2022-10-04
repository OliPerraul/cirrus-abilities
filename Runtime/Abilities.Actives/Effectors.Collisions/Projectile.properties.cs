
using Cirrus.Arpg.Entities;
////using Cirrus.Arpg.World.Entities.Actions.Strategies;
using Cirrus.Unity.Objects;
using System;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	// TODO: Pool
	// Spread ( Scatter etc)

	public partial class Projectile : ComponentBase
	{
		[NonSerialized]
		public EntityObjectBase Source = null;

		////
		///
		public Vector3 Velocity
		{
			get => _Rigidbody.velocity;
			set => _Rigidbody.velocity = value;
		}

		private ObjectWrapper<Rigidbody> _rigidbody;
		private Rigidbody _Rigidbody =>
			_rigidbody == null ?
			_rigidbody = GetComponentInChildren<Rigidbody>() :
			_rigidbody;

		private ObjectWrapper<Collider> _collider;
		public Collider Collider =>
			_collider == null ?
			_collider = GetComponentInChildren<Collider>() :
			_collider;

		[SerializeField]
		public float StartSpeed = 1f;

		[SerializeField]
		public float EndSpeed = 1f;

		//[SerializeField]
		//public float _acceleration = 1f;

		[SerializeField]
		public float Gravity = 1.0f;

		[SerializeField]
		public AnimationCurve Curve;
		////

		[SerializeField]
		private float _speed;

		private float _animationTime = 0;

		private bool _isIdle = false;

		////

		public Action<EntityObjectBase> OnTargetHitHandler;
	}
}
