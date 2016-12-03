using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using OutlookSync.WindowsPhone.Model;

namespace OutlookSync.WindowsPhone.ViewModels
{
    public class MainViewModel
    {
        private const string fileName = "meetingscache";
        private readonly DataSaver<Meetings> dataSerializer = new DataSaver<Meetings>(); 

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();

            var deserialized = this.dataSerializer.LoadMyData(fileName);
            if (deserialized != null && deserialized.Items != null)
            {
                FillMeetings(deserialized.Items);
            }

            Task.Factory.StartNew(GetMeetingsFromService);
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }
        
        private async void GetMeetingsFromService()
        {
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            try
            {
                var allMeetings = await ServiceWrapper.GetAllMeetings();

                Deployment.Current.Dispatcher.BeginInvoke(() => FillMeetings(allMeetings));
                
                this.dataSerializer.SaveMyData(new Meetings{Items =  allMeetings.ToArray()}, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while reading data from service" + ex.Message);
            }
        }

        private void FillMeetings(IEnumerable<Meeting> allMeetings)
        {
            Items.Clear();

            var temp = new List<ItemViewModel>();
            int count = 0;
            foreach (var meeting in SkipPast(allMeetings))
            {
                var item = new ItemViewModel();
                item.FromDate = DateTime.Parse(meeting.From);
                item.ID = (count++).ToString();
                item.From = ParseDate(meeting.From);
                item.To = ParseDate(meeting.To);
                item.Location = meeting.Location;
                item.Subject = meeting.Subject;
                item.Content = meeting.Content;
                temp.Add(item);
            }

            foreach (var item in temp.OrderBy(c => c.FromDate))
            {
                Items.Add(item);
            }
        }

        private static IEnumerable<Meeting> SkipPast(IEnumerable<Meeting> allMeetings)
        {
            return allMeetings.Where(c => string.IsNullOrEmpty(c.To) || DateTime.Parse(c.To) > DateTime.Now);
        }

        private static string ParseDate(string dateString)
        {
            DateTime dt;
            if (DateTime.TryParse(dateString, out dt))
            {
                if (DateTime.Now.Date == dt.Date)
                {
                    return dt.ToShortTimeString();
                }
                else
                {
                    return dt.ToString("g");
                }
            }
            return dateString;
        }
    }
}