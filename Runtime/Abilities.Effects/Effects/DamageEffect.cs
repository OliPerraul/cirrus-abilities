using Cirrus.Collections;
using Cirrus.Arpg.Abilities;
using Cirrus.Arpg.Attributes;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using Cirrus.Unity.Editor;
using Cirrus.Unity.Numerics;
using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	// TODO
	// simulate loss
	// If you wait too long, permanent damage will be dealt and impossible to save them

	// Get bonus for total max health of characters in a level

	// Order matters, Permanent should be calculated after  critical hit

	// Critical hit should be a processor

	// Permanent damage as separate class since, permanent damage should be
	// conditionally applied

	// TODO flat permanent dmg
	public class PermanentDamageFilter : EffectFilter
	{
		public static object OperationUtils { get; private set; }

		// Calculate amount of damage that is permanent
		public Operation PermanentAmountOperation { get; set; }

		//public override FilterResult ApplyFilter(ObjectBase target, EffectInstanceBase effect)
		//{
		//	return effect.ApplyFilter(this);
		//}
	}

	// TODO replace with correct usage of decorators?

	public partial class DamageEffect : EffectBase
	{
#if UNITY_EDITOR
		//[MenuItem("Assets/Create/Cirrus.Arpg/Abilities/Effects/Damage", false, priority = GameUtils.MenuItemAssetAbilitiesPriority)]
		//public static void CreateAsset() => EditorScriptableObjectUtils.CreateAsset<DamageEffect>();
#endif

		public EntityFlags targetFlags = BitwiseUtils.All<EntityFlags>();		

		public Range_ amount = -1;
		
		public Range_ permanent = -1;

		// TODO handle getting filters

		protected override bool _GetInstance(IEffectContext context, out IEffectInstance instance)
		{
			instance = null;
			DamageEffectInstance damage;
			damage = context.Source.damagePool.Get(this);
			if (damage != null)
			{
				damage.Resource = this;
				damage.amount = amount < 0 ? -1 : amount.Random().RoundToDecimals(2);
				damage.permanent = permanent < 0 ? -1 : permanent.Random().RoundToDecimals(2);
				instance = damage;
				return true;
			}
			return false;
		}
	}

	public class DamageEffectInstance : EffectInstanceBase
	{
		public float amount;

		public float permanent;

		private DamageEffect _resource;

		public override EffectBase Resource { get => _resource; set => _resource = (DamageEffect)value; }

		public override bool DoApply(IEffectContext context)
		{
			if (context.Target != null)
			{
				CharacterObject chara = context.Target.CharacterObject;
				// TODO combine with existing
				if (permanent > 0)
				// Permanent damage simply is decrease of max health
				// it is much easier to read/visualize than max health modifier crap
				{
					// Add effect on permanent damage
					chara.Attributes.MaxHealth.Update(
						chara.Attributes.MaxHealth.Current - permanent,
						new AttributeEvent { IsPermanentDamageDealt = permanent > 0 }
						);
				}

				chara.Attributes.Health.Update(-amount);

				if (chara.Attributes.Health.Current < chara.Attributes.CriticalHealthThreshold)
				{
					//ApplyI
				}

				// TODO
				// Popup text damage has really bad performance cost
				//chara.CharacterEntity.OnDamagedHandler?.Invoke(this);
				return true;
			}
			else if (context.Target != null)
			{
				EntityObjectBase obj = context.Target;

				obj.Attributes.Health.Update(-amount);

				// TODO
				// Popup text damage has really bad performance cost
				//chara.CharacterEntity.OnDamagedHandler?.Invoke(this);
				return true;
			}

			return false;
		}
	}
}

namespace Cirrus.Arpg.Entities
{
	public partial class EntityObjectBase
	{
		private const int _DamagePoolSize = 10;

		public EffectPool<DamageEffect, DamageEffectInstance> damagePool =
			new EffectPool<DamageEffect, DamageEffectInstance>(max: _DamagePoolSize);
	}
}