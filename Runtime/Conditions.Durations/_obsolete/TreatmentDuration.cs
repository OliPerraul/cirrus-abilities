// using Cirrus.Arpg.Abilities;
// using Cirrus.Arpg.Conditions;
// using Cirrus.Unity.Objects;
// using Cirrus.Objects;
// using Cirrus.Unity.Numerics;
// using System.Collections.Generic;
// using System.Linq;

// namespace Cirrus.Arpg.Conditions
// {
// 	public enum InjuryID : int
// 	{
// 		PermanentDamage
// 	}

// 	public class TreatmentDuration : DurationBase
// 	{
// 		public Number InitialAmount { get; set; } = new Range(1);

// 		// high priority wins
// 		public int Priority { get; set; } = 0;

// 		public float _initialAmount = 1;

// 		public float _amount = 1;

// 		// Prerequisite
// 		public TreatmentFlags TreatmentFlags { get; set; } = 0;

// 		// Bonus
// 		public ICollection<TreatmentEfficacy> Effectiveness { get; set; } = new TreatmentEfficacy[0];

// 		public override void Start(EntityBase source)
// 		{
// 			base.Start(source);
// 			source.AddDuration(this);
// 			_amount = _initialAmount = InitialAmount.Value;
// 		}

// 		public bool ApplyTreatment(TreatmentEffect treatment)
// 		{
// 			// TODO could this use filters?
// 			float treatmentAmount = treatment.Data.Amount;
// 			foreach (var effectiveness in Effectiveness)
// 			{
// 				//if (effectiveness.Flags.Intersects(treatment.EffectFlags))
// 				//{
// 				//	treatmentAmount = effectiveness.Operation.Evaluate(treatmentAmount);
// 				//}
// 			}

// 			_amount -= treatmentAmount;
// 			if (_amount < 0) _amount = 0;

// 			OnRemainingHandler?.Invoke(
// 				_amount / _initialAmount,
// 				treatmentAmount / _initialAmount);

// 			return _amount <= 0;
// 		}

// 		public override void OnEnded()
// 		{
// 			base.OnEnded();
// 		}

// 		//public override IEffect Get()
// 		//{
// 		//	return new InjuryEffect
// 		//	{
// 		//		Amount = Amount,
// 		//		TargetConditions = TargetConditions,
// 		//		TreatmentEffectiveness = TreatmentEffectiveness
// 		//	};
// 		//}

// 		//public override bool TryGet(ObjectBase source, out IEffect effect)
// 		//{
// 		//	return base.TryGet(source, out effect);
// 		//}
// 	}
// }

// namespace Cirrus.Arpg.Objects
// {
// 	public abstract partial class EntityBase
// 	{
// 		public virtual void AddDuration(TreatmentDuration duration) { }

// 		public virtual void RemoveDuration(TreatmentDuration duration) { }
// 	}
// }


// namespace Cirrus.Arpg.Entities.Characters
// {
// 	public partial class CharacterEntity
// 	{
// 		// Hight prioerity wins		

// 		public Dictionary<int, TreatmentDuration> _injuryDictionary = new Dictionary<int, TreatmentDuration>();
// 		public List<TreatmentDuration> _injuries = new List<TreatmentDuration>();

// 		public TreatmentFlags _injuryFlags = 0;

// 		public override void AddDuration(TreatmentDuration duration)
// 		{
// 			if (!_injuryDictionary.TryGetValue(duration.ComputeID(), out TreatmentDuration _))
// 			{
// 				_injuryFlags |= duration.TreatmentFlags;
// 				_injuryDictionary.Add(duration.ComputeID(), duration);
// 				_injuries.Add(duration);
// 				_injuries.OrderByDescending(x => x.Priority);
// 			}
// 		}

// 		public override void RemoveDuration(TreatmentDuration duration)
// 		{
// 			_injuryDictionary.Remove(duration.ComputeID());
// 			_injuries.Remove(duration);
// 			for (int i = 0; i < 32; i++)
// 			{
// 				int flag = 1 << i;
// 				if (duration.TreatmentFlags.Intersect(flag)) goto end;

// 				foreach (var injury in _injuries)
// 				// Skip removing flag if supported by other injury
// 				{
// 					if (injury.TreatmentFlags.Intersect(flag)) goto end;
// 				}

// 				_injuryFlags = _injuryFlags & ~(TreatmentFlags)flag;
// 			end:
// 				continue;
// 			}

// 			_injuries.OrderByDescending(x => x.Priority);
// 		}
// 	}
// }