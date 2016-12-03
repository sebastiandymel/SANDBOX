using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SEDY.PhoneCore.DSP;
using SEDY.PhoneUIToolkit;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace PhoneFramework.Modules.Flickr
{
    /// <summary>
    /// Flickr api description:
    /// http://mashupguide.net/1.0/html/ch04s06.xhtml
    /// https://www.flickr.com/services/feeds/docs/photos_public/
    /// </summary>
    public class FlickrFeedViewModel : ViewModelBase
    {
        // Each query will return max 20 entries
        private readonly string[] queries = new[]
        {
            "?tags=pussycat,baby cat",
            "?tags=kitty,baby,cat",
            "?tags=baby,cat,pussy",
            "?tags=kittens,baby,cute,little"
        };

        private const int MaxCats = 40;
        private const int MaxNameLength = 40;

        private ObservableCollection<FlickrImage> images;

        public override async void Initialize()
        {
            base.Initialize();
            if (Helpers.IsConnectedToNetwork)
            {
                await DownloadImagesFromFlickr();
            }
            else
            {
                MessageBox.Show("No internet access", "Error", MessageBoxButton.OK);
            }
        }

        public ObservableCollection<FlickrImage> Images
        {
            get { return this.images; }
            set
            {
                this.images = value;
                RaisePropertyChanged();
            }
        }

        private async Task DownloadImagesFromFlickr()
        {
            var xmls = new List<string>();
            foreach (var tag in this.queries)
            {
                var client = new WebClient();
                var adress = new Uri("https://api.flickr.com/services/feeds/photos_public.gne" + tag);
                var xmlData = await client.GetStringAsync(adress);
                xmls.Add(xmlData);
            }
            CreateImages(xmls.ToArray());
        }

        private void CreateImages(params string[] xmlData)
        {
            if (xmlData == null || !xmlData.Any())
            {
                Images = null;
            }
            else
            {
                if (Images == null)
                {
                    Images = new ObservableCollection<FlickrImage>();
                }
                else
                {
                    Images.Clear();
                }

                foreach (var xml in xmlData)
                {
                    CreateImageFromXml(xml);
                }
            }
        }

        private void CreateImageFromXml(string xmlData)
        {
            string ns = "http://www.w3.org/2005/Atom";
            XDocument doc = XDocument.Parse(xmlData);
            var entries = doc.Root.Elements(XName.Get("entry", ns));
            foreach (var entry in entries)
            {
                if (Images.Count > FlickrFeedViewModel.MaxCats)
                {
                    return;
                }

                // URL
                string url = string.Empty;
                var links = entry.Elements(XName.Get("link", ns));
                foreach (var link in links)
                {
                    if (link.Attribute("rel").Value == "enclosure")
                    {
                        url = link.Attribute("href").Value;
                    }
                }

                // NAME
                string name = entry.Element(XName.Get("title", ns)).Value;

                if (name.Length > FlickrFeedViewModel.MaxNameLength)
                {
                    name = name.Substring(0, 40) + "...";
                }

                if (!string.IsNullOrEmpty(url) && Images.All(c => c.Url != url))
                {
                    Images.Add(new FlickrImage()
                    {
                        Name = name, 
                        Source = GetImageSource(url),
                        Url = url
                    });
                }
            }
        }

        private static ImageSource GetImageSource(string fileName)
        {
            return new BitmapImage(new Uri(fileName, UriKind.Absolute));
        }
    }
}