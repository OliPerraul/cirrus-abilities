using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Numerics;
using Cirrus.Unity.Editor;

using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

//using Cirrus.Arpg.World.Entities.Actions;

namespace Cirrus.Arpg.Abilities
{
	[Flags]
	public enum SustainedActiveAbilityImplFlags
	{
		ScaleStrengthByCharge = 1 << 0,
	}

	public abstract partial class SustainedActiveAbilityImplBase : ActiveAbilityImplBase
	{
		[field: SerializeField]
		public float ChargeTime { get; set; } = 2f;

		// TODO defer frequency to let effects have different time
		[field: SerializeField]
		public float FrequencyTime { get; set; } = 0.5f;

		[field: SerializeField]
		public float RechargeTime { get; set; } = 2f;

		[field: SerializeField]
		public SustainedActiveAbilityImplFlags SustainedActiveAbilityFlags { get; set; }		
	}

	public partial class SustainedActiveAbilityImpl : SustainedActiveAbilityImplBase
	{
		//[SerializeField]
		//[MarkedUpField(showControl: false, indentChildren: false)]
		//public AbilityActionData data;

#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Active Ability Impls/Sustained", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<SustainedActiveAbilityImpl>();
#endif
	}

	public enum SustainedActiveAbilityImplInstanceFlags
	{
		IsEnded = 1 << 0,
	}

	public abstract partial class SustainedActiveAbilityImplInstanceBase : ActiveAbilityImplInstanceBase
	{
		public SustainedActiveAbilityImplInstanceFlags SustainedActiveAbilityInstanceFlags { get; set; }

		protected abstract SustainedActiveAbilityImplBase _SustainedActiveAbilityImpl { get; set; }

		public bool IsEnded
		{
			get => (SustainedActiveAbilityInstanceFlags & SustainedActiveAbilityImplInstanceFlags.IsEnded) != 0;
			set => SustainedActiveAbilityInstanceFlags = value ?
				SustainedActiveAbilityInstanceFlags | SustainedActiveAbilityImplInstanceFlags.IsEnded :
				SustainedActiveAbilityInstanceFlags & ~SustainedActiveAbilityImplInstanceFlags.IsEnded;
		}

		//[SerializeField]
		//public float ChargeTime { get; set; } = 2f;

		//// TODO defer frequency to let effects have different time
		//[SerializeField]
		//public float FrequencyTime { get; set; } = 0.5f;

		//public float RechargeTime { get; set; } = 2f;

		//public AbilityActionBase SelfInflictedSustainedAction { get; set; }

		//public AbilityActionBase SustainedAction { get; set; }

		//public AbilityActionBase SelfInflictedOnEmptyAction { get; set; }

		protected float _remainingTime = 0;

		protected float _remainingRechargeTime = 0;

		protected float _baseTime = 0;

		protected Timer _rechargeTimer;

		protected Coroutine _startUseCoroutine;

		protected Coroutine _sustainedCoroutine;

		//public float Strength = 1;

		public override float Charge
		{
			get => _charge;

			set
			{
				_charge = value;
				if (_charge < 0) _charge = 0;
				if (_charge >= _SustainedActiveAbilityImpl.ChargeTime) _charge = _SustainedActiveAbilityImpl.ChargeTime;
				OnChargeChangedHandler?.Invoke(1 - (_charge / _SustainedActiveAbilityImpl.ChargeTime));
			}
		}

		public override bool IsEmpty => _charge <= 0;

		public override float Cooldown
		{
			get => _cooldownTimer.IsActive ?
				1 - (_cooldownTimer.Time / _SustainedActiveAbilityImpl.CooldownTime) :
				_cooldown;

			set => _cooldown = value;
		}

	}

	public partial class SustainedActiveAbilityImplInstance : SustainedActiveAbilityImplInstanceBase
	{
		private SustainedActiveAbilityImplBase _resource;
		protected override ActiveAbilityImplBase _ActiveAbilityImpl => _resource;

		protected override SustainedActiveAbilityImplBase _SustainedActiveAbilityImpl
		{
			get => _resource;
			set => _resource = (SustainedActiveAbilityImplBase)value;
		}

		protected override AbilityImplBase _AbilityResourceImpl => _resource;		

		public SustainedActiveAbilityImplInstance(SustainedActiveAbilityImplBase asset) : base(asset)
		{
			_resource = asset;

		}
	}
}