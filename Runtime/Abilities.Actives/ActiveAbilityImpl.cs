//using Cirrus.Objects; using Cirrus.Content;

using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class ActiveAbilityImplBase
	{
		protected abstract ActiveAbilityImplInstanceBase _GetInstance();

		protected virtual void _PopulateInstance(ActiveAbilityImplInstanceBase inst)
		{
		}

		// TODO: C# 9 subst "new hiding" with return covariant 
		// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/covariant-returns
		// https://docs.unity3d.com/2022.2/Documentation/Manual/CSharpCompiler.html
		public ActiveAbilityImplInstanceBase GetInstance()
		{
			ActiveAbilityImplInstanceBase inst = _GetInstance();
			_PopulateInstance(inst);
			return inst;
		}
	}

	public partial class ActiveAbilityImpl
	{
		protected override ActiveAbilityImplInstanceBase _GetInstance()
		{
			return new ActiveAbilityImplInstance(this);
		}
	}

	[Serializable]
	public abstract partial class ActiveAbilityImplInstanceBase
	{

		//protected override AbilityBase _Clone()
		//{
		//	var instance = (ActiveAbilityBase)MemberwiseClone();
		//	instance.Action = Action?.DeepCopy();
		//	instance.SelfInflictedStartAction = SelfInflictedStartAction?.DeepCopy();
		//	instance.SelfInflictedStartLagAction = SelfInflictedStartLagAction?.DeepCopy();
		//	instance.SelfInflictedEndLagAction = SelfInflictedEndLagAction?.DeepCopy();
		//	return instance;
		//}

		public ActiveAbilityImplInstanceBase(ActiveAbilityImplBase resource) : base(resource)
		{
			_startupLagWaitForSeconds = new WaitForSeconds(resource.StartLag);
			_cooldownTimer = new Timer(resource.CooldownTime, start: false, repeat: false, fixedUpdate: false);
			_endLagTimer = new Timer(resource.EndLag, start: false, repeat: false, fixedUpdate: false);
			//_startLagTimer = new Timer(resource.StartLag, start: false, repeat: false, fixedUpdate: false);
			_actions = resource.Actions.Select(action => new AbilityActionInstance(action)).ToArray();


			_cooldownTimer.onTimeoutHandler += _OnCooldownTimeout;
			_cooldownTimer.onTickHandler += _OnCooldownValueChanged;
			_endLagTimer.onTimeoutHandler += _OnEndlagTimeout;
		}

		public bool _Eval(
			ActiveAbilityImplEventFlags flags,
			EntityObjectBase source,
			EntityObjectBase target,
			float strength = 1
			)
		{
			for (int i = 0; i < _actions.Length; i++)
			{
				AbilityActionInstance a = _actions[i];
				if (a.Flags.Intersects(flags))
				{
					if (!a.Start(source, target, strength))
						return false;
				}
			}

			return true;
		}

		public bool _DoEval(
			ActiveAbilityImplEventFlags flags,
			EntityObjectBase source,
			EntityObjectBase target,
			Action<AbilityActionInstance, EntityObjectBase, EntityObjectBase> eval
			)
		{
			for (int i = 0; i < _actions.Length; i++)
			{
				AbilityActionInstance a = _actions[i];
				if (a.Flags.Intersects(flags))
				{
					eval(a, source, target);
				}
			}

			return true;
		}

		public bool _DoEval(
			ActiveAbilityImplEventFlags flags,
			Action<AbilityActionInstance> eval
			)
		{
			for (int i = 0; i < _actions.Length; i++)
			{
				AbilityActionInstance a = _actions[i];
				if (a.Flags.Intersects(flags))
				{
					eval(a);
				}
			}

			return true;
		}

		public bool _EvalOnSelf(ActiveAbilityImplEventFlags flags, EntityObjectBase source)
		{
			return _DoEval(flags, source, source, (action, source, target) => action.Start(source, target));
		}

		public bool _Eval(ActiveAbilityImplEventFlags flags, EntityObjectBase source, EntityObjectBase target)
		{
			return _DoEval(flags, source, target, (action, source, target) => action.Start(source, target));
		}

		public virtual bool Start(CharacterObject source, EntityObjectBase target = null)
		{
			if (_EvalOnSelf(ActiveAbilityImplEventFlags.Start_Self, source))
			{
				_source = source;
				_target = target;
				State = ActiveAbilityImplState.Active;

				// TODO _startUseCoroutine = StartCoroutine...
				source.StartCoroutine(_StartupCoroutine(source, target));

				return true;
			}

			return false;
		}

		public virtual bool End(CharacterObject source, EntityObjectBase target)
		{
			return true;
		}

		// Cooldown, Fullness
		protected void _StartCooldown()
		{
			if (!_cooldownTimer.IsActive) _cooldownTimer.Reset();
		}
		protected void _OnCooldownValueChanged(float value)
		{
			OnCooldownValueChangedHandler?.Invoke(Cooldown);
		}

		protected void _OnCooldownTimeout()
		{
			OnCooldownedHandler?.Invoke();
		}

		protected virtual void _StartEndLag()
		{
			_endLagTimer.Reset(_ActiveAbilityImpl.EndLag);
		}

		protected void _OnEndlagTimeout()
		{
			_EvalOnSelf(ActiveAbilityImplEventFlags.EndingLagEnded_Self, _source);
			State = ActiveAbilityImplState.Available;
			OnEndLagEndedHandler?.Invoke(this);
		}
	}

	public partial class ActiveAbilityImplInstance
	{
		private ActiveAbilityImpl _resource;

		protected override ActiveAbilityImplBase _ActiveAbilityImpl => _resource;

		protected override AbilityImplBase _AbilityResourceImpl => _resource;

		protected override IEnumerator _StartupCoroutine(CharacterObject source, EntityObjectBase target)
		{
			yield return _startupLagWaitForSeconds;

			if (_EvalOnSelf(ActiveAbilityImplEventFlags.StartupLagEnded_Self, source))
			{
				//Assert(StartAction != null);
				if (_Eval(ActiveAbilityImplEventFlags.StartupLagEnded, source, target))
					OnStartupLagEndedHandler?.Invoke();
				_StartEndLag();
				_StartCooldown();
			}

			yield return null;
		}

		public ActiveAbilityImplInstance(ActiveAbilityImpl resource) : base(resource)
		{
			_resource = resource;
		}
	}
}
