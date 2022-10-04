using Cirrus.Arpg.Entities.Characters;
using Cirrus.States;
using System;

namespace Cirrus.Arpg.Abilities
{
	public partial class AbilityTriggerState
	{
		public virtual int ID { get; set; } = -1;

		public virtual string Name { get; set; } = StringUtils.UnknownName;

		public Action<IState> OnStateChangedHandler { get; set; }

		public CharacterInstanceBase CharacterInst { get; set; }

		public virtual bool IsInRotation => false;		

		public int Priority { get; set; } = int.MaxValue;
	}
}
