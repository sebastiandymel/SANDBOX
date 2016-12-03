using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumVisualizer.ViewModel
{
    class DesignerSpectrumViewModel: IEnumerable<SpectrumItem>
    {
        private Random rnd = new Random(DateTime.Now.Millisecond);

        public IEnumerable<SpectrumItem> Spectrum { get; set; }
        public DesignerSpectrumViewModel()
        {
            try
            {
                var items = new List<SpectrumItem>();
                var frequency = new[] { 125.0, 250, 500, 1000, 2000, 3000, 5000, 6000, 8000 };
                for (int i = 0; i < frequency.Length; i++)
                {
                    var db = rnd.NextDouble() * 100;
                    //var freq = FrequencyPrettyStringExtension.GetPrettyString(frequency[i]);
                    items.Add(new SpectrumItem { Value = db });
                }
                Spectrum = items;
            }
            catch { }
        }

        public IEnumerator<SpectrumItem> GetEnumerator()
        {
            return Spectrum.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Spectrum.GetEnumerator();
        }
    }

    internal static class FrequencyPrettyStringExtension
    {
        internal static string GetPrettyString(double freq)
        {
            if (freq > 1000)
            {
                return freq / 1000 + "k";
            }
            return freq.ToString();
        }
    }
}
