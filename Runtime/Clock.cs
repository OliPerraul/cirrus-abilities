using Cirrus.Unity.Objects;
using System;

namespace Cirrus.Arpg
{
	// https://issuetracker.unity3d.com/issues/automatic-hold-on-busy-dialog-makes-it-very-hard-to-exit-play-mode-if-update-takes-too-long
	// Timers are not good because this makes Update take a long time..
	public class Clock : FoundSingletonComponentBase<Clock>
	{
		public Action OnUpdateHandler;

		public Action OnFixedUpdateHandler;

		//private Mutex _fixedUpdateMutex = new Mutex();
		//private Mutex _updateMutex = new Mutex();

		//private List<Timer> _fixedUpdateTimers = new List<Timer>();
		//private List<Timer> _updateTimers = new List<Timer>();

		//public void AddFixedUpdate(Timer timer)
		//{
		//	_fixedUpdateTimers.Add(timer);
		//}

		//public void AddUpdate(Timer timer)
		//{
		//	_updateTimers.Add(timer);
		//}

		public void Update()
		{
			if(_IsApplicationQuit) return;
			OnUpdateHandler?.Invoke();
		}

		public void FixedUpdate()
		{
			OnFixedUpdateHandler?.Invoke();
		}
	}
}