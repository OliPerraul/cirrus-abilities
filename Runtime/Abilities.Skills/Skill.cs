//using Cirrus.Arpg.Actions.Items;
//using Cirrus.Events;

using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Unity.Objects;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Cirrus.Arpg.Abilities
{
	public abstract partial class SkillBase 
	: AbilityBase
	{
		public void Select()
		{
		}

		protected override void _PopulateInstance(AbilityInstanceBase inst)
		{
			_PopulateInstance((SkillInstanceBase)inst);
		}

		protected virtual void _PopulateInstance(SkillInstanceBase inst)
		{
		}
	}

	public partial class SkillInstanceBase
	{
		//public Action<bool> OnAvailableHandler { get; set; }

		public virtual void GrantAbility(CharacterObject source)
		{
			if (_source != null)
			{

				//foreach (var listener in _conditionListeners)
				//{
				//	if (listener == null) continue;
				//	listener.UnsubscribeFromTarget();
				//}

				//_conditionListeners.Clear();
			}
		}

		public virtual void ForfeitAbility(CharacterObject source)
		{
			_source = source;

			//if (_conditionListeners == null)
			//	_conditionListeners = new List<ConditionListener>();

			//if (_source != null)
			//{

			//	foreach (var listener in _conditionListeners)
			//	{
			//		if (listener == null) continue;
			//		listener.UnsubscribeFromTarget();
			//	}

			//	_conditionListeners.Clear();
			//}

			//foreach (var cond in Conditions)
			//{
			//	if (cond == null) continue;

			//	var listener = new ConditionListener(_source, cond);
			//	_conditionListeners.Add(listener);

			//	listener.OnListenedHandler += OnConditionedStateChanged;
			//}

			SetAvailable();
		}

		public void SetAvailable()
		{
			// TODO do not check every conditions
			//IsAvailable = false;
			//if (Conditions.All(_source)) IsAvailable = true;
			//OnAvailableHandler?.Invoke(IsAvailable);
		}

		//public void OnConditionedStateChanged(IEvent state)
		//{
		//	// TODO do not check every conditions
		//	SetAvailable();
		//}

		public virtual bool VerifyAvailability(CharacterObject source)
		{
			return true;
			//return IsAvailable;
		}


		// public abstract MenuObjectUIBase StoreMenuReference(SlotUI slot);


		// TODO scene skill??

	}
}
