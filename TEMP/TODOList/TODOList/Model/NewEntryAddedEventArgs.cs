using System;

namespace ListaZakupow
{
    public class NewEntryAddedEventArgs: EventArgs
    {
        private int groupId;

        public NewEntryAddedEventArgs(int groupId)
        {
            this.groupId = groupId;
        }

        public int GroupId
        {
            get { return this.groupId; }
        }
    }
}