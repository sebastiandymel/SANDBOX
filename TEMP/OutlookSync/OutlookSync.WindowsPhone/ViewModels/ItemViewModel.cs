using System;
using System.ComponentModel;

namespace OutlookSync.WindowsPhone.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
        private string _from;
        private string _to;
        private string _subject;
        private string _location;
        private string _content;

        public DateTime FromDate { get; set; }

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyPropertyChanged("From");
            }
        }

        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                NotifyPropertyChanged("To");
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                NotifyPropertyChanged("Subject");
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}