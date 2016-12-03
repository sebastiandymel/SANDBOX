using System;
using System.Windows.Input;
using SEDY.PhoneUIToolkit;

namespace PhoneFramework.ViewModel
{
    internal class PageItem: ViewModelBase
    {
        private string location;
        private string name;

        public PageItem(Action<string> navigateAction)
        {
            this.ShowPage = new RelayCommand(() => navigateAction(Location));
        }

        public ICommand ShowPage { get; private set; }

        public string Location
        {
            get
            {
                return this.location;
                
            }
            set
            {
                this.location = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return this.name;
                
            }

            set
            {
                this.name = value;
                RaisePropertyChanged();
            }
        }

    }
}