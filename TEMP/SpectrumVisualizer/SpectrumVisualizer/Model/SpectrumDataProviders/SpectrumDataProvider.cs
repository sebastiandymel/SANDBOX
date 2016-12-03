using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpectrumVisualizer.Model
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class SpectrumDataProvider : ISpectrumDataProvider
    {
        protected int numberOfBands;
        protected bool isRunning;
        protected Task dataCollectionTask;
        protected CancellationTokenSource token;

        public void StartCollectingData()
        {
            if (this.isRunning)
            {
                StopCollectingData();
            }

            this.isRunning = true;

            if (this.dataCollectionTask == null)
            {
                this.token = new CancellationTokenSource();
                this.dataCollectionTask = new Task(CollectData, this.token.Token);
                this.dataCollectionTask.Start();
            }
        }

        private void CollectData()
        {
            if (!this.isRunning)
            {
                return;
            }
            try
            {
                while (this.isRunning)
                {
                    token.Token.ThrowIfCancellationRequested();

                    var spectrum = GetSpectrumData();
                    Task.Factory.StartNew(() => RaiseSpectrumDataCollected(spectrum.ToArray()));
                    //RaiseSpectrumDataCollected(spectrum.ToArray());

                    token.Token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        protected virtual List<double[]> GetSpectrumData()
        {

            return null;

        }

        public void StopCollectingData()
        {
            this.isRunning = false;
            if (token != null)
            {
                this.token.Cancel();
            }
            this.dataCollectionTask = null;
        }

        private void RaiseSpectrumDataCollected(double[][] data)
        {
            if (SpectrumDataCollected != null)
            {
                SpectrumDataCollected(this, new SpectrumDataCollectedEventArgs(data));
            }
        }

        public event EventHandler<SpectrumDataCollectedEventArgs> SpectrumDataCollected;


        public void SetNumberOfBands(int nrBands)
        {
            numberOfBands = nrBands;
        }
    }

}
