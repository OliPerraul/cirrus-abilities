//using Cirrus.Arpg.World.Entities.Actions;
using Cirrus.Arpg.Entities;
using Cirrus.Unity.Editor;
using System;

using UnityEditor;
using UnityEngine;
using static Cirrus.Debugging.DebugUtils;

namespace Cirrus.Arpg.Abilities
{
	[Serializable]
	public partial class ClosestEffector : EffectorBase
	{
		/// <summary>
		/// Returns succesful use
		/// </summary>
		/// <param name="actor"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		// We dont care whihc type of actor
		public override bool Apply(IEffectContext context)
		{
			// TODO: for now we use closest, other options maybe?
			// TODO min range?

			Assert(false, "IEffectContext should have range? what about passive abilities");
			Collider[] colliders = Physics.OverlapSphere(context.Source.Position, 10);

			// TODO use priority instead? e.g healer should still be able to heal each other altough not in priority
			float dist = Mathf.Infinity;
			float cmp = -1f;
			EntityInstanceBase candidate = null;
			foreach(Collider collider in colliders)
			{
				var tg = collider.GetComponentInParent<EntityObjectBase>();

				if(tg == null)
					continue;

				if(tg.gameObject == context.Source.gameObject)
					continue;

				cmp = Vector3.Distance(tg.Position, tg.Position);
				if(dist > cmp)
				{
					dist = cmp;
					//candidate = tg.Resource;
				}
			}

			if(candidate == null)
				return false;


			context._OnEffectorResult();
			return true;
		}
	}
}