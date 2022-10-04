using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Cirrus.Arpg.Abilities
{
	public partial class PassiveAbilityManager
		: EntitySupportBase
	{
		//private IEnumerable<Mod> _Modifiers => Entity.EntityInst.Mods;

		public Action<IPassiveAbilityInstance> OnAbilityAddedHandler;

		public Action<IPassiveAbilityInstance> OnAbilityRemovedHandler;

		public Action<PassiveAbilityUpdateMap> OnAbilityUpdatedHandler;

		private HashSet<Object> _passiveAbilitiesReferences = new HashSet<Object>();		

		//public Action<Modifier> OnModifierRemovedHandler;

		//public IEnumerator<Mod> GetEnumerator()
		//{
		//	//return _Modifiers.GetEnumerator();
		//}

		//IEnumerator IEnumerable.GetEnumerator()
		//{
		//	return ((IEnumerable)_Modifiers).GetEnumerator();
		//}
	}
}
