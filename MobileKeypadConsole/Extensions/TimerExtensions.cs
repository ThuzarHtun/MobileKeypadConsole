using System.Timers;
using Timer = System.Timers.Timer;

namespace MobileKeypadConsole.Extensions
{
	public static class TimerExtensions
	{
        public static void ToggleTimer(this Timer timer, bool start = true)
        {
            if (timer == null)
            {
                return;
            }

            if (start)
            {
                if (!timer.Enabled)
                {
                    timer.Start();
                }
            }
            else
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                }
            }
        }
    }
}

