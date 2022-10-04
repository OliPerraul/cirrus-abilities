using System;
using System.Collections.Generic;
using System.Linq;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using System.Collections;
using UnityEngine;
using Cirrus.Unity.Numerics;
using Cirrus.Objects;
//using Cirrus.Arpg.World.Entities.Actions;

namespace Cirrus.Arpg.Abilities
{
	public enum ActiveAbilityContentFlags
	{
		Default = 1 << 0
	}

	public interface IActiveAbilityCommon
	{

	}

	public interface IActiveAbility
	: IAbility
	, IActiveAbilityCommon
	, IFlagged<ActiveAbilityContentFlags>
	{
		IActiveAbilityInstance CreateInstance();
	}

	public interface IActiveAbilityCommonInstance
	{
		Action<float> OnChargeChangedHandler { get; set; }

		Action OnChargeEmptyHandler { get; set; }

		Action OnCooldownedHandler { get; set; }

		Action<float> OnCooldownValueChangedHandler { get; set; }

		bool Start(CharacterObject source, EntityObjectBase target = null);

		bool End(CharacterObject source, EntityObjectBase target = null);
	}

	public interface IActiveAbilityInstance	
	: IAbilityInstance
	, IActiveAbilityCommonInstance
	, IActiveAbility
	{
		Range_ Range { get; }

		IActiveAbility ActiveAbility { get; }

		bool IsAvailable(CharacterInstanceBase source);

		Action<IActiveAbilityInstance> OnAvatarAbilityStartedHandler { get; set; }

		Action<IActiveAbilityInstance> OnAvatarAbilitySelectedHandler { get; set; }

		Action<IActiveAbilityInstance> OnAvailableHandler { get; set; }


		//bool IsActive { get; }

		//bool IsDefault { get; set; }

		// DO NOT DO THIS
		// Party members should be at the back of the queue
		// TODO: User should be able to modify this by setting
		// target that are either enemies or friendlies, etc.. 
		// this is to help not target friendlies during battle
		// TODO: we could also use Condition for this
		//public int TargetFlags { get; set; }		


		//AbilityActionBase Action { get; }

		//AbilityActionBase Incident { get; }

		EntityObjectBase Target { get; }
	}

	public partial class ActiveAbility
	: AbilityBase
	//, IActiveSkill
	, IActiveAbility
	, IActiveAbilityImpl
	, IFlagged<ActiveAbilityContentFlags>
	, IFlagged
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Active Skill", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<ActiveSkill>();
#endif
		public static implicit operator IActiveAbility[](ActiveAbility ab) { return new IActiveAbility[] { ab }; }


		//public static implicit operator IActiveSkill[](ActiveAbility skill) { return new IActiveSkill[] { ab }; }


		public static implicit operator ActiveAbility[](ActiveAbility ab) { return new ActiveAbility[] { ab }; }

		public ActiveAbilityContentFlags flags;
		ActiveAbilityContentFlags IFlagged<ActiveAbilityContentFlags>.Flags { get => flags; set => flags = value; }

		int IFlagged.Flags { get => (int)flags; set { flags = (ActiveAbilityContentFlags) value; } }

		[SerializeField]
		//[SerializeEmbedded]
		public ActiveAbilityImplBase _activeAbilityImpl;
		public ActiveAbilityImplBase ActiveAbilityImpl => _activeAbilityImpl;

		public float Charge => _activeAbilityImpl.Charge;

		public float Cooldown => _activeAbilityImpl.Cooldown;

		public float StartLag => _activeAbilityImpl.StartLag;

		public float EndLag => _activeAbilityImpl.EndLag;

		public Range_ Range => _activeAbilityImpl.Range;

		//public Vector3 Direction => ((IActiveAbility)Ability).Direction;

		public ActiveAbilityImplFlags ActiveAbilityFlags => ((IActiveAbilityImpl)_activeAbilityImpl).ActiveAbilityFlags;

		EntityFlags IActiveAbilityImpl.TargetFlags { get => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags; set => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags = value; }
	}


	public partial class ActiveAbilityInstance
	: AbilityInstanceBase
	, IActiveAbilityImpl
	, IActiveAbilityInstance
	, IActiveAbility
	{
		private ActiveAbility _resource;

		public IActiveAbility ActiveAbility => _resource;

		protected ActiveAbilityImplInstanceBase _activeAbilityImpl;

		public override IAbility AbilityResource => _resource;

		public EntityObjectBase Target => _activeAbilityImpl.Target;

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

		public Vector3 Direction => _activeAbilityImpl.Direction;

		public float Cooldown => _activeAbilityImpl.Cooldown;

		public float Charge => _activeAbilityImpl.Charge;

		public string Name => _resource.Name;

		public float StartLag => _resource.StartLag;

		public float EndLag => _resource.EndLag;

		public Range_ Range => _resource.Range;

		public ActiveAbilityImplInstanceFlags ActiveAbilityInstanceFlags { get; set; }
		Action<IActiveAbilityInstance> IActiveAbilityInstance.OnAvatarAbilityStartedHandler { get; set; }
		Action<IActiveAbilityInstance> IActiveAbilityInstance.OnAvatarAbilitySelectedHandler { get; set; }

		EntityObjectBase IActiveAbilityInstance.Target => throw new NotImplementedException();

		public ActiveAbilityImplFlags ActiveAbilityFlags => ((IActiveAbilityImpl)_activeAbilityImpl).ActiveAbilityFlags;

		EntityFlags IActiveAbilityImpl.TargetFlags { get => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags; set => ((IActiveAbilityImpl)_activeAbilityImpl).TargetFlags = value; }

		public ActiveAbilityImplState State => _activeAbilityImpl.State;

		public ActiveAbilityContentFlags Flags { get => ((IFlagged<ActiveAbilityContentFlags>)_resource).Flags; set => ((IFlagged<ActiveAbilityContentFlags>)_resource).Flags = value; }

		public IActiveAbilityInstance CreateInstance()
		{
			return ((IActiveAbility)_resource).CreateInstance();
		}
	}


}
