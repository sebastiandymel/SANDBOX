using System;
using System.Collections.Generic;
using System.Threading;

namespace SpectrumVisualizer.Model
{
    public class SpectrumDataProviderMock : SpectrumDataProvider
    {
        private Random rnd = new Random(DateTime.Now.Millisecond);

        protected override List<double[]> GetSpectrumData()
        {
            Thread.Sleep(300);
            var spectrum = new List<double[]>();

            List<double> values;

            if (this.numberOfBands > 20)
            {
                values = GetRandomValuesEmphasizeSpeech();
            }
            else
            {
                values = GetRandomValuesFlat();
            }

            spectrum.Add(values.ToArray());
            return spectrum;
        }

        private List<double> GetRandomValuesFlat()
        {
            var values = new List<double>();
            for (int i = 0; i < this.numberOfBands; i++)
            {
                values.Add(rnd.NextDouble() * 80);
            }
            return values;
        }

        private List<double> GetRandomValuesEmphasizeSpeech()
        {
            var values = new List<double>();
            var lastThird = this.numberOfBands*2/3;
            var firstThird = this.numberOfBands/3;

            for (int i = 0; i < this.numberOfBands; i++)
            {
                if (i < firstThird)
                {
                    values.Add(rnd.NextDouble()*70);
                }
                else if (i > firstThird && i < lastThird)
                {
                    values.Add(Math.Min((rnd.NextDouble() + 0.3)*100, 100));
                }
                else
                {
                    values.Add(rnd.NextDouble()*20);
                }
            }
            return values;
        }
    }

}
