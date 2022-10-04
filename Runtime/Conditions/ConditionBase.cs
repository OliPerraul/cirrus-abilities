using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
#if UNITY_EDITOR
using Cirrus.Unity.Editor;
using UnityEditor;
#endif
using Cirrus.Unity.Objects;
using Cirrus.Unity.Randomness;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cirrus.Objects;
using Cirrus.Events;

namespace Cirrus.Arpg.Conditions
{
	// TODO: in the event a condition is made on non, EntityComponentBase
	// then we should have an interfaces that refers to a EntityComponentBase ptr ent etc..

	public interface IConditional : IConditionalOnTarget
	{
		ConditionBase[] SourceConditions { get; }
	}

	public interface IConditionalOnTarget
	{
		ConditionBase[] TargetConditions { get; set; }
	}


	public interface ICondition
	{
		bool Evaluate(IConditionContext context);
	}

	public abstract class ConditionBase 
	: MonoBehaviourBase
	, INameable
	{
		[SerializeField]
		private string _name;
		public string Name => _name;

		public virtual bool Evaluate(IConditionContext context)
		{
			return false;
		}

		//public virtual EventListener GetEventListener(IEventContext provider)
		//{
		//	return null;
		//}
	}

	public class Condition : ConditionBase
	{
#if UNITY_EDITOR

		//[MenuItem("Assets/Create/Cirrus.Arpg/Conditions/Condition", false, priority = GameUtils.MenuItemAssetConditionPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<Condition>();

#endif

		public FuncAsset<IConditionContext, bool> EvalCb = null;

		//public FuncAsset<IEventContext, IEvent> GetConditionedStateCb;

		public Condition()
		{ 
		}		


		//public Condition(FuncAsset<IEventContext, IEvent> getStateCb, FuncAsset<IConditionContext, bool> evalCb)
		//{
		//	EvalCb = evalCb;
		//	GetConditionedStateCb = getStateCb;
		//}

		public Condition(FuncAsset<IConditionContext, bool> evalCb)
		{
			EvalCb = evalCb;
		}


		public override bool Evaluate(IConditionContext context)
		{
			return EvalCb.Invoke(context);
		}

		//public override IEvent GetEventListener(IEventContext provider)
		//{
		//	return GetConditionedStateCb.Invoke(provider);
		//}

	}

	public abstract class ObjectConditionBase : ConditionBase
	{
		private ConditionBase _condition;

		protected virtual EntityObjectBase InternalSource { get; }

		public ObjectConditionBase(ConditionBase condition)
		{
			_condition = condition;
		}

		//public override IEvent GetEventListener(IEventContext provider)
		//{
		//	return _condition.GetEventListener(provider);
		//}

		public override bool Evaluate(IConditionContext context)
		{
			return _condition.Evaluate(context);
		}

	}

	public class CharacterCondition : ObjectConditionBase
	{
		protected IConditionContext _context;

		public CharacterCondition(IConditionContext context, ConditionBase condition) : base(condition)
		{
			_context = context;
		}
	}

	public class RandomCondition : ConditionBase
	{
		private Chance _Chance { get; set; } = new Chance();

		public RandomCondition(float value)
		{
			_Chance = new Chance(value);
		}

		public override bool Evaluate(IConditionContext context)
		{
			return _Chance.Result;
		}
	}
}
