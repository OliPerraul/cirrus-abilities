using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

using static Cirrus.Arpg.Abilities.AbilityActionImpl;

namespace Cirrus.Arpg.Abilities
{
	public enum ActiveAbilityImplState
	{
		Available,
		Unavailable,		
		Active,
	}

	[Flags]
	public enum ActiveAbilityImplFlags
	{
		MovementRestricted = 1 << 0,
		DirectionRestricted = 1 << 1,
		AOE = 1 << 2,
		UsableOnSelf = 1 << 3
	}

	public interface IActiveAbilityImpl
	: IActiveAbilityCommon
	{
		float Charge { get; }

		// Cooldown complete
		float Cooldown { get; }


		//bool IsTargetCapacityUnlimited { get; }// { return 1; } }// Resource.SimultaneousCapacity; } }

		// Start-up lag, also known as just start-up and windup, is the delay between 
		// a move being initiated and the move having an effect, such as the length of time before a hitbox 
		float StartLag { get; }// { return 2; } }//; Resource.StartLag; } }

		// Disable inputs??
		//  is the delay between a move's effect finishing and another action being available to begin
		float EndLag { get; }//{ return; } }// Resource.EndLag; } }

		Range_ Range { get; }

		//Vector3 Direction { get; }

		ActiveAbilityImplFlags ActiveAbilityFlags { get; }

		// DO NOT DO THIS
		// Party members should be at the back of the queue
		// TODO: User should be able to modify this by setting
		// target that are either enemies or friendlies, etc.. 
		// this is to help not target friendlies during battle
		// TODO: we could also use Condition for this
		public EntityFlags TargetFlags { get; set; }

	}

	public abstract partial class ActiveAbilityImplBase 
	: AbilityImplBase
	, IActiveAbilityImpl
	{
		[field: SerializeField]
		[field: FormerlySerializedAs("Events")]
		public AbilityAction[] Actions { get; set; }

		[field: SerializeField]
		public ActiveAbilityImplFlags ActiveAbilityFlags { get; set; }


		//[field: SerializeField]
		//public Vector3 Direction { get; set; }

		[field: SerializeField]
		public Range_ Range { get; set; }

		[field: SerializeField]
		public float CooldownTime { get; set; }

		[field: SerializeField]
		public float StartLag { get; set; }

		/// <summary>
		/// End lag prevents other ability from being used (unlike cooldown which only prevents current ability)
		/// </summary>
		[field: SerializeField]
		public float EndLag { get; set; }
		
		[field: SerializeField]
		public virtual float Cooldown { get; set; }		

		[field: SerializeField]
		public float Charge { get; set; } = 0;

		[field: SerializeField]
		public EntityFlags TargetFlags { get; set; }
	}

	public partial class ActiveAbilityImpl : ActiveAbilityImplBase
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Active Ability Impls/Normal", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<ActiveAbilityImpl>();
#endif
	}


	public enum ActiveAbilityImplInstanceFlags
	{
		IsEnded = 1 << 0
	}

	public interface IActiveAbilityImplInstance
	: IAbilityImplInstance
	, IActiveAbilityCommonInstance
	{
		//bool IsMovementRestricted { get; }

		//bool IsDirectionRestricted { get; }

		Action<IActiveAbilityImplInstance> OnEndLagEndedHandler { get; set; }

		Action OnStartupLagEndedHandler { get; set; }

		ActiveAbilityImplState State { get; }

		ActiveAbilityImplInstanceFlags ActiveAbilityInstanceFlags {get ; set;}


		//bool IsTargetCapacityUnlimited { get; }// { return 1; } }// Resource.SimultaneousCapacity; } }

		// TODO disable focus when this ability is active ?
		//[Obsolete]
		//bool IsAOE { get; }// { return 1; } }// Resource.SimultaneousCapacity; } }

		// Start-up lag, also known as just start-up and windup, is the delay between 
		// a move being initiated and the move having an effect, such as the length of time before a hitbox 
		//float StartLag { get; }// { return 2; } }//; Resource.StartLag; } }

		// Disable inputs??
		//  is the delay between a move's effect finishing and another action being available to begin
		//float EndLag { get; }//{ return; } }// Resource.EndLag; } }

		//Range_ Range { get; }

		Vector3 Direction { get; }

		//bool Use(CharacterEntity source, EntityBase target);

		//bool Use(CharacterEntity source);

		//bool StartUse(CharacterEntity source);

	}

	public abstract partial class ActiveAbilityImplInstanceBase
	: AbilityImplInstanceBase
	, IActiveAbilityImplInstance
	, IActiveAbilityImpl
	{
		//private ActiveAbilityAssetBase
		protected abstract ActiveAbilityImplBase _ActiveAbilityImpl { get; }

		protected abstract IEnumerator _StartupCoroutine(CharacterObject source, EntityObjectBase target);

		protected WaitForSeconds _startupLagWaitForSeconds;

		public ActiveAbilityImplInstanceFlags ActiveAbilityInstanceFlags { get; set; }

		protected AbilityActionInstance[] _actions;

		public Action<IActiveAbilityImplInstance> OnEndLagEndedHandler { get; set; }

		public Action OnStartupLagEndedHandler { get; set; }

		public Action OnCooldownedHandler { get; set; }

		public Action<float> OnCooldownValueChangedHandler { get; set; }

		public Action OnChargeEmptyHandler { get; set; }

		public Action<float> OnChargeChangedHandler { get; set; }

		// TODO: replace with coroutine
		protected Timer _cooldownTimer;

		// TODO: replace with coroutine
		protected Timer _endLagTimer;

		//protected Timer _startLagTimer;

		//private bool _isActive = false;

		//public bool IsActive => _isActive;

		public ActiveAbilityImplState State { get; set; }

		protected Vector3 _direction;

		public Vector3 Direction => _direction;

		//public Range_ Range { get; set; }

		//public float CooldownTime { get; set; }


		// TODO: If condition against target, then add extra actions here
		//public AbilityActionBase StartAction { get; set; }

		//public AbilityActionBase SelfInflictedStartAction { get; set; }

		//public AbilityActionBase SelfInflictedStartLagAction { get; set; }

		//public AbilityActionBase SelfInflictedEndLagAction { get; set; }

		//public float StartLag { get; set; }

		//public float EndLag { get; set; }

		public CharacterInstanceBase Source => _source;

		protected EntityObjectBase _target;

		public EntityObjectBase Target => _target;

		public virtual bool IsCooling => !_cooldownTimer.IsActive && !IsEmpty;

		protected float _cooldown = 0;

		public virtual float Cooldown
		{
			get
			{
				// TODO fix, cooldown time may be varying
				return _cooldownTimer.IsActive ?
					1 - (_cooldownTimer.Time / _ActiveAbilityImpl.CooldownTime) :
					_cooldown;
			}

			set
			{
				_cooldown = value;
				OnCooldownValueChangedHandler?.Invoke(value);
			}
		}


		protected float _charge = 0;

		public virtual float Charge
		{
			get => _charge;

			set
			{
				_charge = value;
				OnCooldownValueChangedHandler?.Invoke(value);
			}
		}

		public virtual bool IsEmpty => true;

		public float StartLag => ((IActiveAbilityImpl)_ActiveAbilityImpl).StartLag;

		public float EndLag => ((IActiveAbilityImpl)_ActiveAbilityImpl).EndLag;

		public Range_ Range => ((IActiveAbilityImpl)_ActiveAbilityImpl).Range;

		public ActiveAbilityImplFlags ActiveAbilityFlags => ((IActiveAbilityImpl)_ActiveAbilityImpl).ActiveAbilityFlags;

		public EntityFlags TargetFlags { get => ((IActiveAbilityImpl)_ActiveAbilityImpl).TargetFlags; set => ((IActiveAbilityImpl)_ActiveAbilityImpl).TargetFlags = value; }
	}

	public partial class ActiveAbilityImplInstance : ActiveAbilityImplInstanceBase
	{
	}
}
