using Cirrus.Collections;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Cirrus.Arpg.Abilities
{
	public partial class ActiveAbilityLoadout
	{
	}

	public partial class ActiveAbilityLoadoutInstance
	{

		public ActiveAbilityLoadoutInstance()
		{ 
		}

		public ActiveAbilityLoadoutInstance(ActiveAbilityLoadout resource, CharacterInstanceBase character)
		{
			_resource = resource;
			_list = new List<IActiveAbilityInstance>().Resize(resource.SlotsCount);
			for (int i = 0; i < resource.Count; i++)
			{
				if (character.Get(resource[i], out IActiveAbilityInstance ab))
				{
					_list[i] = ab;
				}
			}
		}


		//object ICloneable.Clone()
		//{
		//	var inst = (AbilityLoadout)MemberwiseClone();
		//	inst._abilities = new IConcreteActiveAbility[Size];
		//	for(int i = 0; i < Size; i++) inst._abilities[i] = _abilities[i];
		//	return inst;
		//}

		public override void Add(IActiveAbilityInstance item)
		{
			for(int i = 0; i < _list.Count; i++)
			{
				if(_list[i] == null)
				{
					_list[i] = item;
					return;
				}
			}
		}

		public bool Set(int index, IActiveAbilityInstance ability)
		{
			if(index < 0) return false;
			if(index >= SlotsCount) return false;
			if(_list[index] != null) return false;
			if(!_list.Contains(ability))
			{
				_list[index] = ability;
				return true;
			}

			return false;
		}

		public bool Get(int index, out IActiveAbilityInstance ab)
		{
			if (index < 0)
			{
				ab = _list[_selectedSlotIndex];
				return true;
			}

			ab = null;
			if (!IsValidAbility(index)) return false;
			ab = _list[index];
			return true;
		}

		public bool IsValidAbility(int index)
		{			
			if (index < 0) return false;
			if (index >= SlotsCount) return false;
			if (_list[index] == null) return false;
			return true;
		}
	}
}
