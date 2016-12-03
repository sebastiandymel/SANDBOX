using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SEDY.PhoneUIToolkit
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private List<INotifyCanExecuteChanged> commands = new List<INotifyCanExecuteChanged>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        protected void RegisterCommand(INotifyCanExecuteChanged cmd)
        {
            if (commands.Contains(cmd))
            {
                return;
            }
            commands.Add(cmd);
        }

        protected void UnregisterCommand(INotifyCanExecuteChanged cmd)
        {
            if (commands.Contains(cmd))
            {
                commands.Remove(cmd);
            }
        }

        protected void NotifyCommandsCanExecuteChanged()
        {
            foreach (var cmd in commands)
            {
                cmd.RaiseCanExecuteChanged();
            }
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Uninitialize()
        {
            if (commands != null)
            {
                commands.Clear();
                commands = null;
            }
        }
    }
}
