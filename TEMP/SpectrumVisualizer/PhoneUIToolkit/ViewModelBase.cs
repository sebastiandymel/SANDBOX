using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace SEDY.PhoneUIToolkit
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        public void RaisePropertyChanged()
        {
            var stack = new StackTrace(1, true);
            var property = stack.GetFrame(0)
                .GetMethod().Name
                .Replace("set_", string.Empty)
                .Replace("get_", string.Empty);
            RaisePropertyChanged(property);
        }

    }
}
