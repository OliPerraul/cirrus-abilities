
////using Cirrus.Arpg.World.Entities.Actions;
//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.Unity.Numerics;
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
//		protected Vector3 _targetDirection = Vector3.zero;

//		public bool IsInRotation => !_direction.Approx(_targetDirection);

//		private string _debugPart = "Unknown";

//		public override string Name => $"Strategy:Tackle ({_debugPart})";

//		public override int ID => (int)CharacterStateID.Tackle;

//		private IEffectSource _source;

//		private ObjectBase _target;

//		private Timer _timer;

//		private Vector3 _initialPosition;

//		private Vector3 _withdrawPosition;

//		private bool _targetHit = false;

//		private float _distance = 0;

//		private bool _isOnHitSubscribed = false;

//		private Coroutine _tackleCoroutine;

//		private Coroutine _withdrawCoroutine;

//		public float TimeoutTime { get; set; } = 2f;

//		public float StepSpeed { get; set; } = 10f;

//		public float StepSpeedDiscount { get; set; } = .5f;

//		public float HitDuration { get; set; } = 1f;

//		public float StepDuration { get; set; } = 0.01f;

//		public float WithdrawDelay { get; set; } = 0.1f;

//		public float WithdrawStepDuration { get; set; } = .01f;

//		public float WithdrawStepSpeed { get; set; } = 10f;

//		public float WithdrawStepDiscount { get; set; } = 0.5f;
//	}
//}

