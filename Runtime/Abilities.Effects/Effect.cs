using Cirrus.Collections;
using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using EntityInstanceBase = Cirrus.Arpg.Entities.EntityInstanceBase;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class EffectBase
	{
		protected virtual bool _GetInstance(IEffectContext context, out IEffectInstance instance)
		{
			instance = null;
			return false;
		}

		public virtual bool GetInstance(
			IEffectContext context,
			out IEffectInstance instance
			)
		{
			// TODO: Factor in outgoing filter
			instance = null;
			if(SourceConditions.All(context.Source))
			{
				if(_GetInstance(context, out instance))
				{
					instance.SubEffectInsts = ListUtils.Create<IEffectInstance>(SubEffects.Length);
					if(context.Source != null)
					{
						foreach(var filter in context.Source.OutgoingFilters)
						{
							filter.ApplyFilter(context.Source, instance);
						}
					}
						
					for(int i = 0; i < SubEffects.Length; i++)
					{
						if (SubEffects[i].GetInstance(context, out IEffectInstance subEffect))
						{
							instance.SubEffectInsts[i] = subEffect;
						}
					}

					for (int i = 0; i < FallbackEffects.Length; i++)
					{
						if (FallbackEffects[i].GetInstance(context, out IEffectInstance fallbackEffect))
						{
							instance.FallbackEffectInsts[i] = fallbackEffect;
						}
					}

					return true;
				}
			}

			return false;
		}
	}

	public partial class NonInstancedEffect
	{
		public EffectBase Resource => this;
		public Coroutine Coroutine { get; set; }
		public List<IEffectInstance> SubEffectInsts { get => ListUtils.Empty<IEffectInstance>(); set { } }
		public List<IEffectInstance> FallbackEffectInsts { get => ListUtils.Empty<IEffectInstance>(); set { } }

		public override bool GetInstance(IEffectContext context, out IEffectInstance instance)
		{
			instance = this;
			return true;
		}

		public virtual void Consolidate(IEffectContext context, EffectBase update)
		{
		}

		public virtual bool DoApply(IEffectContext context)
		{
			return false;
		}

		public virtual bool DoUnapply(IEffectContext context)
		{
			return false;
		}

		public virtual bool DoUpdate(IEffectContext context)
		{
			return false;
		}

		public virtual void OnSustainTimeout()
		{
		}
	}

	public partial class Effect
	{		
		protected override bool _GetInstance(IEffectContext context, out IEffectInstance instance)
		{
			instance = context.Source.effectPool.Get(this);
			if(instance != null)
			{
				((EffectInstance)instance).Resource = this;
				return true;
			}
			return false;
		}
	}
}

namespace Cirrus.Arpg.Entities
{
	public partial class EntityObjectBase
	{
		private const int _EffectPoolSize = 10;

		public EffectPool<Effect, EffectInstance> effectPool =
			new EffectPool<Effect, EffectInstance>(max: _EffectPoolSize);
	}
}