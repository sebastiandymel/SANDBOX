using Windows.Phone.Media.Capture;
using Lomont;
using System;
using System.Linq;

namespace SpectrumVisualizer.Model
{
    public class MicrophoneSpectrumDataProvider : ISpectrumDataProvider
    {
        private static int MicrophoneSensitivity = 200;
        private static int NumberOfBands = 50;
        private readonly IMircophoneAccess microphoneAccess;
        private readonly LomontFFT fft = new LomontFFT();

        public MicrophoneSpectrumDataProvider(IMircophoneAccess microphoneAccess)
        {
            this.microphoneAccess = microphoneAccess;
        }

        public void StartCollectingData()
        {
            this.microphoneAccess.MircophoneDataReceived += OnMicDataReceived;
            this.microphoneAccess.StartReceivingDataFromMicrophone();
        }

        private void OnMicDataReceived(object sender, MicrophoneDataEventArgs e)
        {
            var buffer = e.Data;
            if (buffer.Length <= 0)
            {
                return;
            }

            // Convert from bytes to doubles
            var sampleBuffer = ExtractValuesFromMicrophoneData(buffer);
            
            // Calculate FFT
            fft.RealFFT(sampleBuffer, true);
            
            // Get signal magnitude
            Algorithms.ToMagnitudeReal(sampleBuffer, false);
            
            // Create output array
            var outputData = new double[1][];
            
            outputData[0] = NormalizeToRelative(AverageInBands(sampleBuffer, NumberOfBands), 1, 200);
            
            // Notify listeners
            RaiseSpectrumDataCollected(outputData);
        }

        private void FlatternPeaks(double[] sampleBuffer, int percent)
        {
            var max = sampleBuffer.Max()*percent/100;
            for (int i = 0; i < sampleBuffer.Length; i++)
            {
                if (sampleBuffer[i] > max)
                {
                    sampleBuffer[i] = max;
                }
            }
        }

        private static double[] ExtractValuesFromMicrophoneData(byte[] buffer)
        {
            double[] sampleBuffer = new double[DSP.FourierTransform.NextPowerOfTwo((uint) buffer.Length)];
            var index = 0;
            double rms = 0;

            for (int i = 0; i < buffer.Length; i += 2)
            {
                var value = Convert.ToDouble(BitConverter.ToInt16(new byte[2] {buffer[i], buffer[i + 1]}, 0));//BitConverter.ToInt16(buffer, i));
                sampleBuffer[index] = value;
                rms += Math.Pow(value, 2);
                index++;
            }

            rms /= (buffer.Length / 2.0);
            if (((int)Math.Floor(Math.Sqrt(rms)) < MicrophoneSensitivity))
            {
                for (int i = 0; i < sampleBuffer.Length; i++)
                {
                    sampleBuffer[i] = 0;
                }
            }

            return sampleBuffer;
        }
        
        private static double[] NormalizeToRelative(double[] bands, int min, int max)
        {
            var maximumValue = bands.Max();
            var minimumValue = bands.Min();
            var output = new double[bands.Length];
            for (int i = 0; i < bands.Length; i++)
            {
                output[i] = (max - min)*(bands[i] - minimumValue)/(maximumValue - minimumValue) + min;
            }
            return output;
        }

        private static double[] AverageInBands(double[] spectrumValues, int nrBands)
        {
            var count = spectrumValues.Length;
            int itemsPerBand = count / nrBands;

            var output = new double[nrBands];

            // ===========================================
            // HACK 
            // ===========================================
            for (int i = 0; i < nrBands; i++)
            {
                double sum = 0;
                var windowSize = (i + 1) * itemsPerBand;
                for (int j = i * itemsPerBand; j < windowSize && j < count; j++)
                {
                    sum += spectrumValues[j];
                }
                output[i] = sum / itemsPerBand;
            }

            return output;
        }

        public void StopCollectingData()
        {
            this.microphoneAccess.MircophoneDataReceived -= OnMicDataReceived;
            this.microphoneAccess.StopReceivingDataFromMicrophone();
        }

        public event EventHandler<SpectrumDataCollectedEventArgs> SpectrumDataCollected;
        
        public void SetNumberOfBands(int nrBands)
        {
            if (nrBands < 1)
            {
                throw new ArgumentException("Invalid number of bands", "nrBands");
            }
            MicrophoneSpectrumDataProvider.NumberOfBands = nrBands;
        }

        private void RaiseSpectrumDataCollected(double[][] data)
        {
            if (SpectrumDataCollected != null)
            {
                SpectrumDataCollected(this, new SpectrumDataCollectedEventArgs(data));
            }
        }
    }
}
