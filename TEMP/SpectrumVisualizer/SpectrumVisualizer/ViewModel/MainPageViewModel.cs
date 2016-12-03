using SEDY.PhoneUIToolkit;
using SpectrumVisualizer.Model;
using SpectrumVisualizer.Resources;
using SpectrumVisualizer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpectrumVisualizer
{
    public class MainPageViewModel: ViewModelBase
    {
        private bool isAnalyzing;
        private string startToken;
        private ISpectrumDataProvider spectrumDataProvider;
        private IApplicationSettingsManager applicationSettings;
        private SpectrumDataProviderFactory spectrumDataProviderFactory;
        private IEnumerable<SpectrumItem> spectrum;

        public MainPageViewModel(SpectrumDataProviderFactory spectrumDataProviderFactory, IApplicationSettingsManager settingsProvider)
        {
            this.applicationSettings = settingsProvider;
            this.applicationSettings.SpectrumSourceChanged += OnSpectrumSourceChanged;
            this.applicationSettings.NumberOfBandsChanged += OnNumberOfBandsChanged;
            this.spectrumDataProviderFactory = spectrumDataProviderFactory;
            CreateSpectrumDataProvider();
            StartCommand = new RelayCommand(AnalyzeSpectrum);
            StartStopButtonText = AppResources.StartButton;
        }

        public string StartStopButtonText
        {
            get { return this.startToken; }
            set
            {
                this.startToken = value;
                RaisePropertyChanged("StartStopButtonText");
            }
        }
        
        
        public RelayCommand StartCommand { get; private set; }
        
        public IEnumerable<SpectrumItem> Spectrum 
        {
            get { return this.spectrum; }
            set
            {
                this.spectrum = value;
                RaisePropertyChanged("Spectrum");
            }
        } 
        
        private void AnalyzeSpectrum()
        {
            if (!this.isAnalyzing)
            {
                StartAnalyzingSpectrum();             
            }
            else
            {
                StopAnalyzingSpectrum();               
            }

            this.isAnalyzing = !isAnalyzing;
        }

        private void StartAnalyzingSpectrum()
        {
            StartStopButtonText = AppResources.StopButton;
            spectrumDataProvider.SpectrumDataCollected += OnDataReceived;
            spectrumDataProvider.StartCollectingData();
        }

        private void StopAnalyzingSpectrum()
        {
            StartStopButtonText = AppResources.StartButton;
            spectrumDataProvider.SpectrumDataCollected -= OnDataReceived;
            spectrumDataProvider.StopCollectingData();
        }

        private void OnDataReceived(object sender, SpectrumDataCollectedEventArgs e)
        {
            var receivedSpectrum = e.Spectrum;                       
            var lenght = receivedSpectrum[0].Length;

            if (Spectrum == null || Spectrum.Count() != lenght)
            {
                var items = new List<SpectrumItem>();
                for (int i = 0; i < lenght; i++)
                {
                    items.Add(new SpectrumItem
                    {
                        Value = receivedSpectrum[0][i]
                    });
                }
                Spectrum = items;
            }
            else
            {
                for (int i = 0; i < lenght; i++)
                {
                    var item = Spectrum.ElementAt(i);
                    item.Value = receivedSpectrum[0][i];
                }
            }
        }

        private void OnSpectrumSourceChanged(object sender, EventArgs e)
        {
            if (this.isAnalyzing)
            {
                StopAnalyzingSpectrum();
                CreateSpectrumDataProvider();
                StartAnalyzingSpectrum();
            }
            else
            {
                CreateSpectrumDataProvider();
            }
        }

        private void OnNumberOfBandsChanged(object sender, EventArgs e)
        {
            spectrumDataProvider.SetNumberOfBands(this.applicationSettings.NumberOfBands);
        }

        private void CreateSpectrumDataProvider()
        {
            this.spectrumDataProvider = this.spectrumDataProviderFactory.Create();
            this.spectrumDataProvider.SetNumberOfBands(this.applicationSettings.NumberOfBands);
        }
    }


}
