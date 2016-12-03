using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumVisualizer.Model
{
    public interface ISpectrumDataProvider
    {
        void StartCollectingData();
        void StopCollectingData();
        event EventHandler<SpectrumDataCollectedEventArgs> SpectrumDataCollected;

        void SetNumberOfBands(int nrBands);
    } 
}
