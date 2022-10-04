using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Cirrus.Arpg.Abilities
{
	// TODO all effects should be filterable
	// No condition on effects, only use filter which are themselves conditioned


	// TODO:
	// How to filter actions (to add effects..) ?
	// How to filter strategies, (increase speed) ?

	// TODO Source modifier effect filter should be retrieve from getEffect
	// source.GetEffect should have all filtered on it already

	public enum FilterResult
	{
		None,
		Filtered,
		Cancelled,
	}



	// Filter are not applied at the source to be consistent
	// Only apply filter at the target stage once the conditions were evaluated
	// TODO should this really be abstract?
	public class EffectFilter
	: IConditional
	{
		public ConditionBase[] SourceConditions { get; set; }

		public ConditionBase[] TargetConditions { get; set; }

		public ISet<CopiableBase> Metadata { get; set; } = SetUtils.Empty<CopiableBase>();

		public virtual FilterResult ApplyFilter(EntityObjectBase target, IEffectInstance effect)
		{
			if(!TargetConditions.All(target)) return FilterResult.None;
			return _ApplyFilter(target, effect);
		}

		protected virtual FilterResult _ApplyFilter(EntityObjectBase target, IEffectInstance effect)
		{
			return FilterResult.Filtered;
		}
	}
}
