using SEDY.PhoneUIToolkit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using ListaZakupow.ViewModels.MainPage;

namespace ListaZakupow.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ItemGroup> groups;
        private ItemGroup selectedGroup;
        private bool hasAnyItems;

        public MainViewModel()
        {
            App.Model.ModelDataChanged += OnModelDataChanged;
            App.Model.GroupNameChanged += OnGroupNameChanged;
            LoadData();
        }

        private void OnGroupNameChanged(object sender, EventArgs e)
        {
            Groups = new ObservableCollection<ItemGroup>(Groups);
        }

        private void OnModelDataChanged(object sender, EventArgs e)
        {
            if (Groups != null)
            {
                Groups.Clear();
            }
            SelectedGroup = null;

            LoadData();
        }

        public ItemGroup SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ItemGroup> Groups
        {
            get { return this.groups; }
            private set
            {
                this.groups = value;
                RaisePropertyChanged();

                if (groups != null)
                {
                    foreach (var itemGroup in groups)
                    {
                        if (itemGroup != null)
                        {
                            itemGroup.RemoveGroupCommand = new RelayCommand(() => RemoveGroup(itemGroup));
                        }
                    }
                }

                HasAnyItems = groups != null && groups.Any();
            }
        }

        public bool HasAnyItems
        {
            get { return this.hasAnyItems; }
            set
            {
                this.hasAnyItems = value;
                RaisePropertyChanged();
            }
        }

        private void RemoveGroup(ItemGroup itemGroup)
        {
            if (Groups != null && Groups.Any())
            {
                if (SelectedGroup == itemGroup)
                {
                    SelectedGroup = Groups.FirstOrDefault();
                }
                App.Model.RemoveGroup(itemGroup.GroupId);
            }
        }
        
        private void LoadData()
        {
            if (App.Model.HasData)
            {
                var data = App.Model.LoadData();
                if (data == null || !data.Any())
                {
                    Groups = null;
                }
                else
                {
                    Groups = new ObservableCollection<ItemGroup>(
                        data.Select(g => new ItemGroup(g.GroupName)
                                         {
                                             GroupId = g.Id,
                                             Entries = g.Entries == null
                                                     ? null
                                                     : new ObservableCollection<Entry>(g.Entries.Select(e =>
                                                 new Entry(g.Id, e.Description, e.IsChecked, e.Id)))
                                         }));
                }
            }
            else
            {
                Groups = null;
            }
        }

        internal void RemoveAll()
        {
            App.Model.DeleteAll();
        }

        public void RemoveAllCheckedInCurrentGroup()
        {
            if (!HasAnyItems)
            {
                return;
            }

            if (SelectedGroup == null || SelectedGroup.Entries == null)
            {
                return;
            }

            var group = SelectedGroup;
            var entities = group.Entries;
            foreach (var entity in entities)
            {
                if (entity.IsChecked)
                {
                    entity.RemoveEntryCommand.Execute(null);
                }
            }
        }

        public event EventHandler<ViewModelEventArgs> StateChanged;

        protected virtual void RaiseStateChange(string eventName)
        {
            var handler = StateChanged;
            if (handler != null) handler(this, new ViewModelEventArgs(eventName));
        }
    }
}