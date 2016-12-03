using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;
using SEDY.PhoneUIToolkit;

namespace PhoneFramework.Modules.Geolocation
{
    public partial class AddNewLocation : PhoneApplicationPage
    {
        public AddNewLocation()
        {
            InitializeComponent();
            DataContext = Bootstraper.Container.Resolve<AddNewLocationViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var vm = (DataContext as ViewModelBase);
            if (vm != null)
            {
                vm.Initialize();
            }
        }
    }
}