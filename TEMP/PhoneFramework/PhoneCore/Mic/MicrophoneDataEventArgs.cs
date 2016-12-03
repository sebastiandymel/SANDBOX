using System;

namespace SEDY.PhoneCore
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
