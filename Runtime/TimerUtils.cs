using Cirrus.Unity;
using Cirrus.Unity.Inputs;
using Cirrus.Unity.Objects;
using System;
using UnityEngine;

namespace Cirrus.Arpg
{
	public static class TimerUtils
	{
		public static void ResetHelper(this Timer timer, Action callback, bool inactiveOnly=true, float limit=-1, bool start=true)
		{
			if (timer == null)
			{
				callback();
			}
			else if (!inactiveOnly || !timer.IsActive)
			{
				timer.Reset(callback, limit, start);
			}
		}
	}
}
