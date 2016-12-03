using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumVisualizer.Model
{
    public class ApplicationSettingsManager : IApplicationSettingsManager
    {
        private string selectedSpectrumSource;
        private readonly IApplicationSettingsReaderWriter settingsReaderWriter;
        private int nrOfBands;

        public ApplicationSettingsManager(IApplicationSettingsReaderWriter readerWriter)
        {
            this.settingsReaderWriter = readerWriter;
            this.selectedSpectrumSource = readerWriter.ReadValue<string>("SelectedSpectrumSource");
            this.nrOfBands = readerWriter.ReadValue<int>("NumberOfBands" + SelectedSpectrumSource);
        }

        public event EventHandler SpectrumSourceChanged;
        public event EventHandler NumberOfBandsChanged;

        public string SelectedSpectrumSource
        {
            get { return this.selectedSpectrumSource; }
            set
            {
                if (this.selectedSpectrumSource != value)
                {
                    this.selectedSpectrumSource = value;
                    this.settingsReaderWriter.SaveValue<string>("SelectedSpectrumSource", value);
                    this.nrOfBands = settingsReaderWriter.ReadValue<int>("NumberOfBands" + SelectedSpectrumSource);
                    RaiseSpectrumSourceChanged();
                    RaiseNumberOfBandsChanged();
                }
            }
        }
        public int NumberOfBands
        {
            get { return this.nrOfBands; }
            set
            {
                this.nrOfBands = value;
                this.settingsReaderWriter.SaveValue<int>("NumberOfBands" + SelectedSpectrumSource, value);
                RaiseNumberOfBandsChanged();
            }
        }

        private void RaiseSpectrumSourceChanged()
        {
            var handler = SpectrumSourceChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void RaiseNumberOfBandsChanged()
        {
            var handler = NumberOfBandsChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
