//using UnityEngine.InputSystem;
using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Numerics;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using System.Collections.Generic;
using System.Linq;
using Cirrus.Collections;
using Object = UnityEngine.Object;
// TODO
// begin duration
// e.g wait until you die until buff is applied
// e.g curse
// e.g hitby fire to get boost in damage

// end duration

//using Cirrus.Arpg.Actions.Modifiers.Cirrus.Arpg.World.Entities.Actions.Modifiers;

// TODO : For stack comparison (determien which is the highest)

// STRENGTH VALUE OF THE MODIFICATION


// TODO self inflicted action during events

namespace Cirrus.Arpg.Abilities
{
	public partial class PassiveAbilityImpl
	{
		protected virtual PassiveAbilityImplInstance _GetInstance()
		{
			return new PassiveAbilityImplInstance(this);
		}

		protected virtual void _PopulateInstance(PassiveAbilityImplInstance inst)
		{
		}

		// TODO: C# 9 subst "new hiding" with return covariant 
		// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/covariant-returns
		// https://docs.unity3d.com/2022.2/Documentation/Manual/CSharpCompiler.html
		public PassiveAbilityImplInstance GetInstance()
		{
			PassiveAbilityImplInstance inst = _GetInstance();
			_PopulateInstance(inst);
			return inst;
		}

	}

	// Treatment effect should be here and filtered to represent treatment efficacy

	// TODO: update effects at interval (using Effect.Update)
	public partial class PassiveAbilityImplInstance
	{
		public static implicit operator PassiveAbilityImpl(PassiveAbilityImplInstance inst) => inst._resource;

		public PassiveAbilityImplInstance(PassiveAbilityImpl resource)
		{
			_resource = resource;

			// NOTE: initialize duration here, since modifier decorator might overwrite the duration
			// TODO: should we fix this?			
			Duration = _resource.Duration.CreateInstance();
			// TODO: apply Effects update here..
			Duration.OnRemainingHandler += _OnDurationRemaining;
			Duration.OnEndedHandler += _OnDurationEnded;
			if (_resource.Frequency > 0) _frequencyTimer = new Timer(_OnFrequencyUpdate, true);

			EffectInstances = ListUtils.Create<IEffectInstance>(resource.Effects.Count);
			for (int i = 0; i < resource.Effects.Count; i++)
			{
				var effect = resource.Effects[i];
				if(effect == null) continue;
				if(effect.GetInstance(this, out IEffectInstance inst))
				{
					EffectInstances[i] = inst;
				}
			}
		}

		//public Mod(DurationAssetBase duration, params EffectAssetBase[] effects)
		//{
		//	Effects = effects;
		//	Duration = duration;
		//}

		public void _OnDurationRemaining(DurationInstanceBase duration, float time, float delta)
		{
			OnRemainingDurationHandler?.Invoke(time, delta);
		}

		protected virtual void _OnDurationEnded(DurationInstanceBase duration)
		{
			OnImplDurationEndedHandler?.Invoke();
			//if(Detach(_target))
			//{
			//}
		}


		object ICopiable.MemberwiseCopy()
		{
			return MemberwiseClone();
		}

		//		object ICloneable.Clone()
		//		{
		//			var modifier = (Modifier)MemberwiseClone();
		//#if UNITY_EDITOR
		//			if(Duration != null) Duration.__Modifier = modifier;
		//#endif
		//			modifier.Duration = Duration?.DeepCopy();

		//			modifier.Effects = new List<EffectBase>().Resize(Effects.Count);			
		//			for(int i = 0; i < Effects.Count; i++)
		//			{
		//				modifier.Effects[i] = Effects[i]?.DeepCopy();
		//				//modifier.OnRemainingDurationHandler += modifier.Effects[i].OnRemainingDuration;				
		//			}
		//			return modifier;
		//		}

		//public override void OnResourceRealized()
		//{
		//	base.OnResourceRealized();
		//	// NOTE: initialize duration here, since modifier decorator might overwrite the duration
		//	// TODO: should we fix this?			
		//	Duration.Current = Duration.Total;
		//	// TODO: apply Effects update here..
		//	Duration.OnRemainingHandler += _OnDurationRemaining;
		//	Duration.OnEndedHandler += _OnDurationEnded;
		//	if (Frequency > 0) _frequencyTimer = new Timer(_OnFrequencyUpdate, true);
		//}

		public void _OnDurationRemaining(DurationBase duration, float time, float delta)
		{
			OnRemainingDurationHandler?.Invoke(time, delta);
		}

		public bool GetInteracts(
			IPassiveAbility target
			, out List<PassiveAbilityInteraction> interactions
			)
		{
			List<PassiveAbilityInteraction> list = new List<PassiveAbilityInteraction>();
			interactions = list;

			for(int i = 0; i < _resource.Interacts.Count; i++)
			{
				PassiveAbilityInteraction interaction = _resource.Interacts[i];
				if (interaction == null) continue;

				if (interaction.Targets.Intersects((Object)target))
				{
					list.Add(interaction);
				}
			}

			return list.Count != 0;
		}

		public virtual bool DoApply(EntityObjectBase target)
		{
			Duration.Start(target);

			((IEntityContext)this).Target = target;

			((IEffectContext)this).Strength = _resource.PassiveAbilityFlags.Intersects(PassiveAbilityFlags.StrengthScaled) ?
				(1 - Duration.Ratio) * _resource.Strength :
				_resource.Strength;

			for (int i = 0; i < EffectInstances.Count; i++)
			{
				EffectInstances[i].Apply(this);
			}

			if (_resource.Frequency > 0) _frequencyTimer.Reset(_resource.Frequency.Random());

			return true;
		}

		public virtual bool DoUnapply(EntityObjectBase target)
		{
			Duration.Stop(target);

			for(int i = 0; i < EffectInstances.Count; i++) EffectInstances[i].Unapply(this);

			if (_resource.Frequency > 0) _frequencyTimer.Stop();

			((IEntityContext)this).Target = null;

			return true;
		}

		protected void _OnFrequencyUpdate()
		{
			for (int i = 0; i < EffectInstances.Count; i++) EffectInstances[i].Update(this);
		}


		public void _OnEffectorResult()
		{
		}

		public void _OnTriggerEnded(EffectorBase strat)
		{
		}

		public virtual PassiveAbilityUpdateFlags DoUpdateDuration(
			EntityObjectBase entity,
			PassiveAbilityDelta update
			)
		{
			PassiveAbilityUpdateFlags flags = PassiveAbilityUpdateFlags.Duration;
			if(Duration
				.Evaluate(update.operation)
				.Almost(0, 0.01f)
				)
			{
				flags |= PassiveAbilityUpdateFlags.Removal;
			}

			return flags;
		}

		// TODO : Can we increase modifications without increasing the duration??
		public bool DoConsolidate(
			EntityObjectBase target,
			IPassiveAbilityInstance update)
		{
			if(
				Duration.GetType() != ((IPassiveAbilityImplInstance)update).Duration.GetType() ||
				EffectInstances.Count != update.EffectInstances.Count
				)
			{
				return false;
			}

			//float incumbentScore = Duration.ConsolidationWeight * Duration.Total;
			//float updateScore = update.Duration.ConsolidationWeight * update.Duration.Total;

			//for(int i = 0; i < Effects.Count; i++)
			//{
			//	incumbentScore += Effects[i].ConsolidationWeight * Effects[i].Strength;
			//	updateScore += update.Effects[i].ConsolidationWeight * update.Effects[i].Strength;
			//}

			//if(incumbentScore < updateScore)
			//{
			//	TODO: event handler
			//	Duration.Total = update.Duration.Total;
			//}

			return true;
		}

		protected virtual void _OnDurationEnded(DurationBase duration)
		{
			OnImplDurationEndedHandler?.Invoke();
			//if(Detach(_target))
			//{
			//}
		}
	}
}


