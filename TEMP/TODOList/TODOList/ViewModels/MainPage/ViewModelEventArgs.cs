using System;

namespace ListaZakupow.ViewModels.MainPage
{
    public class ViewModelEventArgs : ViewModelEventArgsBase<string>
    {
        public ViewModelEventArgs(string eventData) : base(eventData)
        {
        }
    }

    public abstract class ViewModelEventArgsBase<T> : EventArgs
    {
        public T Event { get; private set; }

        protected ViewModelEventArgsBase(T eventData)
        {
            this.Event = eventData;
        }
    }
}
