using Microsoft.Xna.Framework;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SEDY.PhoneUIToolkit
{
    public sealed class XnaAsyncLoop : IApplicationService
    {
        private DispatcherTimer frameworkDispatcherTimer;

        public XnaAsyncLoop(TimeSpan dispatchInterval)
        {
            FrameworkDispatcher.Update();
            this.frameworkDispatcherTimer = new DispatcherTimer();            
            this.frameworkDispatcherTimer.Interval = dispatchInterval;
        }

        public XnaAsyncLoop(int intervalMs): this(TimeSpan.FromMilliseconds(intervalMs))
        {            
        }

        void IApplicationService.StartService(ApplicationServiceContext context)
        {
            this.frameworkDispatcherTimer.Tick += new EventHandler(OnTimerTick);
            this.frameworkDispatcherTimer.Start();
        }

        void IApplicationService.StopService()
        {
            this.frameworkDispatcherTimer.Tick -= new EventHandler(OnTimerTick);
            this.frameworkDispatcherTimer.Stop();
        }

        static void OnTimerTick(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }
    }
}
