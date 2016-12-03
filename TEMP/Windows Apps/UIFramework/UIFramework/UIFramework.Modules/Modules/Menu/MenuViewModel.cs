using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Controls;
using UIFramework.Modules.Modules.Views;
using UIToolkit;

namespace UIFramework.Modules.Modules.Menu
{
    [Export]
    public class MenuViewModel: ViewModelBase, IPartImportsSatisfiedNotification
    {
        private string currentView;
        private ObservableCollection<UICommand> menuItems;
        [Import]
        private CompositionContainer Container { get; set; }
        [Import]
        private IContentHost Host { get; set; }
        
        public ObservableCollection<UICommand> MenuItems
        {
            get { return this.menuItems; }
            set
            {
                this.menuItems = value;
                RaisePropertyChanged();
            }
        }

        public void OnImportsSatisfied()
        {
            MenuItems = new ObservableCollection<UICommand>();
            
            // ------------------>
            // VIEWS
            // ------------------>
            var views = new UICommand
                        {
                            Label = "Views",
                            SubCommands =
                                new ObservableCollection<UICommand>(
                                KnownViews.Views.Select(c => new UICommand(new RelayCommand(() => SwitchView(c))) {Label = c}))
                        };

            MenuItems.Add(views);
        }

        private void SwitchView(string viewName)
        {
            if (this.currentView != viewName)
            {
                var view = Container.GetExportedValue<UIViewBase>(viewName);
                Host.SetContent(view);
                this.currentView = viewName;
            }
        }
    }
}