using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Numerics;
using System.Collections;
using UnityEngine;

//using Cirrus.Arpg.World.Entities.Actions;

namespace Cirrus.Arpg.Abilities
{
	// TODO: Cooldown should only be part of the charged region

	// https://www.reddit.com/r/dragonage/comments/2mwctn/is_anyone_else_really_missing_sustained_abilities/ https://www.google.com/search?sxsrf=ALeKk00rObXHKJwOb8mTK9Nh2hYy-5xuyw:1618893099859&q=Sustained+Active+Ability&spell=1&sa=X&ved=2ahUKEwjmtZXh_ovwAhXJXM0KHRBACgEQBSgAegQIAhAw&biw=1536&bih=662
	//////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	/////////////////////////////////////////////////////////////////////
	///
	public abstract partial class SustainedActiveAbilityImplBase
	{
	}

	public partial class SustainedActiveAbilityImpl
	{
		protected override ActiveAbilityImplInstanceBase _GetInstance()
		{
			return new SustainedActiveAbilityImplInstance(this);
		}
	}	

	public abstract partial class SustainedActiveAbilityImplInstanceBase
	{
		private WaitForSeconds _waitForSeconds = null;

		private WaitForSeconds _frequencyTimeWaitForSeconds = null;

		//protected override AbilityBase _Clone()
		//{
		//	var instance = (SustainedActiveAbility)base._Clone();

		// instance.SustainedAction = SustainedAction?.DeepCopy();

		//	instance.SelfInflictedSustainedAction = SelfInflictedSustainedAction?.DeepCopy();
		//	instance.SelfInflictedOnEmptyAction = SelfInflictedOnEmptyAction?.DeepCopy();
		//	return instance;
		//}

		public SustainedActiveAbilityImplInstanceBase(SustainedActiveAbilityImplBase resource) : base(resource)
		{
			//base.OnResourceRealized();

			_SustainedActiveAbilityImpl = resource;

			_waitForSeconds = new WaitForSeconds(resource.StartLag);

			_frequencyTimeWaitForSeconds = new WaitForSeconds(resource.FrequencyTime);

			_startUseCoroutine = null;

			_sustainedCoroutine = null;

			_rechargeTimer = new Timer(resource.RechargeTime, repeat: false, start: false);

			_rechargeTimer.onTickHandler += _OnRechargeTimerTicked;

			Charge = resource.ChargeTime;
		}


		// TODO items ? NO refil ability
		protected void _StartRecharge()
		{
			if(_SustainedActiveAbilityImpl.RechargeTime.Almost(0))
			{
				Charge = _SustainedActiveAbilityImpl.ChargeTime;
			}
			else
			{
				_baseTime = _charge;
				_remainingTime = (_SustainedActiveAbilityImpl.ChargeTime - _baseTime);
				_remainingRechargeTime = (_remainingTime / _SustainedActiveAbilityImpl.ChargeTime) * _SustainedActiveAbilityImpl.RechargeTime;
				_rechargeTimer.Reset(_remainingRechargeTime);
			}
		}

		protected void _OnRechargeTimerTicked(float value)
		{
			float val = Mathf.Clamp(
				_baseTime + (_rechargeTimer.Time / _remainingRechargeTime) * _remainingTime,
				0,
				_SustainedActiveAbilityImpl.ChargeTime
				);

			Charge = val;
		}

		public override bool Start(CharacterObject source, EntityObjectBase target = null)
		{
			if(_EvalOnSelf(ActiveAbilityImplEventFlags.Start_Self, source))
			{
				_source = source;
				_target = target;
				_startUseCoroutine = source.StartCoroutine(_StartupCoroutine(source, target));

				return true;
			}

			return false;
		}

		protected override IEnumerator _StartupCoroutine(CharacterObject source, EntityObjectBase target)
		{
			yield return _waitForSeconds;

			if(_EvalOnSelf(ActiveAbilityImplEventFlags.StartupLagEnded_Self, source))
			{
				IsEnded = false;

				_rechargeTimer.Stop();

				OnStartupLagEndedHandler?.Invoke();

				if(_Eval(ActiveAbilityImplEventFlags.StartupLagEnded, source, target))
				{
					_sustainedCoroutine = source.StartCoroutine(
						_SustainedCoroutine(source, target));
				}
				else
				{
					End(_source, _target);
				}
			}

			yield return null;
		}

		protected virtual IEnumerator _SustainedCoroutine(CharacterObject source, EntityObjectBase target)
		{
			while(true)
			{
				if(IsEnded) break;

				if(Charge <= 0)
				{
					End(_source, _target);
					OnChargeEmptyHandler?.Invoke();
					break;
				}

				if (!_EvalOnSelf(ActiveAbilityImplEventFlags.Sustained_Self, source))
				{
					End(_source, _target);
					break;
				}

				_Eval(ActiveAbilityImplEventFlags.Sustained, source, target);

				Charge -= _SustainedActiveAbilityImpl.FrequencyTime;
				yield return _frequencyTimeWaitForSeconds;
			}

			yield return null;
		}

		public override bool End(CharacterObject source, EntityObjectBase target)
		{
			if(IsEnded) return false;

			IsEnded = true;

			if(_startUseCoroutine != null)
			{
				_source.StopCoroutine(_startUseCoroutine);
				_startUseCoroutine = null;
			}

			if(_sustainedCoroutine != null)
			{
				_source.StopCoroutine(_sustainedCoroutine);
				_sustainedCoroutine = null;
			}

			_DoEval(ActiveAbilityImplEventFlags.StartupLagEnded | ActiveAbilityImplEventFlags.Sustained, 
			action =>
			{
				action.End(_source, _target);
			});

			_StartEndLag();

			_StartRecharge();

			_StartCooldown();

			return true;
		}
	}
}