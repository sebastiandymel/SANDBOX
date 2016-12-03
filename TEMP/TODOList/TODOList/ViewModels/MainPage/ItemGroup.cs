using SEDY.PhoneUIToolkit;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ListaZakupow.ViewModels
{
    public class ItemGroup : ViewModelBase
    {
        private bool isEditing;
        private string groupName;
        private ObservableCollection<Entry> items;
        private Entry selectedEntry;
        private string newItem;

        public ItemGroup(string groupName)
        {
            App.Model.SingleEntryAddedOrRemoved += OnNewEntryAdded;
            var addNewItemCommand = new RelayCommand(AddNewItem, CanAddNewItem);
            RegisterCommand(addNewItemCommand);
            AddNewItemCommand = addNewItemCommand;
            EditEntryCommand = new RelayCommand(Edit);
            StopEditingCommand = new RelayCommand(StopEditing);
            this.groupName = groupName;
            RaisePropertyChanged("GroupName");
        }

        public ICommand EditEntryCommand { get; set; }
        public ICommand StopEditingCommand { get; set; }
        public ICommand RemoveGroupCommand { get; set; }
        public ICommand AddNewItemCommand { get; private set; }

        public string NewItem
        {
            get { return this.newItem; }
            set
            {
                this.newItem = value;
                RaisePropertyChanged();
                NotifyCommandsCanExecuteChanged();
            }
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                this.isEditing = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Entry> Entries
        {
            get { return this.items; }
            set
            {
                this.items = value;
                RaisePropertyChanged();

                if (items != null)
                {
                    foreach (var entry in items)
                    {
                        if (entry != null)
                        {
                            entry.RemoveEntryCommand = new RelayCommand(() => RemoveEntry(entry));
                        }
                    }
                }
            }
        }

        public int GroupId { get; set; }
        
        public string GroupName
        {
            get { return this.groupName; }
            set
            {
                if (this.groupName != value)
                {
                    this.groupName = value;
                    RaisePropertyChanged();
                    PersistGroup();
                }
            }
        }

        public Entry SelectedEntry
        {
            get { return this.selectedEntry; }
            set
            {
                this.selectedEntry = value;
                RaisePropertyChanged();
            }
        }

        private void RemoveEntry(Entry entry)
        {
            if (Entries != null && Entries.Any())
            {
                App.Model.RemoveEntry(entry.EntryId, this.GroupId);
            }
        }

        private bool CanAddNewItem()
        {
            return !string.IsNullOrEmpty(NewItem);
        }

        private void AddNewItem()
        {
            string desc = this.newItem;
            NewItem = null;
            App.Model.AddEntry(desc, GroupId);
        }

        private void OnNewEntryAdded(object sender, NewEntryAddedEventArgs e)
        {
            if (e.GroupId == GroupId)
            {
                var groupData = App.Model.LoadData().SingleOrDefault(c => c.Id == GroupId);
                if (groupData != null)
                {
                    Entries =
                        new ObservableCollection<Entry>(
                            groupData.Entries.Select(
                                i => new Entry(GroupId, i.Description, i.IsChecked,i.Id)));
                }
            }
        }

        private void Edit()
        {
            IsEditing = true;
        }

        private void StopEditing()
        {
            IsEditing = false;
        }
        private void PersistGroup()
        {
            App.Model.ModifyGroupName(this.GroupId, this.GroupName);
        }
    }
}