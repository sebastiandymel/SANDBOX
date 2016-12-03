using System;
namespace SEDY.PhoneCore
{
    public interface IMircophoneAccess
    {
        event EventHandler<MicrophoneDataEventArgs> MircophoneDataReceived;
        void StartReceivingDataFromMicrophone();
        void StopReceivingDataFromMicrophone();
        void SetBufferDuration(int milisec);
    }
}
