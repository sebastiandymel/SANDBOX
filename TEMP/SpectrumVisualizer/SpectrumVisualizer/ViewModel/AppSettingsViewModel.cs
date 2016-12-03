using SEDY.PhoneUIToolkit;
using SpectrumVisualizer.Infrastructure;
using SpectrumVisualizer.Model;
using System;
using System.Windows.Input;

namespace SpectrumVisualizer.ViewModel
{
    class AppSettingsViewModel: ViewModelBase
    {
        private IApplicationSettingsManager applicationSettingsManager;
        private string selectedSpectrumSource;
        private double nrBands;

        public AppSettingsViewModel(IApplicationSettingsManager appSettingsManager)
        {
            this.applicationSettingsManager = appSettingsManager;
            this.selectedSpectrumSource = this.applicationSettingsManager.SelectedSpectrumSource;
            RaisePropertyChanged("SelectedSpectrumSource");
            UpdateNumberOfBands();
            SetSourceCommand = new RelayCommand<string>(SetSource);
        }

        private void UpdateNumberOfBands()
        {
            this.nrBands = this.applicationSettingsManager.NumberOfBands;
            RaisePropertyChanged("NumberOfBands");
        }

        private void SetSource(string val)
        {
            SelectedSpectrumSource = val;
        }

        public string SelectedSpectrumSource
        {
            get { return this.selectedSpectrumSource; }
            set
            {
                if (this.selectedSpectrumSource != value)
                {
                    this.selectedSpectrumSource = value;
                    this.applicationSettingsManager.SelectedSpectrumSource = value;
                    RaisePropertyChanged();
                    UpdateNumberOfBands();
                }
            }
        }

        public double NumberOfBands
        {
            get { return this.nrBands; }
            set
            {
                if (this.nrBands != value)
                {
                    this.nrBands = value;
                    RaisePropertyChanged("NumberOfBands");
                    this.applicationSettingsManager.NumberOfBands = Convert.ToInt32(value);
                }
            }
        }

        public ICommand SetSourceCommand { get; private set; } 
    }
}
