////using Cirrus.Arpg.World.Entities.Actions.Strategies;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Animations;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	[System.Serializable]
	public enum CollisionMode
	{
		Once,
		OncePerObject,
		Multiple,
	}

	public partial class AbilityHitbox : ComponentBase
	{
		[SerializeField]
		private CollisionMode _mode = CollisionMode.Once;

		private EntityObjectBase _source = null;
		private EntityObjectBase _Source =>
			_source == null ?
			_source = GetComponentInParent<EntityObjectBase>() :
			_source;

		private IEffectContext _context;
		public IEffectContext Context => _context;

		public Action<AbilityHitbox, EntityObjectBase> OnTargetHitHandler;

		// TODO replace with on animation finished/timeout
		public Action<AbilityHitbox> OnAnimationFinishedHandler;

		[SerializeField]
		private ObjectWrapper<Collider> _collider = null;
		public Collider Collider => _collider == null ?
			_collider = GetComponentInChildren<Collider>() :
			_collider;


		private ObjectWrapper<Animator> _animator = null;
		public Animator Animator => _animator == null ?
			_animator = GetComponent<Animator>() :
			_animator;

		// TODO replace with animation timer
		private Timer _animationTimer = null;

		[SerializeField]
		private AnimatorWrapperBase _animatorWrapper;
		public AnimatorWrapperBase AnimatorWrapper
		{
			get
			{
				return _animatorWrapper;
			}
			set
			{
				_animatorWrapper = value;
				if(_animatorWrapper == null) return;
				_animatorWrapper.Animator = Animator;
			}
		}

		private HashSet<EntityObjectBase> _entered = new HashSet<EntityObjectBase>();
	}
}
