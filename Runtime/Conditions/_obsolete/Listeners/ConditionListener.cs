//using Cirrus.Arpg.Entities;
//using Cirrus.Arpg.Entities.Characters;

//namespace Cirrus.Arpg.Conditions
//{
//	// TODO make use of flags
//	// Once all flags are set, the condition is met

//	public class ObjectConditionListener : Listener
//	{
//		private ConditionBase _condition;

//		private CharacterObject _character;

//		private Trilean _satisfied = Trilean.Unknown;

//		private IEvent _state;

//		private EntityObjectBase _target;

//		public ObjectConditionListener(EntityObjectBase target, ConditionBase condition)
//		{
//			_target = target;

//			_condition = condition;

//			_SubscribeToTarget();
//		}

//		private void _SubscribeToTarget()
//		{
//			_state = _condition.GetConditionedState(_target);

//			if(_state != null)
//			{
//				_state.OnOccurredHandler += OnConditionStateUpdated;
//			}
//		}


//		public void UnsubscribeFromTarget()
//		{
//			if(_state != null)
//			{
//				_state.OnOccurredHandler -= OnConditionStateUpdated;
//			}
//		}

//		public void OnConditionStateUpdated(IEvent state)
//		{
//			Trilean previouslySatisfied = _satisfied;
//			_satisfied = _target.EvaluateCondition(_condition) ? Trilean.True : Trilean.False;

//			if(previouslySatisfied != _satisfied)
//			{
//				OnObjectListenedHandler?.Invoke(_target);
//			}
//		}
//	}


//	public class ConditionListener : Listener
//	{
//		private IEventContext _target;

//		private ConditionBase _condition;

//		private CharacterObject _character;

//		private bool _isSatisfied = false;

//		private IEvent _state;


//		public ConditionListener(
//			IEventContext target,
//			ConditionBase condition)
//		{
//			_target = target;
//			_condition = condition;
//			_SubscribeToTarget();

//			//if(_target == null) return;
//			//if(_source == null) return;

//			//AddStopOnDestroyed(_source);
//			//AddStopOnDestroyed(_target);
//			//if(start) Start();
//		}

//		private void _SubscribeToTarget()
//		{
//			_state = _target.GetEvent(_condition);

//			if(_state != null)
//			{
//				_state.OnOccurredHandler += OnConditionStateUpdated;
//			}
//		}


//		public void UnsubscribeFromTarget()
//		{
//			if(_state != null)
//			{
//				_state.OnOccurredHandler -= OnConditionStateUpdated;
//			}
//		}

//		public void OnConditionStateUpdated(IEvent state)
//		{
//			bool prev = _isSatisfied;
//			//_isSatisfied = _target.EvaluateCondition(_target, _condition);

//			if(prev != _isSatisfied)
//			{
//				OnListenedHandler?.Invoke(state);
//			}
//		}
//	}

//	// TODO scheduled condition listener

//}