using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;
using SpectrumVisualizer.ViewModel;

namespace SpectrumVisualizer.View
{
    public partial class AppSettings : PhoneApplicationPage
    {
        public AppSettings()
        {
            InitializeComponent();
            DataContext = App.Container.Resolve<AppSettingsViewModel>();
        }
    }
}