//using Cirrus.Arpg.Actions.Items;
//using Cirrus.Events;

using Cirrus.Unity.Objects;
using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	public partial class PassiveSkill
	{
		protected override AbilityInstanceBase _CreateInstance() => new PassiveSkillInstance(this);

		protected override void _PopulateInstance(AbilityInstanceBase inst)
		{
			_PopulateInstance((PassiveSkillInstance)inst);
		}

		public new PassiveSkillInstance CreateInstance()
		{
			return (PassiveSkillInstance)base.CreateInstance();
		}

		protected virtual void _PopulateInstance(PassiveSkillInstance inst)
		{
		}

		IPassiveAbilityInstance IPassiveAbility.CreateInstance()
		{
			return (PassiveSkillInstance) CreateInstance();
		}
	}

	public partial class PassiveSkillInstance
	{
		public PassiveSkillInstance(PassiveSkill resource) : base(resource)
		{
			_resource = resource;
		}
	}
}
