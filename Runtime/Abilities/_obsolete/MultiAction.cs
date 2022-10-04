//using Cirrus.Collections;
//using Cirrus.Unity.Objects;
//using Cirrus.Objects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Cirrus.Arpg.Abilities
//{
//	public partial class MultiAction : Action
//	{
//		public ICollection<Action> Actions = CollectionUtils.Empty<Action>();

//		public override Action Clone_()
//		{
//			var instance = (MultiAction)base.Clone_();
//			instance.Actions = new List<Action>();
//			foreach(var action in Actions)
//			{
//				instance.Actions.Add(action.Clone());
//			}
//			return instance;
//		}

//		public override void Use()
//		{
//			foreach(var action in Actions)
//			{
//				action.Use();
//			}
//		}

//		public override void Use(EntityBase source)
//		{
//			foreach(var action in Actions)
//			{
//				action.Use(source);
//			}
//		}

//		public override void Use(EntityBase source, EntityBase target)
//		{
//			foreach(var action in Actions)
//			{
//				action.Use(source, target);
//			}
//		}

//		public override void Use(EntityBase target)
//		{
//			foreach(var action in Actions)
//			{
//				action.Use(target);
//			}
//		}
//	}
//}
