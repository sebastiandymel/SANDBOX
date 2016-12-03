using System;

namespace UIToolkit
{
    public class ActionFailedEventArgs : EventArgs
    {
        public Exception Ex { get; private set; }
        public ActionFailedEventArgs(Exception ex)
        {
            Ex = ex;
        }
    }
}