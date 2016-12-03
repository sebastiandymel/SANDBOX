using System.Windows.Input;
using SEDY.PhoneUIToolkit;

namespace ListaZakupow.ViewModels
{
    public class Entry : ViewModelBase
    {
        private string description;
        private bool isChecked;
        private bool isEditing;

        public Entry(int parent, string description, bool isChecked, int id)
        {
            ParentId = parent;
            EditEntryCommand = new RelayCommand(Edit);
            StopEditingCommand = new RelayCommand(StopEditing);
            this.description = description;
            this.isChecked = isChecked;
            this.EntryId = id;
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    PersistEntry();
                    IsEditing = false;
                    RaisePropertyChanged();
                }
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

        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    PersistEntry();
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand RemoveEntryCommand { get; set; }
        public ICommand EditEntryCommand { get; set; }

        public ICommand StopEditingCommand { get; set; }

        public int EntryId { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var entry = obj as Entry;

            return entry.Description == this.Description && entry.IsChecked == this.IsChecked;
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(description) ? 0 : description.GetHashCode();
        }

        private int ParentId { get; set; }

        private void PersistEntry()
        {
            App.Model.ModifyEntry(new EntryData(){ Description = this.Description, IsChecked = this.IsChecked, Id = this.EntryId}, ParentId);
        }

        private void Edit()
        {
            IsEditing = true;
        }
        
        private void StopEditing()
        {
            IsEditing = false;
        }

    }
}