using System.Windows.Controls;
using System.Windows.Data;
using ListaZakupow.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace ListaZakupow
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            App.SetRootNavigation(NavigateToMain);

            // Subscribe to the events
            App.ViewModel.PropertyChanged += OnPropChanged;
            App.ViewModel.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object sender, ViewModels.MainPage.ViewModelEventArgs e)
        {
            if (e.Event == "GroupNameChanged")
            {
                if (this.pivot != null)
                {
                    
                }
            }
        }

        private void NavigateToMain()
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        #region AppBar stuff

        private void OnPropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasAnyItems")
            {
                RefreshAppBarButtons();
            }
        }

        private void RefreshAppBarButtons()
        {
            if (removeAll != null)
            {
                removeAll.IsEnabled = App.ViewModel.HasAnyItems;
            }
        }
        
        private void AppBar_AddNewGroup(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddNewGroup.xaml", UriKind.Relative));
        }

        private void AppBar_RemoveAllGroups(object sender, EventArgs e)
        {
            App.ViewModel.RemoveAll();
        }
        
        private void AppBar_RemoveAllChecked(object sender, EventArgs e)
        {
            App.ViewModel.RemoveAllCheckedInCurrentGroup();
        }

        #endregion

        private void OnDoubleTap(object sender, GestureEventArgs e)
        {
            var item = (sender as FrameworkElement);
            if (item != null)
            {
                var dc = item.DataContext as Entry;
                if (dc != null)
                {
                    dc.EditEntryCommand.Execute(null);
                    return;
                }

                var dcGroup = (item.DataContext as ItemGroup);
                if (dcGroup != null)
                {
                    dcGroup.EditEntryCommand.Execute(null);
                }
            }

            // Note:
            // Set e.Handled to true otherwise it breaks ContextMenu
            e.Handled = true;
        }
    }
}