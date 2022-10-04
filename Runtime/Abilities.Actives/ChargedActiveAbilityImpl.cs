using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using System.Collections;
using UnityEngine;
//using Cirrus.Arpg.World.Entities.Actions;

namespace Cirrus.Arpg.Abilities
{

	public partial class ChargedActiveAbilityImpl
	{
		protected override ActiveAbilityImplInstanceBase _GetInstance()
		{
			return new ChargedActiveAbilityImplInstance(this);
		}
	}

	public partial class ChargedActiveAbilityImplInstance : SustainedActiveAbilityImplInstanceBase
	{
		public ChargedActiveAbilityImplInstance(ChargedActiveAbilityImpl resource) : base(resource)
		{
			_resource = resource;
			_frequencyTimeWaitForSeconds = new WaitForSeconds(resource.FrequencyTime);
		}

		//public override ActiveAbilityAssetBase AssetBase => _asset;

		//protected override AbilityBase _Clone()
		//{
		//	var instance = (ChargedActiveAbility)base._Clone();			
		//	instance.EndAction = EndAction?.DeepCopy();
		//	instance.SelfInflictedEndAction = SelfInflictedEndAction?.DeepCopy();			
		//	return instance;
		//}

		//public override void OnResourceRealized()
		//{
		//	base.OnResourceRealized();
			
		//}

		protected override IEnumerator _StartupCoroutine(CharacterObject source, EntityObjectBase target)
		{
			yield return _startupLagWaitForSeconds;

			if (_EvalOnSelf(ActiveAbilityImplEventFlags.StartupLagEnded_Self, source))
			{
				//Assert(StartAction != null);
				if (_Eval(ActiveAbilityImplEventFlags.StartupLagEnded, source, target))
					OnStartupLagEndedHandler?.Invoke();
				_StartCooldown();
			}

			yield return null;
		}


		protected override IEnumerator _SustainedCoroutine(CharacterObject source, EntityObjectBase target)
		{
			while(true)
			{
				if(IsEnded) break;

				if(Charge <= 0)
				{
					Charge = 0;
					yield return _frequencyTimeWaitForSeconds;
					continue;
				}

				if(!_EvalOnSelf(ActiveAbilityImplEventFlags.Sustained_Self, source))
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

			// TODO: Does this justify making ChargedActiveAbility a class apart?
			if(_EvalOnSelf(ActiveAbilityImplEventFlags.End_Self, source))
			{
				_Eval(ActiveAbilityImplEventFlags.End, _source, _target, 1 - Charge);
			}

			_DoEval(ActiveAbilityImplEventFlags.StartupLagEnded, _source, _target, (action, source, target) =>
			{
				action.End(source, target);
			});

			_DoEval(ActiveAbilityImplEventFlags.Sustained, _source, _target, (action, source, target) =>
			{
				action.End(source, target);
			});			

			_endLagTimer.Reset();

			_StartRecharge();

			_StartCooldown();

			return true;
		}
	}
}
