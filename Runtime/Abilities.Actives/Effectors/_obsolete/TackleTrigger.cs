//using Cirrus.Unity.Objects;
////using Cirrus.Arpg.World.Entities.Actions;
//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.Arpg.Entities.Characters.Controls;
//using Cirrus.Numerics;
//using Cirrus.States;
//using Cirrus.Unity.Numerics;
//using System.Collections;
//using UnityEngine;

//namespace Cirrus.Arpg.Abilities
//{
//	// TODO: derive from dash

//	// Make into a State
//	// TrySetState(Strategy)

//	//[field: SerializeField]
//	//[field: SerializeProperty]
//	//[field: Rename((nameof(StepSpeed)))]

//	public partial class TODO_TackleStrategy : StrategyState, ICharacterState
//	{
//		public CharacterComponent CharacterObject => Character;

//		public CharacterControl Control => CharacterObject.Control;


//		public TODO_TackleStrategy() : base()
//		{
//			_timer = new Timer(_OnTimeOut);
//		}

//		// We dont care whihc type of actor
//		// TODO: unsubscribe on action finished as well
//		public override bool Start(IEffectSource source, ObjectBase target)
//		{
//			if(
//				target != null &&
//				source == target)
//			{
//				return false;
//			}

//			_source = source;
//			// TODO push and set state in one method
//			//if(
//			//	//CharacterObject.StateMachine.SetState(this, target).Succeeded() &&
//			//	CharacterObject.StateMachine.SetState(this).Succeeded() &&
//			//	CharacterObject.StateMachine.State != this)
//			//{
//			//	//CharacterObject.StateMachine.SetState(ID, target);
//			//	CharacterObject.StateMachine.SetState(ID);
//			//	return true;
//			//}

//			return false;
//		}

//		public override void CustomUpdate2(float deltaTime)
//		{
//			if (!MathUtils.Approx(Control.Axes.Right.magnitude, 0))
//			{
//				// Smoothly interpolate from current to target look direction				
//				_targetDirection = new Vector3(
//					Control.Axes.Right.x,
//					0.0f,
//					Control.Axes.Right.y);

//				_direction = Vector3.Lerp(
//					_direction,
//					_targetDirection,
//					CharacterObject.Kinematics.RotationLerp).normalized;

//				if (_direction.magnitude > 0.01f)
//				{
//					Control.Rotation =
//						Quaternion.LookRotation(_direction, CharacterObject.Transform.up);
//				}
//			}
//		}

//		public override StateMachineStatus Enter()
//		{
//			_Reset();
//			_initialPosition = CharacterObject.Position;

//			// TODO FIX ME!
//			var args = new object[10];

//			if (args.Length == 1)
//			{
//				_target = (ObjectBase)args[0];
//				_direction = _target.Component.Position - CharacterObject.Position;
//				_distance = _direction.magnitude;
//			}
//			else
//			{
//				_target = null;
//				_direction = CharacterObject.Transform.forward;
//				_distance = Range.Max;
//			}


//			_direction = _direction.normalized;
//			_timer.Reset(TimeoutTime);

//			_targetHit = false;
//			_isOnHitSubscribed = false;
//			_SubscribeOnHit();
//			_tackleCoroutine = CharacterObject.StartCoroutine(_TackleCoroutine());

//			return StateMachineStatus.Success;
//		}

//		public override void OnDrawGizmos_()
//		{
//			base.OnDrawGizmos_();

//			Gizmos.color = Color.blue;
//			Gizmos.DrawSphere(_withdrawPosition, .2f);

//			Gizmos.DrawRay(CharacterObject.Position + Vector3.up, _direction * 4);
//		}

//		public override StateMachineStatus Exit()
//		{
//			base.Exit();

//			_Reset();

//			return StateMachineStatus.Success;
//		}

//		private void _Reset()
//		{
//			_distance = 0;
//			_targetHit = false;

//			if (_timer.IsActive)
//			{
//				_timer.Stop();
//				_SubscribeOnHit(false);
//				if (_tackleCoroutine != null) CharacterObject.StopCoroutine(_tackleCoroutine);
//				_tackleCoroutine = null;
//				if (_withdrawCoroutine != null) CharacterObject.StopCoroutine(_withdrawCoroutine);
//				_withdrawCoroutine = null;

//				CharacterObject.Phys.ActionVelocity = Vector3.zero;
//			}
//		}

//		protected void _Stop()
//		{
//			_Reset();
//			_source._OnStrategyEnded(this);
//			CharacterObject.Bt.Flags &= ~CharacterBtFlags.Active_Strategy;
//		}

//		private void _OnTimeOut() => _Stop();

//		private void _SubscribeOnHit(bool subscribe = true)
//		{
//			if (subscribe)
//			{
//				if (!_isOnHitSubscribed)
//				{
//					_isOnHitSubscribed = true;
//					// TODO
//					//CharacterObject.OnObjectCollidedHandler += _OnHit;
//				}
//			}
//			else
//			{
//				_isOnHitSubscribed = false;
//				//CharacterObject.OnObjectCollidedHandler -= _OnHit;
//			}
//		}

//		private IEnumerator _TackleCoroutine()
//		{			
//			while ((_initialPosition - CharacterObject.Position).magnitude < _distance)
//			{				
//				if (_targetHit) break;

//				CharacterObject.Phys.ActionVelocity = _direction * StepSpeed;

//				yield return new WaitForSeconds(StepDuration);
//			}

//			if (
//				_target != null &&
//				(_target.Component.Position - CharacterObject.Position).magnitude < Range.Min)
//			{
//				_StartWithdraw();
//			}
//			else _Stop();
//		}

//		private void _StopTackle()
//		{
//			if (_tackleCoroutine != null) CharacterObject.StopCoroutine(_tackleCoroutine);
//			_tackleCoroutine = null;

//			CharacterObject.Phys.ActionVelocity = Vector3.zero;
//		}

//		private void _StartWithdraw()
//		{			
//			_withdrawCoroutine = CharacterObject.StartCoroutine(_WithdrawCoroutine());
//		}


//		private IEnumerator _WithdrawCoroutine()
//		{
//			_withdrawPosition = CharacterObject.Position;

//			while ((_withdrawPosition - CharacterObject.Position).magnitude < Range.Min)
//			{				
//				// TODO instead of withdrawing to initial
//				// withdraw behind
//				var dir = (_initialPosition - CharacterObject.Position).normalized;

//				// If the direction actually puts us closer to the position we want to withdraw from
//				// invert the direction
//				if ((CharacterObject.Position + dir - _withdrawPosition).magnitude <
//					(CharacterObject.Position - _withdrawPosition).magnitude)
//				{
//					dir = -dir;
//					//_direction = dir;
//				}
//				//else _direction = -dir;

//				CharacterObject.Phys.ActionVelocity = dir * WithdrawStepSpeed;
//				//Character.CharacterObjectInstance.Ax

//				yield return new WaitForSeconds(StepDuration);
//			}			
//			_Stop();
//		}

//		private void _OnHit(ObjectComponentBase target)
//		{
//			Control.Axes.Left = Vector3.zero;
//			_targetHit = true;
//			_SubscribeOnHit(false);
//			_source._OnStrategySucceeded(this, _source, target.Resource);
//			_StopTackle();
//			if (
//				_target != null &&
//				(_target.Component.Position - CharacterObject.Position).magnitude < Range.Min)
//			{
//				_StartWithdraw();
//			}
//			else _Stop();

//			// TODO cache target here 
//			// to get target to withdraw from
//			//_strategy.OnTargetHitFromCharacterHandler?.Invoke(Character, target);
//		}
//	}
//}