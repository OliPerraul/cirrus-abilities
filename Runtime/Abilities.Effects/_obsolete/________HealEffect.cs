//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.Objects; //using Cirrus.Arpg.World.Entities.Actions.Goals;
//						//using Cirrus.Arpg.World.Entities.Actions.Task;
//using Cirrus.Unity.Numerics;
//using Cirrus.Unity.Randomness;
////using Entities.Characters;

//namespace Cirrus.Arpg.Abilities
//{
//	//public class HealthBonusModification : AttributeChange
//	//{
//	//	public float Value = 0;

//	//	public float MaxBonus = 0;

//	//	public override void OnRemaining(float t, float delta)
//	//	{
//	//		base.OnRemaining(t, delta);
//	//		Value -= delta * MaxBonus;
//	//		if (Value < 0) Value = 0;
//	//	}

//	//	public override void Detach(EntityBase target)
//	//	{

//	//	}
//	//}	

//	public class OverhealFilter : EffectFilter
//	{
//		public Operation Conversion { get; set; }

//		protected override FilterResult _ApplyFilter(ObjectBase target, EffectBase result)
//		{
//			return base._ApplyFilter(target, result);
//		}
//	}

//	public class HealEffect : EffectBase
//	{
//		public Range_ Range = 2;

//		public float RoundedAmount => Range;

//		private bool HasCriticalHit = false;

//		public Chance CriticalHitChance;

//		public Operation _criticalHitOperation;

//		public Operation CriticalHitOperation => _criticalHitOperation;

//		public bool IsCriticalHit => HasCriticalHit && _criticalHitOperation != null && CriticalHitChance.Result;


//		public float Amount;

//		public bool _IsCriticalHit { get; set; }

//		// TODO populate with user effects

//		private HealEffect _GetHealingInstance(ObjectBase source, HealEffect prototype)
//		{
//			HealEffect heal = null;
//			if(source.Character != null)
//			{

//				heal = source.HealPool.Get(prototype);

//				// TODO factor in luck
//				// TODO factor in heal
//				heal._IsCriticalHit = prototype.IsCriticalHit;

//				heal.Amount =
//					heal._IsCriticalHit ?
//						prototype.CriticalHitOperation.EvaluateSecond(prototype.Range) :
//						(float)prototype.Range;
//			}
//			else
//			{
//				heal = source.HealPool.Get(prototype);
//				heal.Amount = prototype.RoundedAmount;
//			}

//			return heal;
//		}

//		protected override bool _GetInstance(ObjectBase source, out EffectBase instance)
//		{
//			instance = source == null ?
//				this.Realize() :
//				_GetHealingInstance(source, this);
//			instance.IsInstance = true;
//			return true;
//		}

//		protected override bool _Apply(IEffectSource action, ObjectBase target)
//		{
//			if(target != null)
//			{
//				CharacterComponent chara = target;
//				chara.Attrs.Attr_Health.Update(
//					chara
//					.Attrs
//					.Attr_Health.Current + Amount);

//				// TODO injuries are modifiers...
//				float criticalHealthThreshold = chara.Attrs.CriticalHealthThreshold;
//				if(chara.Attrs.Attr_Health.Current < criticalHealthThreshold)
//				{
//					// TODO recover				
//				}

//				return true;
//			}

//			return false;
//		}
//	}
//}
