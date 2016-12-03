using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace SEDY.PhoneUIToolkit
{
    /// <summary>
    /// Helper class that shows toast notifications to the user
    /// To enable toast notifications go to Package.appxmanifest and change "Toast capable" to true.
    /// </summary>
    public static class ToastHelper
    {
        /// <summary>
        /// Show text toast to the user.
        /// To enable toast notifications go to Package.appxmanifest and change "Toast capable" to true.
        /// Dy default uses [ToastTemplateType.ToastText02].
        /// </summary>
        /// <param name="header">Message header</param>
        /// <param name="body">Message body</param>
        /// <param name="expiration">
        /// Optional. Setting an expiration time on a toast notification defines the maximum
        /// time the notification will be displayed in action center before it expires and is removed. 
        /// If this property is not set, the notification expires after 7 days and is removed.
        /// Tapping on a toast in action center launches the app and removes it immediately from action center.</param>
        /// <param name="duration">i.e. "long"</param>
        /// <param name="directlyToActionCenter">
        /// Set SuppressPopup = true on the toast in order to send it directly to action center without 
        // producing a popup on the user's phone.
        /// </param>
        public static void ShowToast(string header, string body, int expiration = 3600, string duration = null, bool directlyToActionCenter = false)
        {
            // Using the ToastText02 toast template.
            const ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;

            // Retrieve the content part of the toast so we can change the text.
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            //Find the text component of the content
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");

            // Set the text on the toast. 
            // The first line of text in the ToastText02 template is treated as header text, and will be bold.
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(header));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(body));

            // Set the duration on the toast
            if (!string.IsNullOrEmpty(duration))
            {
                IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
                ((XmlElement) toastNode).SetAttribute("duration", duration);
            }

            // Create the actual toast object using this toast specification.
            ToastNotification toast = new ToastNotification(toastXml);

            // Optional. Setting an expiration time on a toast notification defines the maximum
            // time the notification will be displayed in action center before it expires and is removed. 
            // If this property is not set, the notification expires after 7 days and is removed.
            // Tapping on a toast in action center launches the app and removes it immediately from action center.
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(expiration);

            // Set SuppressPopup = true on the toast in order to send it directly to action center without 
            // producing a popup on the user's phone.
            toast.SuppressPopup = directlyToActionCenter;

            // Display the toast.
            ToastNotificationManager.CreateToastNotifier().Show(toast); 
        }
    }
}