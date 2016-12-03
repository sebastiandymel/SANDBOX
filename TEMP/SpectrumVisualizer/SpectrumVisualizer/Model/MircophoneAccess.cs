using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SpectrumVisualizer.Model
{
    public class MircophoneAccess : IMircophoneAccess
    {
        private readonly Microphone microphone = Microphone.Default;
        private byte[] buffer;
        private readonly IApplicationService dispatcherService;
        private int bufferDuration = 100;

        public MircophoneAccess()
        {
            this.dispatcherService = new AsyncLoop(TimeSpan.FromMilliseconds(100));
        }

        public void SetBufferDuration(int milisec)
        {
            this.bufferDuration = milisec;
            if (this.microphone.State == MicrophoneState.Started)
            {
                StopReceivingDataFromMicrophone();
                StartReceivingDataFromMicrophone();
            }
        }

        public void StartReceivingDataFromMicrophone()
        {
            if (this.microphone.State == MicrophoneState.Stopped)
            {
                this.dispatcherService.StartService(null);
                this.microphone.BufferDuration = TimeSpan.FromMilliseconds(bufferDuration);
                this.buffer = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
                this.microphone.BufferReady += OnBufferReady;
                this.microphone.Start();                
            }
        }

        private void OnBufferReady(object sender, EventArgs e)
        {
            if (buffer.Length <= 0)
            {
                return;
            }
            // Retrieve audio data
            this.microphone.GetData(buffer);
            RaiseDataReceived(buffer, this.microphone.SampleRate);
        }

        public void StopReceivingDataFromMicrophone()
        {
            if (this.microphone.State == MicrophoneState.Started)
            {
                this.microphone.BufferReady -= OnBufferReady;
                this.microphone.Stop();
                this.dispatcherService.StopService();
            }
        }

        public event EventHandler<MicrophoneDataEventArgs> MircophoneDataReceived;

        private void RaiseDataReceived(byte[] data, int sampleRate)
        {
            var handler = MircophoneDataReceived;
            if (handler != null)
            {
                handler(this, new MicrophoneDataEventArgs(data, sampleRate));
            }
        }

        private sealed class AsyncLoop : IApplicationService
        {
            private DispatcherTimer frameworkDispatcherTimer;

            public AsyncLoop(TimeSpan dispatchInterval)
            {
                FrameworkDispatcher.Update();
                this.frameworkDispatcherTimer = new DispatcherTimer {Interval = dispatchInterval};
            }

            public AsyncLoop(int intervalMs)
                : this(TimeSpan.FromMilliseconds(intervalMs))
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

  
}
