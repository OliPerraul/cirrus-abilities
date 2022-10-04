//using Cirrus.Arpg.Actions.Items;
//using Cirrus.Events;

using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Objects;
using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	public partial class ActiveSkill
	{
		protected override AbilityInstanceBase _CreateInstance() => new ActiveSkillInstance(this);


		protected override void _PopulateInstance(AbilityInstanceBase inst)
		{
			_PopulateInstance((ActiveSkillInstance)inst);
		}

		public new ActiveSkillInstance CreateInstance()
		{
			return (ActiveSkillInstance)base.CreateInstance();
		}

		IActiveAbilityInstance IActiveAbility.CreateInstance()
		{
			return CreateInstance();
		}

		protected virtual void _PopulateInstance(ActiveSkillInstance inst)
		{
		}
	}

	public partial class ActiveSkillInstance
	{
		public ActiveSkillInstance(ActiveSkill resource) : base(resource)
		{
			_resource = resource;
			_activeAbilityImpl = resource.ActiveAbilityImpl.GetInstance();
			_activeAbilityImpl.OnEndLagEndedHandler += _OnEndLagEnded;
		}

		public void _OnEndLagEnded(IActiveAbilityImplInstance impl)
		{
			if(IsAvailable(_activeAbilityImpl.Source))
			{
				OnAvailableHandler?.Invoke(this);
			}
		}

		public bool IsAvailable(CharacterInstanceBase source)
		{
			return _activeAbilityImpl.State == ActiveAbilityImplState.Available;
		}

		public bool Test(ActiveSkillFlags flags) => _resource.ActiveSkillFlags.Intersects(flags);

		public bool Start(CharacterObject source, EntityObjectBase target)
		{
			return _activeAbilityImpl.Start(source, target);
		}

		public bool End(CharacterObject source, EntityObjectBase target)
		{
			return _activeAbilityImpl.End(source, target);// (source, target);
		}
	}
}
