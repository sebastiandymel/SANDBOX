using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace OutlookSync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer syncTimer;
        private bool isUpdating;
        private DateTime UpperTimeLimit = DateTime.Now.AddDays(30);
        private DateTime LowerLimit = DateTime.Now.AddHours(-2);
        private int minutesToSync = 60;

        public MainWindow()
        {
            InitializeComponent();
            DatePickerFrom.SelectedDate = LowerLimit;
            DatePickerTo.SelectedDate = UpperTimeLimit;
            SyncTextBox.Text = minutesToSync.ToString();
        }

        private async void OnSync(object sender, RoutedEventArgs e)
        {
            await ExecuteSync();
        }

        private async Task ExecuteSync()
        {
            if (this.isUpdating)
            {
                return;
            }
            Mouse.OverrideCursor = Cursors.Wait;
            this.isUpdating = true;

            try
            {
                var allMeetings = OutlookHelper.GetAllCalendarItems(LowerLimit, UpperTimeLimit);
                var meetingsTable = Service.MobileService.GetTable<Meeting>();

                // DELETE CURRENT MEETINGS
                Log("Removing all the meetings..");
                var currentMeetings = await meetingsTable.ToListAsync();
                foreach (var currentMeeting in currentMeetings)
                {
                    await meetingsTable.DeleteAsync(currentMeeting);
                }

                // ADD NEW ONE
                Log("Adding new meetings...");
                foreach (var meeting in allMeetings)
                {
                    await meetingsTable.InsertAsync(meeting);
                }
                Log("Meetings added.");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.isUpdating = false;
            }
        }

        private void OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate.HasValue)
            {
                LowerLimit = DatePickerFrom.SelectedDate.Value;
            }
            if (DatePickerTo.SelectedDate.HasValue)
            {
                UpperTimeLimit = DatePickerTo.SelectedDate.Value;
            }
        }

        private void OnSynchronizeChecked(object sender, RoutedEventArgs e)
        {
            ToggleSyncTimer();
        }

        private void ToggleSyncTimer()
        {
            if (SyncCheckBox.IsChecked.HasValue && SyncCheckBox.IsChecked.Value)
            {
                this.syncTimer = new DispatcherTimer();
                this.syncTimer.Interval = TimeSpan.FromMinutes(this.minutesToSync);
                this.syncTimer.Tick += OnSyncRequest;
            }
            else
            {
                if (this.syncTimer != null)
                {
                    this.syncTimer.Tick -= OnSyncRequest;
                    this.syncTimer.IsEnabled = false;
                    this.syncTimer.Stop();
                }
            }
        }

        private void OnSyncRequest(object sender, EventArgs e)
        {
            Log("Synchronization...");
            ExecuteSync().Wait();
        }

        private void OnSyncTextChanged(object sender, TextChangedEventArgs e)
        {
            int minutes = 0;
            if (int.TryParse(SyncTextBox.Text, out minutes))
            {
                this.minutesToSync = minutes;
            }
            else
            {
                SyncTextBox.Text = this.minutesToSync.ToString();
            }

            ToggleSyncTimer();
        }

        private string messageLog;
        private void Log(string msg)
        {
            if (string.IsNullOrEmpty(messageLog))
            {
                this.messageLog = msg;
            }
            else
            {
                this.messageLog += Environment.NewLine + msg;
            }
        }
    }
}
