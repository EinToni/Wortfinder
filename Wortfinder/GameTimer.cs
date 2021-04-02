using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Wortfinder
{
	public class GameTimer
	{
		private readonly DispatcherTimer timerCountingInSeconds;
		private int timeRemaining = 0;
		private Func<bool> timeout;
		private Func<int, bool> tick;

		public GameTimer(DispatcherTimer dispatcher)
		{
			timerCountingInSeconds = dispatcher;
			timerCountingInSeconds.Tick += new EventHandler(DispatcherTimerTick);
			timerCountingInSeconds.Interval = new TimeSpan(0, 0, 1);
		}
		
		public void StartTimerInSeconds(int seconds)
		{
			timeRemaining = seconds;
			timerCountingInSeconds.Start();
		}

		public void StopTimer()
		{
			timerCountingInSeconds.Stop();
		}

		public void SetTickCallback(Func<int, bool> tickFunction)
		{
			tick = tickFunction;
		}

		public void SetTimeoutFunc(Func<bool> timeoutFunc)
		{
			timeout = timeoutFunc;
		}

		private void DispatcherTimerTick(object sender, EventArgs e)
		{
			timeRemaining -= 1;
			if (timeRemaining <= 0)
			{
				timeRemaining = 0;
				StopTimer();
				timeout();
			}
			tick(timeRemaining);
		}
	}
}
