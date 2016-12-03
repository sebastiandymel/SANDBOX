using System;

namespace PhoneFramework.ViewModel
{
    internal class NavigationRequest: EventArgs
    {
        public string Adress { get; private set; }

        public NavigationRequest(string adress)
        {
            this.Adress = adress;
        }
    }
}