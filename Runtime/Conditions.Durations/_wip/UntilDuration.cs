//namespace Cirrus.Arpg.Conditions
//{
//	// Chained duration
//	public class UntilDuration : DurationBase
//	{
//		public DurationBase Predecessor;

//		// Preceded duration must occur before duration
//		public DurationBase Duration;

//		public UntilDuration(DurationBase duration , DurationBase predecessor)
//		{
//			this.Predecessor = predecessor;
//			this.Duration = duration;
//		}

//		public override void OnCloned()
//		{
//			Predecessor.OnEndedHandler += OnPrecedecessorEnded;
//			Duration.OnEndedHandler += (d) => OnEndedHandler?.Invoke(this);
//		}

//		public virtual void OnPrecedecessorEnded(DurationBase d)
//		{
//			Duration.Start();
//		}

//		public override void _Start()
//		{
//			Predecessor.Start();			
//		}

//	}
//}
