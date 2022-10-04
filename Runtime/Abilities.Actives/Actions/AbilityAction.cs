using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;

using System;

using UnityEngine;
using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Debugging;
using System;
using System.Collections;
using UnityEngine;
using static Cirrus.Debugging.DebugUtils;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using Mono.Cecil;
using static UnityEngine.GraphicsBuffer;


namespace Cirrus.Arpg.Abilities
{

	// TODO: anaction does not need to be cloned if it isa direct action (deterministic)	
	public partial class AbilityActionInstance
	{
		object ICopiable.MemberwiseCopy()
		{
			return MemberwiseClone();
		}

		public AbilityActionInstance(AbilityAction resource) : base(resource)
		{
			_resource = resource;
			//if (Trigger != null) PopulateCallbacks(Trigger);
			//if (FallbackTrigger != null) PopulateCallbacks(FallbackTrigger);
		}

		public void PopulateCallbacks(EffectorBase effector)
		{
			if(effector == null) return;
			//strategy. += _OnStrategySucceeded;
			//strategy.OnEndedHandler += _OnStrategyEnded;
		}

		protected virtual bool _EvalConditions(IConditionContext context)
		{
			if(
				context.Source != null &&
				context.Target != null &&
				_resource.Range.Contains((context.Source.Position - context.Target.Position).magnitude)
				)
			{
				if(_resource.Conditions.IsAll(cond => cond.Evaluate(context)))
				{
					return _resource.Conditions.IsAll(cond => cond.Evaluate(context));
				}
			}

			return false;
		}

		public bool Start(
			EntityObjectBase source = null,
			EntityObjectBase target = null,
			float strengthScale = 1
			)
		{
			//if(
			//	target == null ||
			//	target == source ||
			//	target.Flags.Intersects(EntityObjectFlags.Target))
			{
				Source = source;

				Target = target;

				Strength = strengthScale * _resource.Strength;

				if(Source != null && Target != null) Direction = (Target.Position - Source.Position).normalized;
				else if(Source != null) Direction = Source.Forward;
				else Direction = Vector3.forward;

				if(_EvalConditions(this))
				{
					var effectors = _resource.Effectors;
					for(int i = 0; i < effectors.Length; i++)
					{
						var effector = effectors[i];
						if(effector != null && effector.Apply(this)) return true;
					}
				}
			}

			return false;
		}

		public void _OnEffectorResult()
		{
			//if(target == null) return;

			if(_effectInsts.Count == 0)
			// TODO: do not PERSIST and instance all effects (some might be created every frame)
			{
				_resource.Effects.Foreach(effect =>
				{
					if(!effect.GetInstance(this, out IEffectInstance inst)) return;
					_effectInsts.Add(inst);
				});
			}

			_effectInsts.Foreach(effect =>
			 {
				 effect.Apply(this);
			 });

			if(_asyncEffectInsts.Count == 0)
			{
				_resource.Asyncs.Foreach(effect =>
				{
					if(!effect.GetInstance(this, out IEffectInstance inst)) return;
					//Assert(inst.IsInstance);
					inst.Apply(this);
					inst.Coroutine = Source.StartCoroutine(_OnAsyncUpdateCoroutine(inst));
					_asyncEffectInsts.Add(inst);
				});
			}
		}


		public void End(EntityObjectBase source, EntityObjectBase target)
		{
			if(Source == null) return;

			_effectInsts.Foreach(effect =>
			 {
				 effect.Unapply(this);
			 });

			_effectInsts.Clear();

			_asyncEffectInsts.Foreach(effect =>
			 {
				 if(effect.Coroutine != null)
				 {
					//Assert(effect.IsInstance);
					Source.StopCoroutine(effect.Coroutine);
					 effect.Coroutine = null;
					 effect.Unapply(this);
				 }
			 });
			_asyncEffectInsts.Clear();
		}

		public IEnumerator _OnAsyncUpdateCoroutine(IEffectInstance effect)
		{
			var waitForSeconds = new WaitForSeconds(effect.Frequency);

			while(true)
			{
				effect?.Update(this);
				yield return waitForSeconds;
			}
		}
	}
}