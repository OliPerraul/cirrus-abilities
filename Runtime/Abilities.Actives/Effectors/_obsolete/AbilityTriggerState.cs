//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.States;
//using Cirrus.States.AI;
//using System;

//namespace Cirrus.Arpg.Abilities
//{
//	public partial class StrategyState : StrategyBase, ICharacterState
//	{
//		public CharacterStateMachine Context { get; set; }
//		public virtual StateMachineState Locomotion { get; set; } = null;

//		public virtual StateMachineState Rotation { get; set; } = null;
//		public virtual void OnMachineDestroyed() { }

//		// TODO : extension function enter, if stale
//		// simply call Next
//		public virtual StateMachineStatus Enter() { return StateMachineStatus.Success; }

//		public virtual StateMachineStatus SetNextState()
//		{ 
//			return StateMachineStatus.Failure;
//		}

//		public StateMachineStatus Reenter() { return StateMachineStatus.Success; }

//		public virtual StateMachineStatus Exit() { return StateMachineStatus.Success; }


//		public virtual void Update_() { }

//		public void FixedUpdate_() { }

//		public void LateUpdate_() { }

//		public virtual void OnDrawGizmos_() { }

//		public StateMachineStatus RemoveSubState()
//		{
//			return StateMachineStatus.Failure;
//		}

//		public StateMachineStatus __OBSOLETE_SetSubState(int state, Func<IState, bool> pred = null)
//		{
//			return StateMachineStatus.Failure;
//		}

//		public StateMachineStatus __OBSOLETE_SetSubState(IState state, Func<IState, bool> pred = null)
//		{
//			return StateMachineStatus.Failure;
//		}

//		public virtual void CustomUpdate1(float deltaTime)
//		{

//		}

//		public virtual void CustomUpdate2(float deltaTime)
//		{

//		}

//		public virtual void CustomUpdate3(float deltaTime)
//		{

//		}
//	}
//}
