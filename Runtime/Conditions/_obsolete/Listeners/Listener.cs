//using Cirrus.Arpg.Entities;
//using System;
//using System.Collections.Generic;

//namespace Cirrus.Arpg.Conditions
//{
//	public class Listener
//	{
//		public Action<IEvent> OnListenedHandler;

//		public ArgsDelegate<EntityObjectBase> OnObjectListenedHandler;

//		public Listener()
//		{

//		}

//		/*
//		 * 
//		 * public bool IsEventHandlerRegistered(Action prospectiveHandler)
//{	
//	if ( this.EventHandler != null )
//	{
//		foreach ( Action existingHandler in this.EventHandler.GetInvocationList() )
//		{
//			if ( existingHandler == prospectiveHandler )
//			{
//				return true;
//			}
//		}
//	}
//	return false;
//}
//		 * 
//		 * */


//		private List<EntityObjectBase> _objectDestroyed = new List<EntityObjectBase>();

//		//private List<EntityComponentBase> _entityDestroyed = new List<EntityComponentBase>();

//		public virtual void AddStopOnDestroyed(EntityObjectBase obj)
//		{
//			_objectDestroyed.Add(obj);
//		}

//		//public virtual void AddStopOnDestroyed(EntityComponentBase ent)
//		//{
//		//	_entityDestroyed.Add(ent);
//		//}

//		public void OnDestroyed(EntityObjectBase obj)
//		{
//			Stop();
//		}

//		public void OnDestroyed(EntityInstanceBase obj)
//		{
//			Stop();
//		}

//		public virtual void Stop()
//		{
//			foreach(var obj in _objectDestroyed)
//			{
//				if(obj == null) continue;
//				obj.onEntityDestroyHandler -= OnDestroyed;
//			}

//			//foreach(var ent in _entityDestroyed)
//			//{
//			//	if(ent == null) continue;
//			//	ent.OnDestroyedHandler -= OnDestroyed;
//			//}
//		}

//		public virtual void Start()
//		{
//			foreach(var obj in _objectDestroyed)
//			{
//				if(obj == null) continue;
//				obj.onEntityDestroyHandler += OnDestroyed;
//			}

//			//foreach(var ent in _entityDestroyed)
//			//{
//			//	if(ent == null) continue;
//			//	ent.OnDestroyedHandler += OnDestroyed;
//			//}
//		}

//	}
//}
