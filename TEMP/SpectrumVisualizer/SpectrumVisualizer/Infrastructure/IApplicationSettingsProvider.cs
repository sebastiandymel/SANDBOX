using System;
namespace SpectrumVisualizer.Model
{
    public interface IApplicationSettingsManager
    {
        string SelectedSpectrumSource { get; set; }
        event EventHandler SpectrumSourceChanged;

        int NumberOfBands { get; set; }
        event EventHandler NumberOfBandsChanged;
    }
}
