using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using System;

namespace Cirrus.Arpg.Abilities
{
	// IDEA? damage afinities (melee, spell etc..)
	// might be overkill, healing is focus

	// Rarely used, only to speed up
	public enum EffectInstanceMode
	{
		NonInstanced,
		InstancedperExecution,
		InstancedperActor,
	}

	public enum EffectType
	{
		Unknown,
		Object_Destroyed,
		Shadow_Dispelled
	}

	[Flags]
	public enum EffectFlags
	{
		None = 0,
		// Event qualifiers
		Damage = 1 << 0,
		Distraction = 1 << 1,
		Collision = 1 << 2,
		Discriminate = 1 << 3
	}


	public static class EffectUtils
	{
		public static void Populate(			
			EffectInstanceBase res
			, EffectBase ef
			)
		{
			//ef.EffectFlags = res.EffectFlags;
			//ef.TargetConditions = res.TargetConditions;
		}

		//public static readonly Effect Null = new Effect();

		public static readonly ConditionBase[] Empty;
	}

	public static class EffectInstanceUtils
	{
		public static bool Apply(this IEffectInstance effect, IEffectContext context)
		{
			// TODO: Factor in incoming filter

			if (effect.TargetConditions.All(context))
			{
				if (context.Target != null)
				{
					foreach (var filter in context.Target.IncomingFilters)
					{
						filter.ApplyFilter(context.Target, effect);
					}
				}

				if (effect.DoApply(context))
				{
					for (int i = 0; i < effect.SubEffectInsts.Count; i++)
					{
						if (!effect.SubEffectInsts[i].Apply(context))
						{
							return false;
						}
					}

					context.Target.onEffectAppliedHandler?.Invoke(context, effect);
					return true;
				}
			}

			for (int i = 0; i < effect.FallbackEffectInsts.Count; i++)
			{
				effect.FallbackEffectInsts[i].Apply(context);
			}

			return false;
		}

		public static bool Update(this IEffectInstance effect, IEffectContext context)
		{
			if (effect.TargetConditions.All(context))
			{
				//if(target != null)
				//	foreach(var filter in target.IncomingFilters) filter
				//		.ApplyFilter(target, this);

				if (effect.DoUpdate(context))
				{
					for (int i = 0; i < effect.SubEffectInsts.Count; i++)
					{
						if (!effect.SubEffectInsts[i].Update(context))
						{
							return false;
						}
					}

					context.Target.onEffectUpdateHandler?.Invoke(context, effect);
					return true;
				}
			}

			return false;
		}

		public static bool Unapply(this IEffectInstance effect, IEffectContext context)
		{
			if (effect.DoUnapply(context))
			{
				for (int i = 0; i < effect.SubEffectInsts.Count; i++)
				{
					if (!effect.SubEffectInsts[i].Unapply(context))
					{
						return false;
					}
				}

				context.Target.onEffectUnappliedHandler?.Invoke(context, effect);
				return true;
			}

			return false;
		}
	}

	// https://en.wikipedia.org/wiki/Identity_function
	//public class Effect : EffectBase
	//{
	//	public override bool GetInstance(
	//		ObjectBase source,
	//		out EffectBase instance)
	//	{
	//		instance = this;
	//		return true;
	//	}
	//}
}
