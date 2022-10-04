using Cirrus.Collections;
using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Entities;
using System;
using System.Collections.Generic;

namespace Cirrus.Arpg.Conditions
{
	public static class ConditionUtils
	{
		public static readonly IEnumerable<ConditionBase> Empty = ArrayUtils.Empty<ConditionBase>();

		//public static readonly ConditionBase False = new _FALSECondition();

		//public static readonly ConditionBase True = new _TRUECondition();

		//public static readonly ConditionBase LookAt = new LookAtCondition();

		//private class _TRUECondition : ConditionBase
		//{
		//	public override bool Evaluate(EntityComponentBase first, EntityComponentBase target)
		//	{
		//		return true;
		//	}
		//}

		//private class _FALSECondition : ConditionBase
		//{
		//	public override bool Evaluate(EntityComponentBase first, EntityComponentBase target)
		//	{
		//		return false;
		//	}
		//}

		//private class _ORCondition : ConditionBase
		//{
		//	public ConditionBase[] Conditions { get; set; } = ArrayUtils.Empty<ConditionBase>();

		//	public _ORCondition(params ConditionBase[] conditions)
		//	{
		//		Conditions = conditions;
		//	}

		//	public override bool Evaluate(EntityComponentBase first, EntityComponentBase target)
		//	{
		//		for(int i = 0; i < Conditions.Length; i++)
		//		{
		//			var cond = Conditions[i];
		//			if(cond.Evaluate(first, target)) return true;
		//		}

		//		return false;
		//	}
		//}

		//private class _ANDCondition : ConditionBase
		//{
		//	public ConditionBase[] Conditions { get; set; } = ArrayUtils.Empty<ConditionBase>();

		//	public _ANDCondition(params ConditionBase[] conditions)
		//	{
		//		Conditions = conditions;
		//	}

		//	public override bool Evaluate(EntityComponentBase first, EntityComponentBase target)
		//	{
		//		for(int i = 0; i < Conditions.Length; i++)
		//		{
		//			var cond = Conditions[i];
		//			if(cond.Evaluate(first, target)) return true;
		//		}

		//		return false;
		//	}
		//}

		//private class _NOTCondition : ConditionBase
		//{
		//	public ConditionBase Condition { get; set; }

		//	public _NOTCondition(ConditionBase condition)
		//	{
		//		this.Condition = condition;
		//	}

		//	public override bool Evaluate(IConditionContext context)
		//	{
		//		return !Condition.Evaluate(first, target);
		//	}

		//	public override IConditionedState GetConditionedState(EntityComponentBase target)
		//	{
		//		return target.GetConditionedState(Condition);
		//	}

		//}

		//public static ConditionBase Not(this ConditionBase cond)
		//{
		//	return new _NOTCondition(cond);
		//}

		//// TODO : using static utils..
		//public static ConditionBase Any(this ConditionBase[] conds)
		//{
		//	return new _ORCondition(conds);
		//}

		//public static ConditionBase All(this ConditionBase[] conds)
		//{
		//	return new _ANDCondition(conds);
		//}

		//public static bool All(			
		//this List_<Effect.Condition> conds,
		//Effect eff,
		//ObjectInstanceBase first,
		//ObjectInstanceBase second = null)
		//{
		//	for (int i = 0; i < conds.Length; i++)
		//	{
		//		var cond = conds[i];
		//		if (cond == null) continue;
		//		if (!cond.Invoke(eff, first, second)) return false;
		//	}

		//	return true;
		//}

		//public static bool All<TData>(			
		//this List_<Effect<TData>.Condition> conds,
		//Effect<TData> eff,
		//ObjectInstanceBase first,
		//ObjectInstanceBase second = null)
		//{
		//	for (int i = 0; i < conds.Length; i++)
		//	{
		//		var cond = conds[i];
		//		if (cond == null) continue;
		//		if (!cond.Invoke(eff, first, second)) return false;
		//	}

		//	return true;
		//}

		public static bool All(
		this ConditionBase[] conds,
		IConditionContext context
		)
		{
			for(int i = 0; i < conds.Length; i++)
			{
				var cond = conds[i];
				if(cond == null) continue;
				if(!cond.Evaluate(context)) return false;
			}

			return true;
		}

		//public static bool All(
		//this List_<ConditionBase> conds,
		//IConditionContext context,
		//EntityComponentBase target = null)
		//{
		//	for(int i = 0; i < conds.Length; i++)
		//	{
		//		var cond = conds[i];
		//		if(cond == null) continue;
		//		if(!cond.Evaluate(first, target)) return false;
		//	}

		//	return true;
		//}



		//public static bool Any<TData>(
		//this List_<Effect<TData>.Condition> conds,
		//Effect<TData> eff,
		//ObjectInstanceBase first,
		//ObjectInstanceBase second = null)
		//{
		//	for (int i = 0; i < conds.Length; i++)
		//	{
		//		var cond = conds[i];
		//		if (cond == null) continue;
		//		if (cond.Invoke(eff, first, second)) return true;
		//	}

		//	return false;
		//}

		public static bool Any(
		this ConditionBase[] conds, 
		IConditionContext context
		)
		{
			for(int i = 0; i < conds.Length; i++)
			{
				var cond = conds[i];
				if(cond == null) continue;
				if(cond.Evaluate(context)) return true;
			}

			return false;
		}

		public static bool Any(
		this ListBase<ConditionBase> conds,
		IConditionContext context
		)
		{
			for(int i = 0; i < conds.Length; i++)
			{
				var cond = conds[i];
				if(cond == null) continue;
				if(cond.Evaluate(context)) return true;
			}

			return false;
		}
	}
}
