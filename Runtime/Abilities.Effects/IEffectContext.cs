using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using Cirrus.Unity.Numerics;
using System.Collections.Generic;
using UnityEngine;
//using ObjectEvent = Cirrus.Arpg.Entities.Event;

namespace Cirrus.Arpg.Abilities
{
	public interface IEffectContext
		: IConditionContext
	{
		IAbilityContext AbilityContext { get; set; }

		float Strength { get; set; }

		Range_ Range { get; }

		Vector3 Direction { get; }

		void _OnEffectorResult();

		//void _OnTriggerEnded(AbilityTriggerBase strat);
	}
}
