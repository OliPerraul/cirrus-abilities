//using Cirrus.DH.Actions.Abilities;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class AbilityBase
	{
		protected abstract AbilityInstanceBase _CreateInstance();

		protected virtual void _PopulateInstance(AbilityInstanceBase inst)
		{
		}

		public AbilityInstanceBase CreateInstance()
		{
			var inst = _CreateInstance();
			_PopulateInstance(inst);
			return inst;
		}
	}

	public abstract partial class AbilityInstanceBase
	{
		public AbilityInstanceBase(IAbility ab)
		{
		}
	}
}
