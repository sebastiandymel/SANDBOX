using System;
using System.Collections.Generic;
using System.Windows;

namespace OutlookSync
{
    public static class OutlookHelper
    {
        public static List<Meeting> GetAllCalendarItems(DateTime from, DateTime to)
        {
            var result = new List<Meeting>();

            Microsoft.Office.Interop.Outlook.Application oApp = null;
            Microsoft.Office.Interop.Outlook.NameSpace mapiNamespace = null;
            Microsoft.Office.Interop.Outlook.MAPIFolder CalendarFolder = null;
            Microsoft.Office.Interop.Outlook.Items outlookCalendarItems = null;

            try
            {
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                mapiNamespace = oApp.GetNamespace("MAPI");
                CalendarFolder = mapiNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);
                outlookCalendarItems = CalendarFolder.Items;
                outlookCalendarItems.IncludeRecurrences = true;

                foreach (Microsoft.Office.Interop.Outlook.AppointmentItem item in outlookCalendarItems)
                {
                    if (item.Start > from && item.End < to)
                    {
                        var meeting = new Meeting
                        {
                            From = item.Start.ToString(),
                            To = item.End.ToString(),
                            Location = item.Location,
                            Subject = item.Subject,
                            IsRecurring = item.IsRecurring,
                            Content = item.Body
                        };
                        result.Add(meeting);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while reading outlook meetings." + ex.Message);
            }

            return result;
        }
    }
}