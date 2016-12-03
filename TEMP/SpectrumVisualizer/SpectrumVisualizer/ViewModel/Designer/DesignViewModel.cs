using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumVisualizer.ViewModel
{
    class DesignViewModel
    {
        public IEnumerable<SpectrumItem> Spectrum { get; set; }
        public string StartStopButtonText { get; set; }

        public DesignViewModel()
        {
            StartStopButtonText = "START";
            Spectrum = new DesignerSpectrumViewModel();
        }
    }
}
