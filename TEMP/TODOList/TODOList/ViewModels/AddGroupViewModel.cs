using System.Windows;
using System.Windows.Input;
using SEDY.PhoneUIToolkit;

namespace ListaZakupow.ViewModels
{
    public class AddGroupViewModel : ViewModelBase
    {
        private string groupName;

        public AddGroupViewModel()
        {
            SaveGroupCommand = new RelayCommand(SaveGroup, CanSaveGroup);
            (SaveGroupCommand as RelayCommand).RaiseCanExecuteChanged();
            CancelCommand = new RelayCommand(App.NavigateToMain);
        }

        public ICommand SaveGroupCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public bool CanSaveGroup()
        {
            return !string.IsNullOrEmpty(GroupName);
        }

        public string GroupName
        {
            get { return this.groupName; }
            set
            {
                this.groupName = value;
                RaisePropertyChanged();
                (SaveGroupCommand as RelayCommand).RaiseCanExecuteChanged();
            }
        }


        private void SaveGroup()
        {
            App.Model.AddGroup(GroupName);
            App.NavigateToMain();
        }
    }
}