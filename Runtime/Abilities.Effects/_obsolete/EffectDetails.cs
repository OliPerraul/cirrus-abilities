
//using Cirrus.Collections;
//using Cirrus.Arpg.Conditions;
//using Cirrus.Arpg.Entities;
//using Cirrus.Unity.Editor;
//using Cirrus.Unity.Objects;
//using System;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;
////using Event = Cirrus.Arpg.Entities.Event;
//using Range_ = Cirrus.Unity.Numerics.Range_;
//using ObjectUtils = Cirrus.Objects.ObjectUtils;

//namespace Cirrus.Arpg.Abilities
//{
//	public interface IEffectDetails
//	{
//		EffectBase[] SubEffects { get; }

//		EffectBase[] FallbackEffects { get; }

//		ConditionBase[] SourceConditions { get; }

//		ConditionBase[] TargetConditions { get; }

//		EffectFilter[] Filters { get; }
//		float Frequency { get; }

//		EffectType EffectType { get; }

//		EffectFlags EffectFlags { get; }

//		AnimationCurve StrengthCurve { get; }

//		Range_ StrengthRange { get; }

//		float ConsolidationWeight { get; }
//	}

//	public abstract partial class EffectDetailsMonoBehaviourBase
//	: MonoBehaviourBase
//	, IEffectDetails
//	{
//		public abstract EffectBase[] SubEffects { get; }

//		public abstract EffectBase[] FallbackEffects { get; }

//		public abstract ConditionBase[] SourceConditions { get; }

//		public abstract ConditionBase[] TargetConditions { get; }

//		public abstract EffectFilter[] Filters { get; }

//		public abstract float Frequency { get; }

//		public abstract EffectType EffectType { get; }

//		public abstract EffectFlags EffectFlags { get; }

//		public abstract AnimationCurve StrengthCurve { get; }

//		public abstract Range_ StrengthRange { get; }

//		public abstract float ConsolidationWeight { get; }
//	}

//	public partial class EffectDetails
//	: EffectDetailsMonoBehaviourBase
//	, IEffectDetails
//	{
//#if UNITY_EDITOR
//		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Effect Details", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
//		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<EffectDetails>();
//#endif

//		[SerializeField]
//		//[SerializeEmbedded]
//		private EffectBase[] _subEffects;
//		public override EffectBase[] SubEffects => _subEffects;

//		[SerializeField]
//		//[SerializeEmbedded]
//		private EffectBase[] _fallbackEffects;
//		public override EffectBase[] FallbackEffects => _fallbackEffects;

//		[SerializeField]
//		//[SerializeEmbedded]
//		private ConditionBase[] _sourceConditions;
//		public override ConditionBase[] SourceConditions => _sourceConditions;

//		[SerializeField]
//		//[SerializeEmbedded]
//		private ConditionBase[] _targetConditions;
//		public override ConditionBase[] TargetConditions => _targetConditions;

//		[SerializeField]
//		//[SerializeEmbedded]
//		private EffectFilter[] _filters;
//		public override EffectFilter[] Filters => _filters;

//		[SerializeField]
//		private float _frequency;
//		public override float Frequency => _frequency;

//		[SerializeField]
//		private EffectType _effectType;
//		public override EffectType EffectType => _effectType;

//		[SerializeField]
//		protected EffectFlags _flags;
//		public override EffectFlags EffectFlags => _flags;


//		///////////////////////////////////////////
//		///////////////////////////////////////////	

//		// TODO
//		[SerializeField]
//		public AnimationCurve _strengthCurve;
//		public override AnimationCurve StrengthCurve => _strengthCurve;

//		[SerializeField]
//		public Range_ _strengthRange = new Range(1, 2);
//		public override Range_ StrengthRange => _strengthRange;

//		[SerializeField]
//		public float _consolidationWeight = 1;
//		public override float ConsolidationWeight => _consolidationWeight;
//	}
//}
