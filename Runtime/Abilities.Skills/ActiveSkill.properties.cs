using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using Range_ = Cirrus.Unity.Numerics.Range_;

namespace Cirrus.Arpg.Abilities
{
	public enum ActiveSkillFlags
	{
		Special,
		Command,
	}

	public interface IActiveSkill
	{
		ActiveSkillFlags ActiveSkillFlags { get; set; }
		EntityObjectFlags TargetFlags { get; set; }
	}

	public partial class ActiveSkill
	: SkillBase
	, IActiveSkill
	, IActiveAbility
	, IActiveAbilityImpl
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Active Skill", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<ActiveSkill>();
#endif
		public static implicit operator IActiveAbility[] (ActiveSkill skill) { return new IActiveAbility[] {skill }; }


		public static implicit operator IActiveSkill[](ActiveSkill skill) { return new IActiveSkill[] { skill }; }


		public static implicit operator ActiveSkill[](ActiveSkill skill) { return new ActiveSkill[] { skill }; }

		[SerializeField]
		//[SerializeEmbedded]
		public ActiveAbilityImplBase _activeAbilityImpl;
		public ActiveAbilityImplBase ActiveAbilityImpl => _activeAbilityImpl;

		[field: SerializeField]
		public ActiveSkillFlags ActiveSkillFlags { get; set; }

		[field: SerializeField]
		public EntityObjectFlags TargetFlags { get; set; }

		[SerializeField]
		private ActiveAbilityContentFlags _activeAbilityContentFlags;
		ActiveAbilityContentFlags IFlagged<ActiveAbilityContentFlags>.Flags
		{
			get => _activeAbilityContentFlags;
			set { _activeAbilityContentFlags = value; }
		}


		public float Charge => ((IActiveAbilityImpl)_activeAbilityImpl).Charge;

		public float Cooldown => ((IActiveAbilityImpl)_activeAbilityImpl).Cooldown;

		public float StartLag => ((IActiveAbilityImpl)_activeAbilityImpl).StartLag;

		public float EndLag => ((IActiveAbilityImpl)_activeAbilityImpl).EndLag;

		public Range_ Range => ((IActiveAbilityImpl)_activeAbilityImpl).Range;

		//public Vector3 Direction => ((IActiveAbility)Ability).Direction;

		public ActiveAbilityImplFlags ActiveAbilityFlags => ((IActiveAbilityImpl)_activeAbilityImpl).ActiveAbilityFlags;

		EntityFlags IActiveAbilityImpl.TargetFlags { get => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags; set => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags = value; }
	}


	public partial class ActiveSkillInstance
	: SkillInstanceBase
	, IActiveAbilityImpl
	, IActiveAbilityInstance
	, IActiveAbility
	{
		private ActiveSkill _resource;

		public IActiveAbility ActiveAbility => _resource;

		protected ActiveAbilityImplInstanceBase _activeAbilityImpl;

		public override IAbility AbilityResource => _resource;

		public EntityObjectBase Target => _activeAbilityImpl.Target;

		public virtual Action<IActiveAbilityInstance> OnAvatarAbilityStartedHandler { get; set; }

		public virtual Action<IActiveAbilityInstance> OnAvatarAbilitySelectedHandler { get; set; }

		public virtual Action<IActiveAbilityInstance> OnEndRequestHandler { get; set; }

		public virtual Action<IActiveAbilityInstance> OnAvailableHandler { get; set; }

		public Action OnCooldownedHandler
		{
			get => _activeAbilityImpl.OnCooldownedHandler;
			set => _activeAbilityImpl.OnCooldownedHandler = value;
		}

		public Action<float> OnCooldownValueChangedHandler
		{
			get => _activeAbilityImpl.OnCooldownValueChangedHandler;
			set => _activeAbilityImpl.OnCooldownValueChangedHandler = value;
		}

		public Action OnChargeEmptyHandler
		{
			get => _activeAbilityImpl.OnChargeEmptyHandler;
			set => _activeAbilityImpl.OnChargeEmptyHandler = value;
		}

		public Action<float> OnChargeChangedHandler
		{
			get => _activeAbilityImpl.OnChargeChangedHandler;
			set => _activeAbilityImpl.OnChargeChangedHandler = value;
		}

		public Action<IActiveAbilityImplInstance> OnEndLagEndedHandler
		{
			get => _activeAbilityImpl.OnEndLagEndedHandler;
			set => _activeAbilityImpl.OnEndLagEndedHandler = value;
		}

		public Action OnStartLagEndedHandler
		{
			get => _activeAbilityImpl.OnStartupLagEndedHandler;
			set => _activeAbilityImpl.OnStartupLagEndedHandler = value;
		}

		//public float CooldownTime => Ability.CooldownTime;

		//public bool IsUsableOnSelf => Ability.IsU;

		//public bool IsActive => _activeAbilityImpl.IsActive;

		//public bool IsAOE => Ability.IsAOE;

		//public Range_ Range => Ability.Range;

		public Vector3 Direction => _activeAbilityImpl.Direction;


		//public float StartLag => Ability.StartLag;

		//public float EndLag => Ability.EndLag;


		public float Cooldown => _activeAbilityImpl.Cooldown;

		public float Charge => _activeAbilityImpl.Charge;

		//public bool IsMovementRestricted => Ability.IsMovementRestricted;

		//public bool IsDirectionRestricted => Ability.IsDirectionRestricted;


		public ActiveSkillFlags ActiveSkillFlags { get => _resource.ActiveSkillFlags; set => _resource.ActiveSkillFlags = value; }

		public EntityObjectFlags TargetFlags { get => _resource.TargetFlags; set => _resource.TargetFlags = value; }

		public string Name => _resource.Name;

		public float StartLag => _resource.StartLag;

		public float EndLag => _resource.EndLag;

		public Range_ Range => _resource.Range;

		public ActiveAbilityImplInstanceFlags ActiveAbilityInstanceFlags { get; set; }
		Action<IActiveAbilityInstance> IActiveAbilityInstance.OnAvatarAbilityStartedHandler { get; set; }
		Action<IActiveAbilityInstance> IActiveAbilityInstance.OnAvatarAbilitySelectedHandler { get; set; }

		EntityObjectBase IActiveAbilityInstance.Target => _activeAbilityImpl.Target;

		public ActiveAbilityImplFlags ActiveAbilityFlags => _activeAbilityImpl.ActiveAbilityFlags;

		EntityFlags IActiveAbilityImpl.TargetFlags { get => _activeAbilityImpl.TargetFlags; set => _activeAbilityImpl.TargetFlags = value; }

		public ActiveAbilityImplState State => _activeAbilityImpl.State;

		public ActiveAbilityContentFlags Flags { get => ((IFlagged<ActiveAbilityContentFlags>)_resource).Flags; set => ((IFlagged<ActiveAbilityContentFlags>)_resource).Flags = value; }

		public IActiveAbilityInstance CreateInstance()
		{
			return _resource.CreateInstance();
		}
	}
}
