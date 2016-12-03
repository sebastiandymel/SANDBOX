using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumVisualizer.Model
{
    public class SpectrumDataCollectedEventArgs: EventArgs
    {
        public double[][] Spectrum { get; private set; }

        public SpectrumDataCollectedEventArgs(double[][] data)
        {
            Spectrum = data;
        }
    }
}
