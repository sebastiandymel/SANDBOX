using System.Windows.Media;
using SEDY.PhoneUIToolkit;

namespace PhoneFramework.Modules.Flickr
{
    public class FlickrImage : ViewModelBase
    {
        private ImageSource source;
        private string name;

        public ImageSource Source
        {
            get { return this.source; }
            set
            {
                this.source = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged();
            }
        }

        public string Url { get; set; }
    }
}