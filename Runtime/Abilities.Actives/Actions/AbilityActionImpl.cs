using Cirrus.Collections;
using Cirrus.Arpg.Conditions;
using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Debugging;
using System;
using System.Collections;
using UnityEngine;
using static Cirrus.Debugging.DebugUtils;
using Cirrus.Unity.Numerics;
using Cirrus.Unity.Objects;
using System.Linq;
using System.Collections.Generic;

namespace Cirrus.Arpg.Abilities
{
	// TODO: anaction does not need to be cloned if it isa direct action (deterministic)	
	public partial class AbilityActionImpl
	{
		public virtual void OnValidate()
		{
			if(_data.Name == null || _data.Name == "")
				_data.Name = string.Join(", ", _data.Effects.Select(e => e.Name));
		}
	}
}