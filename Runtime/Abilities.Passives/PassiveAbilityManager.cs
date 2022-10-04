using Cirrus.Collections;
using Cirrus.Arpg.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	public partial class PassiveAbilityManager
	{
		//protected override void _OnObjectPopulated(ObjectBase source)
		// NOTE: Ensure HUD safety
		//public override void LateStart()
		//{
		//	base.LateStart();

		//	// TODO be careful concurence ModifierUI.SetCharacter and here
		//	foreach (var mod in EntityObject.Resource.Mods)
		//	{
		//		_Add(mod.CreateInstance());
		//	}
		//}
		public bool Intersects(HashSet<Object> others)
		{
			return _passiveAbilitiesReferences.Intersects(others);
		}

		protected override void _OnEntityLateInit(EntityInstanceBase source)
		{
			base._OnEntityLateInit(source);
		
			// TODO be careful concurence ModifierUI.SetCharacter and here
			foreach (var mod in EntityObject.Entity.PassiveAbilities)
			{
				_Add(mod.CreateInstance());
			}
		}

		private void _OnDurationEnded(IPassiveAbilityInstance ab)
		{
			_Unapply(ab);
		}

		private void _Add(IPassiveAbilityInstance ab)
		{
			if (ab.DoApply(EntityObject))
			{
				ab.OnDurationEndedHandler += _OnDurationEnded;
				EntityInst.PassiveAbilities.Add(ab);
				_passiveAbilitiesReferences.Add((Object)ab.AbilityResource);
				OnAbilityAddedHandler?.Invoke(ab);
			}
		}

		public bool _Unapply(IPassiveAbilityInstance ab, int remove = -1)
		{
			if (ab.DoUnapply(EntityObject))
			{
				ab.OnDurationEndedHandler -= _OnDurationEnded;

				if (remove > -1) EntityInst.PassiveAbilities.RemoveAt(remove);
				else EntityInst.PassiveAbilities.Remove(ab);

				_passiveAbilitiesReferences.Remove((Object)ab.AbilityResource);

				//OnModRemovedHandler?.Invoke(mod);
				OnAbilityRemovedHandler?.Invoke(ab);

				return true;
			}

			return false;
		}

		// INVARIANT: Update of same modifier must appear succession
		public bool ApplyUpdates(PassiveAbilityDelta[] updates)
		{
			var results = new PassiveAbilityUpdateMap();
			for (int i = 0; i < updates.Length; i++)
			{
				ApplyUpdate(updates[i], results);
			}

			OnAbilityUpdatedHandler?.Invoke(results);

			return true;
		}

		public bool ApplyUpdate(
			PassiveAbilityDelta update,
			PassiveAbilityUpdateMap results = null)
		{
			var abilities = EntityInst.PassiveAbilities;
			var updated = results == null ?
				new PassiveAbilityUpdateMap() :
				results;

			for (int i = abilities.Count - 1; i >= 0; i--)
			{
				var ab = abilities[i];
				if (ab.References.Intersects(update.References))
				{
					if (_UpdateModifier(ab, update, i))
					{
						if (!updated.TryGetValue(
							ab,
							out BHeap<PassiveAbilityDelta> bheap))
						{
							bheap = new BHeap<PassiveAbilityDelta>(SortDirection.Descending);
							updated.Add(ab, bheap);
						}

						bheap.Insert(update, (float)update.flags);
					}
				}
			}

			//if (results == null) OnModUpdatedHandler?.Invoke(updated);

			return true;
		}

		public bool _UpdateModifier(IPassiveAbilityInstance passive, PassiveAbilityDelta update, int remove = -1)
		{
			if (update.flags.Intersects(PassiveAbilityUpdateFlags.Ended)) return false;

			var mods = EntityInst.PassiveAbilities;
			if (update.flags.Intersects(PassiveAbilityUpdateFlags.Removal))
			{
				return _Unapply(passive, remove);
			}
			if (update.flags.Intersects(PassiveAbilityUpdateFlags.Duration))
			{
				if (passive
					.DoUpdateDuration(EntityObject, update)
					.Intersects(PassiveAbilityUpdateFlags.Removal))
				{
					update.flags |= PassiveAbilityUpdateFlags.Removal;
					return _Unapply(passive, remove);
				}
			}
			//if(update.Flags.Intersects(ModifierUpdateFlags.Strength))
			//{
			//	return mod.UpdateStrength(Entity.Entity, update);
			//}		

			return true;
		}

		private bool _ConsolidateModifier(IPassiveAbilityInstance modifier, IPassiveAbility update)
		{
			//return modifier.Consolidate(EntComp.Resource, update);
			return false;
		}

		public bool Apply(IPassiveAbility passive)
		{
			List<IPassiveAbilityInstance> passives = EntityInst.PassiveAbilities;
			if (passive.PassiveAbilityFlags.Intersects(PassiveAbilityFlags.Unique))
			{
				for (int i = 0; i < passives.Count; i++)
				{
					if (passives[i].References.Intersects(passive.References))
					{
						return false;
					}
				}
			}

			List<PassiveAbilityInteraction>[] interacts = new List<PassiveAbilityInteraction>[passives.Count];
			bool canceled = false;
			for (int i = 0; i < interacts.Length; i++)
			{
				if (passives[i].GetInteracts(
					passive,
					out List<PassiveAbilityInteraction> interactions2))
				{
					if (interactions2 == null) continue;
					foreach (var interaction in interactions2)
					{
						// If strong cancel, abort return early
						if (interaction.Cancelation == PassiveAbilityCancelationMode.Strong) return false;
						if (interaction.Cancelation == PassiveAbilityCancelationMode.Weak) canceled = true;
					}

					interacts[i] = interactions2;
				}
			}

			for (int i = 0; i < interacts.Length; i++)
			{
				if (interacts[i] == null) continue;
				for (int j = 0; j < interacts[i].Count; j++)
				{
					PassiveAbilityInteraction interact = interacts[i][j];

					if (
						interact.Consolidate &&
						_ConsolidateModifier(passives[i], passive))
					// NOTE: usually consolidate is mutually exlusive to update
					// with the same effect.
					// TODO: We can clean this up later..
					{
						canceled = true;
						break;
					}

					for (int k = 0; k < interact.Updates.Length; k++)
					{
						// Includes removals if necessary
						_UpdateModifier(passives[i], interact.Updates[k]);
					}
				}
			}

			if (!canceled)
			{
				_Add(passive.CreateInstance());
			}

			return true;
		}

		public bool Unapply(IPassiveAbility ab)
		{			
			for(int i = 0; i < EntityInst.PassiveAbilities.Count; i++)
			{
				var inst = EntityInst.PassiveAbilities[i];
				if(inst.AbilityResource == ab)
				{
					_Unapply(inst, i);
					return true;
				}
			}

			return false;
		}


		private void _OnAbilityRemoved(PassiveAbilityImplInstance ab)
		{
		}
	}
}
