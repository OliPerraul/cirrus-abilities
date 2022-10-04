
using Cirrus.Collections;
using Cirrus.Arpg.Animations;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;

using NaughtyAttributes;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
//using Event = Cirrus.Arpg.Entities.Event;
using Range_ = Cirrus.Unity.Numerics.Range_;

namespace Cirrus.Arpg.Abilities
{
	public interface IEffect
	{
		EffectBase[] SubEffects { get; }

		EffectBase[] FallbackEffects { get; }

		ConditionBase[] SourceConditions { get; }

		ConditionBase[] TargetConditions { get; }

		EffectFilter[] Filters { get; }

		float Frequency { get; }

		EffectType EffectType { get; }

		EffectFlags EffectFlags { get; }


		AnimationCurve StrengthCurve { get; }

		Range_ StrengthRange { get; }

		float ConsolidationWeight { get; }
	}

	public abstract partial class EffectBase
	: MonoBehaviourBase
	, IEffect
	, INameable
	{
		public const string EffectFoldoutName = "Effect Properties";

		[SerializeField]		
		public string _name = null;
		public string Name => _name;
		
		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private EffectBase[] _subEffects;
		public EffectBase[] SubEffects => _subEffects;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private EffectBase[] _fallbackEffects;
		public EffectBase[] FallbackEffects => _fallbackEffects;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private ConditionBase[] _sourceConditions;
		public ConditionBase[] SourceConditions => _sourceConditions;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private ConditionBase[] _targetConditions;
		public ConditionBase[] TargetConditions => _targetConditions;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private EffectFilter[] _filters;
		public EffectFilter[] Filters => _filters;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private float _frequency;
		public float Frequency => _frequency;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		private EffectType _effectType;
		public EffectType EffectType => _effectType;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		protected EffectFlags _flags;
		public EffectFlags EffectFlags => _flags;


		///////////////////////////////////////////
		///////////////////////////////////////////	

		// TODO
		[SerializeField]
		[Foldout(EffectFoldoutName)]
		public AnimationCurve _strengthCurve;
		public AnimationCurve StrengthCurve => _strengthCurve;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		public Range_ _strengthRange = new Range(1, 2);
		public Range_ StrengthRange => _strengthRange;

		[SerializeField]
		[Foldout(EffectFoldoutName)]
		public float _consolidationWeight = 1;
		public float ConsolidationWeight => _consolidationWeight;

		public virtual void OnValidate()
		{
			if(_name == null || _name == "")
			{
				_name = GetType().Name.Before("Effect").PrettifyName();
			}
		}
	}

	public partial class Effect : EffectBase
	{
	}

	public partial class NonInstancedEffect : EffectBase, IEffectInstance
	{
	}

	public interface IEffectInstance : IEffect
	{
		//EffectBase _Resource { get; }

		List<IEffectInstance> SubEffectInsts { get; set; }

		List<IEffectInstance> FallbackEffectInsts { get; set; }

		Coroutine Coroutine { get; set; }

		bool DoApply(IEffectContext context);

		bool DoUnapply(IEffectContext context);

		bool DoUpdate(IEffectContext context);

		void OnSustainTimeout();

		void Consolidate(IEffectContext context, EffectBase update);
	}

	public abstract partial class EffectInstanceBase : IEffectInstance, IEffect
	{
		public Range_ Range { get; set; }

		public abstract EffectBase Resource { get; set; }

		public List<IEffectInstance> SubEffectInsts { get; set; } = new List<IEffectInstance>();

		public List<IEffectInstance> FallbackEffectInsts { get; set; } = new List<IEffectInstance>();

		public Coroutine Coroutine { get; set; }

		public EffectBase[] SubEffects => ((IEffect)Resource).SubEffects;

		public EffectBase[] FallbackEffects => ((IEffect)Resource).FallbackEffects;

		public ConditionBase[] SourceConditions => ((IEffect)Resource).SourceConditions;

		public ConditionBase[] TargetConditions => ((IEffect)Resource).TargetConditions;

		public EffectFilter[] Filters => ((IEffect)Resource).Filters;

		public float Frequency => ((IEffect)Resource).Frequency;

		public EffectType EffectType => ((IEffect)Resource).EffectType;

		public EffectFlags EffectFlags => ((IEffect)Resource).EffectFlags;

		public AnimationCurve StrengthCurve => ((IEffect)Resource).StrengthCurve;

		public Range_ StrengthRange => ((IEffect)Resource).StrengthRange;

		public float ConsolidationWeight => ((IEffect)Resource).ConsolidationWeight;

		public virtual bool DoApply(IEffectContext context) { return false; }

		public virtual bool DoUnapply(IEffectContext context) { return false; }

		public virtual bool DoUpdate(IEffectContext context) { return false; }

		public virtual void OnSustainTimeout() { return; }

		public virtual void Consolidate(IEffectContext context, EffectBase update) { return; }
	}

	public partial class EffectInstance : EffectInstanceBase
	{
		public Effect resource;
		public override EffectBase Resource { get => resource; set => resource = (Effect)value; }

		public override bool DoApply(IEffectContext context)
		{
			return true;
		}

		public override bool DoUnapply(IEffectContext context)
		{
			return true;
		}

		public override bool DoUpdate(IEffectContext context)
		{
			return true;
		}	

		public override void Consolidate(IEffectContext context, EffectBase update)
		{
			return;
		}

		public EffectInstance()
		{
		}
	}
}
