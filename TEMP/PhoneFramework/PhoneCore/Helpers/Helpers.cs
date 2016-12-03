using System.IO;
using System.Reflection;
using NetworkInterface = System.Net.NetworkInformation.NetworkInterface;

namespace SEDY.PhoneCore.DSP
{
    public static class Helpers
    {
        /// <summary>
        /// Returns path to the installed application directory
        /// </summary>
        public static string AppDirectoryPath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            }
        }

        public static bool IsConnectedToNetwork
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable(); 
            }
        }
    }
}