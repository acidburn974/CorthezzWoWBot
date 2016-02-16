using System.Diagnostics;

namespace BotTemplate.Helper
{
    internal class cTimer
    {
        private int wait;
        private Stopwatch watch;

        internal cTimer(int ms)
        {
            wait = ms;
            watch = new Stopwatch();
        }

        internal bool autoReset = true;

        internal bool IsReady()
        {
            if (watch.IsRunning)
            {
                if (watch.Elapsed.TotalMilliseconds > wait)
                {
                    if (autoReset)
                    {
                        watch.Reset();
                        watch.Start();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                watch.Start();
                return false;
            }
        }

        internal void Stop()
        {
            watch.Stop();
        }

        internal void ResetOnly()
        {
            watch.Reset();
        }

        internal void Reset()
        {
            watch.Reset();
            watch.Start();
        }
    }
}
