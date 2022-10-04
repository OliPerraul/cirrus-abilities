//using UnityEngine;
//using System.Collections;
//using Cirrus.DH.Actions.Modifiers;
//using Cirrus.DH.Actions;
//using Cirrus.Unity.Numerics;
//using System.Collections.Generic;
//using Cirrus.Collections;
//using Cirrus.DH.Objects;
//using Cirrus.DH.Conditions;
//using Cirrus.Names;
//using System.Linq;

//namespace Cirrus.DH.Conditions
//{
//	//public enum InjuryID : int
//	//{
//	//	PermanentDamage
//	//}

//	public class DamageDuration : DurationBase, IIdentifiable
//	{
//		public IValue InitialAmount { get; set; } = new Range(1);

//		// high priority wins
//		public int Priority { get; set; } = 0;

//		public float _initialAmount = 1;

//		public float _amount = 1;

//		public string Identifier { get; set; } = ProjectUtils.UnknownName;

//		public int Id { get; set; } = -1;

//		public string Salt { get; set; } = "";

//		public override void Begin(EntityBase source)
//		{
//			base.Begin(source);
//			source.AddDuration(this);
//			_amount = _initialAmount = InitialAmount;			
//		}

//		public bool ApplyTreatment(DamageEffect treatment)
//		{			
//			_amount -= treatmentAmount;
//			if(_amount < 0) _amount = 0;

//			OnRemainingHandler?.Invoke(_amount / _initialAmount);

//			return _amount <= 0;			
//		}

//		public override void OnEnded()
//		{
//			base.OnEnded();
//		}

//		//public override IEffect Get()
//		//{
//		//	return new InjuryEffect
//		//	{
//		//		Amount = Amount,
//		//		TargetConditions = TargetConditions,
//		//		TreatmentEffectiveness = TreatmentEffectiveness
//		//	};
//		//}

//		//public override bool TryGet(ObjectBase source, out IEffect effect)
//		//{
//		//	return base.TryGet(source, out effect);
//		//}
//	}
//}

//namespace Cirrus.DH.Objects
//{
//	public abstract partial class EntityBase
//	{
//		public virtual void AddDuration(DamageDuration duration) { }

//		public virtual void RemoveDuration(DamageDuration duration) { }
//	}
//}


//namespace Cirrus.DH.Entities.Characters
//{
//	public partial class CharacterEntity
//	{
//		// Hight prioerity wins		

//		public Dictionary<int , DamageDuration> _damageDurationsDictionary = new Dictionary<int , TreatmentDuration>();
//		public List<DamageDuration> _injuries = new List<TreatmentDuration>();

//		public TreatmentFlags _injuryFlags = 0;

//		public override void AddDuration(TreatmentDuration duration)
//		{
//			if(!_injuryDictionary.TryGetValue(duration.FindID() , out TreatmentDuration _))
//			{
//				_injuryFlags |= duration.TreatmentFlags;
//				_injuryDictionary.Add(duration.FindID() , duration);
//				_injuries.Add(duration);
//				_injuries.OrderByDescending(x => x.Priority);
//			}
//		}

//		public override void RemoveDuration(TreatmentDuration duration)
//		{
//			_injuryDictionary.Remove(duration.FindID());
//			_injuries.Remove(duration);
//			for(int i = 0; i < 32; i++)
//			{
//				int flag = 1 << i;
//				if((duration.TreatmentFlags & (TreatmentFlags)flag) == 0) goto end;

//				foreach(var injury in _injuries)
//				// Skip removing flag if supported by other injury
//				{
//					if((injury.TreatmentFlags & (TreatmentFlags)flag) != 0) goto end;
//				}

//				_injuryFlags = _injuryFlags & ~(TreatmentFlags)flag;
//end:
//				continue;
//			}

//			_injuries.OrderByDescending(x => x.Priority);
//		}
//	}
//}