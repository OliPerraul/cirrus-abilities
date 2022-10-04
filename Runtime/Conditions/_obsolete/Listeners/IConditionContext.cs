using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cirrus.Arpg.Attributes;
using Cirrus.Arpg.Entities;
using Cirrus.Arpg.Entities.Characters;
using UnityEngine;
using Attribute = Cirrus.Arpg.Attributes.AttributeInstance;
using Comparison = Cirrus.Unity.Numerics.Comparison;

namespace Cirrus.Arpg.Conditions
{
	public interface IConditionContext : IEntityContext
	{
	}
}
