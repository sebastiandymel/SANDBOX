using System;

namespace SpectrumVisualizer.Model
{
    public class MicrophoneDataEventArgs: EventArgs
    {
        public MicrophoneDataEventArgs(byte[] data, int sampleRate)
        {
            Data = data;
            SampleRate = sampleRate;
        }

        public byte[] Data { get; private set; }
        public int SampleRate { get; private set; }
    }
}
