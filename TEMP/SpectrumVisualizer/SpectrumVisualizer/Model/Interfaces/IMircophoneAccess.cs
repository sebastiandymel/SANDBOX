using System;
namespace SpectrumVisualizer.Model
{
    public interface IMircophoneAccess
    {
        event EventHandler<MicrophoneDataEventArgs> MircophoneDataReceived;
        void StartReceivingDataFromMicrophone();
        void StopReceivingDataFromMicrophone();
        void SetBufferDuration(int milisec);
    }
}
