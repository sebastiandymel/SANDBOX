
using SEDY.PhoneUIToolkit;

namespace SpectrumVisualizer.ViewModel
{
    public class SpectrumItem: ViewModelBase
    {
        private double val;
        public double Value
        {
            get { return val; }
            set 
            { 
                val = value;
                RaisePropertyChanged("Value");
            }
        }
        

    }

}
