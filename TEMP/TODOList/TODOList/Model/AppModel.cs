using ListaZakupow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaZakupow
{
    public class AppModel
    {
        #region Private fields

        private bool isDirty;
        private List<ItemGroupData> data;
        private readonly SerializerDeserializer serializer = new SerializerDeserializer();

        #endregion

        #region Constructors

        public AppModel()
        {
            try
            {
                this.data = DeserializeData();
            }
            catch { }

            if (this.data == null)
            {
                this.data = new List<ItemGroupData>(SampleDataSource.GetSampleData());
            }
        }

        #endregion

        #region Public members

        public bool HasData
        {
            get { return this.data != null && this.data.Any(); }
        }

        public void AddEntry(string description, int groupId)
        {
            var group = data.FirstOrDefault(c => c.Id == groupId);
            if (group != null)
            {
                var entryId = GetUniqueEntryId(group);
                var e = new EntryData()
                           {
                               Description = description,
                               Id = entryId
                           };
                if (group.Entries == null)
                {
                    group.Entries = new List<EntryData>();
                }
                group.Entries.Insert(0, e);

                InternalSave();
                RaiseEntryAddedOrRemoved(groupId);
            }
        }

        public void AddGroup(string groupName)
        {
            int id = GetUniqueGroupId();
            if (data == null)
            {
                data = new List<ItemGroupData>();
            }
            data.Add(new ItemGroupData()
                     {
                         GroupName = groupName,
                         Id = id
                     });

            InternalSave();
            RaiseDataChanged();
        }

        public void RemoveEntry(int entryId, int groupId)
        {
            if (this.data == null || !this.data.Any())
            {
                return;
            }
            var groupFound = this.data.FirstOrDefault(c => c.Id == groupId);
            if (groupFound != null)
            {
                var entry = groupFound.Entries.FirstOrDefault(c => c.Id == entryId);
                if (entry != null)
                {
                    groupFound.Entries.Remove(entry);
                    InternalSave();
                    RaiseEntryAddedOrRemoved(groupId);
                }
            }
        }

        public void RemoveGroup(int groupId)
        {
            if (data == null || !data.Any())
            {
                return;
            }
            var groupFound = this.data.FirstOrDefault(c => c.Id == groupId);
            if (groupFound != null)
            {
                this.data.Remove(groupFound);
                InternalSave();
                RaiseDataChanged();
            }
        }

        public void ModifyEntry(EntryData entry, int groupId)
        {
            var groupFound = this.data.FirstOrDefault(c => c.Id == groupId);
            if (groupFound != null)
            {
                if (groupFound.Entries != null)
                {
                    var entryFound = groupFound.Entries.SingleOrDefault(s => s.Id == entry.Id);
                    if (entryFound != null)
                    {
                        entryFound.Description = entry.Description;
                        entryFound.IsChecked = entry.IsChecked;
                        entryFound.Id = entry.Id;
                    }
                }
            }
            InternalSave();
        }

        public void ModifyGroupName(int groupId, string groupName)
        {
            var groupFound = this.data.FirstOrDefault(c => c.Id == groupId);
            if (groupFound != null)
            {
                groupFound.GroupName = groupName;
            }
            InternalSave();
            RaiseGroupNameChanged();
        }
        
        public IEnumerable<ItemGroupData> LoadData()
        {
            if (!this.isDirty)
            {
                return data;
            }

            // Reload from storage
            this.data = DeserializeData();
            this.isDirty = false;
            return this.data;
        }

        public void DeleteAll()
        {
            this.data = null;
            InternalSave();
            RaiseDataChanged();
        }

        #endregion

        #region Events...

        public event EventHandler ModelDataChanged;
        public event EventHandler<NewEntryAddedEventArgs> SingleEntryAddedOrRemoved;
        public event EventHandler GroupNameChanged;
        
        #endregion

        #region Private members

        protected virtual void RaiseGroupNameChanged()
        {
            var handler = GroupNameChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void RaiseDataChanged()
        {
            var handler = ModelDataChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseEntryAddedOrRemoved(int groupId)
        {
            var handler = SingleEntryAddedOrRemoved;
            if (handler != null)
            {
                handler(this, new NewEntryAddedEventArgs(groupId));
            }
        }

        private int GetUniqueGroupId()
        {
            if (this.data == null || !this.data.Any())
            {
                return 0;
            }
            int i = data.Max(c => c.Id);
            return i + 1;
        }

        private static int GetUniqueEntryId(ItemGroupData group)
        {
            if (group.Entries == null || !group.Entries.Any())
            {
                return 0;
            }
            int id = group.Entries.Max(c => c.Id);
            return id + 1;
        }
        
        private List<ItemGroupData> DeserializeData()
        {
            return serializer.Deserialize();
        }

        private void InternalSave()
        {
            serializer.SerializeData(this.data);
            this.isDirty = true;
        }

        #endregion
    }
}