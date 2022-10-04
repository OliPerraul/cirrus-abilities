//using Cirrus.Arpg.Abilities;
//using Cirrus.Unity.Objects;
//using Cirrus.Arpg.Entities.Characters;
//using Cirrus.Arpg.Entities.Collectibles;

//namespace Cirrus.Arpg.Abilities
//{
//	// Defers to the weapon's strategy

//	// TODO do not use weapon strategy, instead use Weapon Ability
//	public partial class WeaponStrategy : StrategyBase
//	{	
//		public void OnWeaponEquipped(WeaponItem weapon)
//		{
//			//UpdateWeapon(_source._equipment.Weapon);
//		}

//		public void OnWeaponUnequipped()
//		{
//			//UpdateWeapon(_source._equipment.Weapon);
//		}

//		bool _GetWeaponStrategy(CharacterEntity source, out StrategyBase strat)
//		{
//			strat = null;
//			//if(source == null) return false;
//			//if(source.Equipment.Weapon == null) return false;
//			//if(source.Equipment.Weapon.Strategies == null) return false;
//			//if(source.Equipment.Weapon.Strategies.Length <= WeaponStrategyID) return false;
//			//strat = source.Equipment.Weapon.Strategies[WeaponStrategyID];
//			//return true;
//			return false;
//		}

//		private void Subscribe(StrategyBase strat)
//		{
//			strat.OnSucceededHandler += _OnTargetHit;
//			strat.OnEndedHandler += _OnStrategyFinished;
//		}

//		private void Unsubscribe(StrategyBase strat)
//		{
//			strat.OnSucceededHandler -= _OnTargetHit;
//			strat.OnEndedHandler -= _OnStrategyFinished;
//		}

//		public override bool Use(
//			EntityBase source, 
//			EntityBase target)
//		{		
//			if(!(source is CharacterEntity)) return false;
//			if(!_GetWeaponStrategy((CharacterEntity)source, out StrategyBase strat)) return false;
//			_source = (CharacterEntity) source;
//			_direction = (target.ObjectPosition - source.ObjectPosition).normalized;
//			Subscribe(strat);

//			return strat.Use(source, target);
//		}

//		private void _OnTargetHit(StrategyBase strat, EntityBase target)
//		{
//			Unsubscribe(strat);
//			_direction = (target.ObjectInstance.Position - _source.ObjectPosition).normalized;
//			//OnSucceededHandler?.Invoke(this, target);
//		}

//		private void _OnStrategyFinished(StrategyBase strat)
//		{
//			Unsubscribe(strat);
//			OnEndedHandler?.Invoke(this);
//		}

//		// We dont care whihc type of actor
//		// TODO: unsubscribe on action finished as well
//		public override bool Use(EntityBase source)
//		{
//			if(!(source is CharacterEntity)) return false;
//			if(!_GetWeaponStrategy((CharacterEntity)source, out StrategyBase strat)) return false;
//			_source = (CharacterEntity)source;
//			_direction = source.ObjectInstance.Transform.forward;
//			Subscribe(strat);			
//			return strat.Use(source);
//		}
//	}
//}



//namespace Cirrus.Arpg.Objects
//{
//	public partial class EntityBase
//	{

//		public virtual bool UseStrategy(WeaponStrategy strat)
//		{
//			return false;
//		}

//		public virtual void ApplyStrategy(WeaponStrategy strat)
//		{
//			return;
//		}
//	}
//}


//namespace Cirrus.Arpg.Entities.Characters
//{
//	public partial class CharacterEntity
//	{
//		public override bool UseStrategy(WeaponStrategy strat)
//		{
//			//strat.Character = this;
//			return true;
//		}

//		public override void ApplyStrategy(WeaponStrategy strat)
//		{

//		}
//	}
//}