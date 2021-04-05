using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	public interface IGameTimer
	{
		void SetTickCallback(Func<int, bool> setTimer);
		void SetTimeoutFunc(Func<bool> timerTimeout);
		void StartTimerInSeconds(int gameTimeSeconds);
	}
}
