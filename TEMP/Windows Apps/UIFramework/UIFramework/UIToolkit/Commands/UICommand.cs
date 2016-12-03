using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace UIToolkit
{
    public class UICommand : ViewModelBase, ICommand
    {
        private readonly ICommand internalCommand;

        private string label;
        private ObservableCollection<UICommand> subCommands;

        public UICommand() : this(new RelayCommand(() => {}))
        {
            
        }

        public UICommand(ICommand cmd)
        {
            this.internalCommand = cmd;
        }

        public string Label
        {
            get { return this.label; }
            set
            {
                SetBackingValue(ref label, value);
            }
        }

        public ObservableCollection<UICommand> SubCommands
        {
            get { return this.subCommands; }
            set
            {
                bool hasCommandsRef = HasSubCommands;

                SetBackingValue(ref subCommands, value);

                if (hasCommandsRef != HasSubCommands)
                {
                    RaisePropertyChanged("HasSubCommands");
                }
            }
        }

        public bool HasSubCommands
        {
            get { return SubCommands != null && SubCommands.Any(); }
        }

        public bool CanExecute(object parameter)
        {
            return this.internalCommand.CanExecute(parameter);
        }

        public event System.EventHandler CanExecuteChanged
        {
            add { this.internalCommand.CanExecuteChanged += value; }
            remove { this.internalCommand.CanExecuteChanged -= value; }
        }

        public void Execute(object parameter)
        {
            this.internalCommand.Execute(parameter);
        }
    }
}