using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace UIToolkit
{
    public class CommandCanExecuteNotifier
    {
        Dispatcher dispatcher;

        public CommandCanExecuteNotifier()
        {
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Use this method when a thread other than the UI thread updates properties that affect the
        /// CanExecute method for a command. The class must be instantiated on the UI thread.
        /// It forces the CommandManager to raise the RequerySuggested event on the thread that was current
        /// when this class was created. the CanExecute delegates for all commands in the application are
        /// hooked up to this event and will therefore be called.
        /// For all this to work the RequerySuggested event must by fired on the UI thread and that is why
        /// it is important that this class is instantiated on the UI thread.
        /// </summary>
        public void EvaluateCanExecuteOnUIThread()
        {
            if (this.dispatcher != null && !this.dispatcher.CheckAccess())
            {
                this.dispatcher.BeginInvoke
                    (
                        (ThreadStart)(() =>
                        {
                            CommandManager.InvalidateRequerySuggested();
                        })
                    );
            }
            else
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
