using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Wortfinder
{
	public class GameTimer
	{
		private readonly DispatcherTimer dispatcherTimer;
		private int timeRemaining = 0;
		private int StartTime { get; set; } = 180;
		private Func<string, bool> displayTime = null;
		private Func<bool> timeout = null;

		public GameTimer()
		{
			dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
			
		}

		public void SetDisplayFunc(Func<string, bool> displayFunc)
		{
			displayTime = displayFunc;
		}

		public void SetTimeoutFunc(Func<bool> timeoutFunc)
		{
			timeout = timeoutFunc;
		}

		public void StartTimerInMinutes(int time)
		{
			timeRemaining = time * 60;
			dispatcherTimer.Start();
		}

		public void StopTimer()
		{
			dispatcherTimer.Stop();
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
			displayTime(timeRemaining.ToString());
		}
	}
}
