using Cirrus.Arpg.Entities;
using Cirrus.Objects;
using Cirrus.Unity.Numerics;

using System;
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
using System.Collections.Generic;
using UnityEngine.Serialization;
using Cirrus.Unity.Editor;

namespace Cirrus.Arpg.Abilities
{
	[Flags]
	public enum ActiveAbilityImplEventFlags
	{
		Start_Self = 1 << 0,
		StartupLagEnded_Self = 1 << 1,
		StartupLagEnded = 1 << 2,
		EndingLagEnded_Self = 1 << 3,
		End_Self = 1 << 4,
		End = 1 << 5,
		Sustained_Self = 1 << 6,
		Sustained = 1 << 7,
		Empty_Self = 1 << 8,
	}

	public interface IAbilityActionData
	{
		public string Name { get; set; }

		public float Strength { get; set; }

		EffectBase[] Effects { get; set; }

		
		EffectBase[] Asyncs { get; set; }
		
		ConditionBase[] Conditions { get; set; }	

		Range_ Range { get; set; }
		
		EffectorBase[] Effectors { get; set; }
	}

	[Serializable]
	public partial class AbilityActionData : SerializableBase, IAbilityActionData
	{

		[field: SerializeField]
		public string Name { get; set; } = "";


		[field: SerializeField]
		public ConditionBase[] Conditions { get; set; } = new ConditionBase[0];

		[field: SerializeField]
		public EffectBase[] Effects { get; set; } = new EffectBase[0];

		[field: SerializeField]
		public EffectorBase[] Effectors { get; set; } = new EffectorBase[0];

		[field: SerializeField]
		public float Strength { get; set; } = 1;

		[field: SerializeField]
		public EffectBase[] Asyncs { get; set; } = new EffectBase[0];

		[field: SerializeField]
		public Range_ Range { get; set; } = new Range_(- 1,-1);
	}

	[Serializable]
	public partial class AbilityAction
	//: MarkupSerializableBase
	: ISerializationCallbackReceiver
	, IAbilityActionData
	//, IAbilityAction
	{
		[SerializeField]
		[HideInInspector]
		private string _name;

		public ActiveAbilityImplEventFlags flags;

		[SerializeField]
		private AbilityActionImpl _impl;
		
		[SerializeField]
		private AbilityActionData _data;
		private IAbilityActionData _Data => _impl == null ? _data : _impl;
		public string Name
		{
			get => _data.Name;
			set => _data.Name = Name = value;
		}

		public float Strength { get => _Data.Strength; set => _Data.Strength = value; }
		public EffectBase[] Effects { get => _Data.Effects; set => _Data.Effects = value; }
		public EffectBase[] Asyncs { get => _Data.Asyncs; set => _Data.Asyncs = value; }
		public ConditionBase[] Conditions { get => _Data.Conditions; set => _Data.Conditions = value; }
		public Range_ Range { get => _Data.Range; set => _Data.Range = value; }
		public EffectorBase[] Effectors { get => _Data.Effectors; set => _Data.Effectors = value; }

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			_name = _impl != null ? _impl.Name : _name = _data.Name;
		}
		void ISerializationCallbackReceiver.OnAfterDeserialize() { }
	}


	public partial class AbilityActionInstance
	: ResourceInstanceBase<AbilityAction>
	, IEffectContext
	, ICopiable
	{
		public ActiveAbilityImplEventFlags Flags => _resource.flags;

		public IAbilityContext AbilityContext { get; set; }

		public EntityObjectBase Source { get; set; }

		public EntityObjectBase Target { get; set; }

		public List<EntityObjectBase> Targets { get; set; }
		public float Strength { get; set; } = 1;

		public Range_ Range { get; set; }

		public Vector3 Direction { get; set; }

		public List<IEffectInstance> _effectInsts = new List<IEffectInstance>();

		// TODO could we replace with passive ability
		public List<IEffectInstance> _asyncEffectInsts = new List<IEffectInstance>();
	}
}