using System;
using System.Threading;

namespace Thread_Monitor_TickTock
{
    class TickTock
    {
        int tickFrequency;
        int tockFrequency;
        int duration;
        int pause;

        public TickTock(int tickFrequency, int tockFrequency, int duration, int pause)
        {
            this.tickFrequency = tickFrequency;
            this.tockFrequency = tockFrequency;
            this.duration = duration;
            this.pause = pause;
        }

        public void Beep(int frequency)
        {
            lock (this)
            {
                while (true)
                {
                    Console.Beep(frequency, duration);
                    Thread.Sleep(pause);
                    Monitor.Pulse(this);
                    Monitor.Wait(this);
                }
            }
        }

        public void Run()
        {
            Thread tick = new Thread(() => Beep(tickFrequency));
            Thread tock = new Thread(() => Beep(tockFrequency));

            tick.Start();

            while (tick.ThreadState != ThreadState.WaitSleepJoin)
            {
                continue;
            }

            tock.Start();
        }
    }

    class Program
    {
        static void Main()
        {
            TickTock tt = new TickTock(1500, 2000, 300, 500);
            tt.Run();
        }
    }
}
