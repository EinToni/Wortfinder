using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Wortfinder
{
	public class GameTimer
	{
		private readonly Label remainingTimeLabel;
		private readonly DispatcherTimer timerCountingInSeconds;
		private int timeRemaining = 0;
		private Func<bool> timeout;

		public GameTimer(Label remainingTimeLabel, Func<bool> timeoutFunc)
		{
			timerCountingInSeconds = new DispatcherTimer();
			timerCountingInSeconds.Tick += new EventHandler(DispatcherTimerTick);
			timerCountingInSeconds.Interval = new TimeSpan(0, 0, 1);
			this.remainingTimeLabel = remainingTimeLabel;
			timeout = timeoutFunc;
		}

		public void StartTimerInMinutes(int time)
		{
			timeRemaining = time * 60;
			timerCountingInSeconds.Start();
		}

		public void StopTimer()
		{
			timerCountingInSeconds.Stop();
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
			remainingTimeLabel.Content = timeRemaining.ToString() + "s";
		}
	}
}
