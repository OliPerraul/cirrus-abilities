//using UnityEngine.InputSystem;
using Cirrus.Arpg.Entities;
using System.Collections.Generic;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class PassiveAbilityBase
	{
		public new PassiveAbilityInstanceBase CreateInstance()
		{
			return (PassiveAbilityInstanceBase)base.CreateInstance();
		}

		protected override void _PopulateInstance(AbilityInstanceBase inst)
		{
		}

		IPassiveAbilityInstance IPassiveAbility.CreateInstance()
		{
			return CreateInstance();
		}
	}

	public partial class PassiveAbility : PassiveAbilityBase
	{
		protected override AbilityInstanceBase _CreateInstance()
		{
			return new PassiveAbilityInstance(this);
		}

		protected override void _PopulateInstance(AbilityInstanceBase inst)
		{
			_PopulateInstance((PassiveAbilityInstance)inst);
		}

		public new PassiveAbilityInstance CreateInstance()
		{
			return (PassiveAbilityInstance)base.CreateInstance();
		}



		protected virtual void _PopulateInstance(PassiveAbilityInstance inst)
		{
		}
	}

	public abstract partial class PassiveAbilityInstanceBase
	{
		public void _OnImplDurationEndedHandler()
		{
			OnDurationEndedHandler?.Invoke(this);
		}

		public bool GetInteracts(
			IPassiveAbility target
			, out List<PassiveAbilityInteraction> interactions)
		{
			return _PassiveAbilityImplInstance.GetInteracts(target, out interactions);
		}

		public bool DoApply(EntityObjectBase target)
		{
			return _PassiveAbilityImplInstance.DoApply(target);
		}

		public bool DoUnapply(EntityObjectBase target)
		{
			return _PassiveAbilityImplInstance.DoUnapply(target);
		}

		public PassiveAbilityUpdateFlags DoUpdateDuration(EntityObjectBase e, PassiveAbilityDelta update)
		{
			return _PassiveAbilityImplInstance.DoUpdateDuration(e, update);
		}

		public bool DoConsolidate(EntityObjectBase target, IPassiveAbilityInstance update)
		{
			return _PassiveAbilityImplInstance.DoConsolidate(target, update);
		}
	}


	public partial class PassiveAbilityInstance
	{
	}
}
