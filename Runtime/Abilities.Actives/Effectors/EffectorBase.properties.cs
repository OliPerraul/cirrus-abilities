using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cirrus.Arpg.Abilities
{
	//{
	//	public delegate void OnTargetHitFromObject(BaseObject source, BaseObject target);

	//	public delegate void OnTargetHitFromCharacter(Character source, BaseObject target);

	/*
	 *  Self-targeting
	 *  
	 *  is a targeting paradigm for abilities that 
	 *  requires no direct input from the user upon activation - 
	 *  instead, they automatically select the player's champion as 
	 *  the anchor or target of the spell.
	 *  
	 *  
	 *  Single target 
	 *  
	 *  refers to game elements that affect only one unit when they are cast. 
	 * 
	 * 
	 * 
	 * Direction-targeting 
	 * 
	 * is a targeting paradigm in League of Legends that specifies that abilities
	 * using it require an input direction to be used. The term skillshot is often used to refer to 
	 * projectile-based direction-targeted abilities, such as 
	 * 
	 * 
	*/

	[Serializable]
	public abstract partial class EffectorBase
	: ScriptableObjectBase
	//, ICopiable
	{

		//public Range_ Range { get; set; }

		// TODO refactor finished should be called every time even on hit
		// TODO reuse OnStrategyFinished unsubscribe on hit
		//public Action<StrategyBase, IEffectSource, ObjectBase> OnSucceededHandler;
		// OnEnded != OnFailed. e.g Projectile we do not know if it succeded but we have finished as soon as shot..
		//public Action<StrategyBase> OnEndedHandler;

		//public Vector3 _direction = Vector3.forward;

		//public Vector3 Direction => _direction;

		//public virtual bool IsImmeditate { get => false; set { } }
	}
}