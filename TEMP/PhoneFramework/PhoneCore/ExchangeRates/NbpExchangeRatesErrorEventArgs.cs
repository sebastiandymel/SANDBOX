using System;

namespace SEDY.PhoneCore.DSP
{
    public class NbpExchangeRatesErrorEventArgs: EventArgs
    {
        public string ErrorDetails { get; private set; }

        public NbpExchangeRatesErrorEventArgs(string details)
        {
            this.ErrorDetails = details;
        }
    }
}