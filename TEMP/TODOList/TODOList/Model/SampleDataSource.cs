using System.Collections.Generic;

namespace ListaZakupow.ViewModels
{
    public static class SampleDataSource
    {
        public static IEnumerable<ItemGroupData> GetSampleData()
        {
            var items = new List<ItemGroupData>();

            items.Add(new ItemGroupData()
                      {
                          GroupName = "Group 1",
                          Entries = new List<EntryData>()
                                    {
                                        new EntryData(){Description = "Description 1", IsChecked = false},
                                        new EntryData(){Description = "Description 2", IsChecked = false},
                                        new EntryData(){Description = "Description 3", IsChecked = false},
                                        new EntryData(){Description = "Description 4", IsChecked = false},
                                        new EntryData(){Description = "Description 5", IsChecked = false},
                                        new EntryData(){Description = "Description 6", IsChecked = true},
                                        new EntryData(){Description = "Description 7", IsChecked = false},
                                        new EntryData(){Description = "Description 8", IsChecked = false},
                                        new EntryData(){Description = "Description 9", IsChecked = true},
                                        new EntryData(){Description = "Description 10", IsChecked = false},
                                        new EntryData(){Description = "Description 11", IsChecked = false},
                                        new EntryData(){Description = "Description 12", IsChecked = true},
                                        new EntryData(){Description = "Description 13", IsChecked = false},
                                        new EntryData(){Description = "Description 14", IsChecked = false},
                                        new EntryData(){Description = "Description 15", IsChecked = false},
                                        new EntryData(){Description = "Description 16", IsChecked = false},
                                        new EntryData(){Description = "Description 17", IsChecked = true},
                                        new EntryData(){Description = "Description 18", IsChecked = false},
                                        new EntryData(){Description = "Description 19", IsChecked = false},
                                    }
                      });

            items.Add(new ItemGroupData()
                      {
                          GroupName = "Group 2",
                          Entries = new List<EntryData>()
                                    {
                                        new EntryData(){Description = "Description 1", IsChecked = false},
                                        new EntryData(){Description = "Description 2", IsChecked = false},
                                        new EntryData(){Description = "Description 3", IsChecked = false},
                                        new EntryData(){Description = "Description 4", IsChecked = false},
                                        new EntryData(){Description = "Description 5", IsChecked = false},
                                        new EntryData(){Description = "Description 6", IsChecked = true},
                                        new EntryData(){Description = "Description 7", IsChecked = false},
                                        new EntryData(){Description = "Description 8", IsChecked = false},
                                        new EntryData(){Description = "Description 9", IsChecked = true},
                                        new EntryData(){Description = "Description 10", IsChecked = false},
                                        new EntryData(){Description = "Description 11", IsChecked = false},
                                        new EntryData(){Description = "Description 12", IsChecked = true},
                                        new EntryData(){Description = "Description 13", IsChecked = false},
                                        new EntryData(){Description = "Description 14", IsChecked = false},
                                        new EntryData(){Description = "Description 15", IsChecked = false},
                                        new EntryData(){Description = "Description 16", IsChecked = false},
                                        new EntryData(){Description = "Description 17", IsChecked = true},
                                        new EntryData(){Description = "Description 18", IsChecked = false},
                                        new EntryData(){Description = "Description 19", IsChecked = false},
                                    }
                      });
            items.Add(new ItemGroupData()
                      {
                          GroupName = "Group 3",
                          Entries = new List<EntryData>()
                                    {
                                        new EntryData(){Description = "Description 1", IsChecked = false},
                                        new EntryData(){Description = "Description 2", IsChecked = false},
                                        new EntryData(){Description = "Description 3", IsChecked = false},
                                        new EntryData(){Description = "Description 4", IsChecked = false},
                                        new EntryData(){Description = "Description 5", IsChecked = false},
                                        new EntryData(){Description = "Description 6", IsChecked = true},
                                        new EntryData(){Description = "Description 7", IsChecked = false},
                                        new EntryData(){Description = "Description 8", IsChecked = false},
                                        new EntryData(){Description = "Description 9", IsChecked = true},
                                        new EntryData(){Description = "Description 10", IsChecked = false},
                                        new EntryData(){Description = "Description 11", IsChecked = false},
                                        new EntryData(){Description = "Description 12", IsChecked = true},
                                        new EntryData(){Description = "Description 13", IsChecked = false},
                                        new EntryData(){Description = "Description 14", IsChecked = false},
                                        new EntryData(){Description = "Description 15", IsChecked = false},
                                        new EntryData(){Description = "Description 16", IsChecked = false},
                                        new EntryData(){Description = "Description 17", IsChecked = true},
                                        new EntryData(){Description = "Description 18", IsChecked = false},
                                        new EntryData(){Description = "Description 19", IsChecked = false},
                                    }
                      });

            var groupCount = 0;
            foreach (var itemGroupData in items)
            {
                itemGroupData.Id = groupCount;
                var entryId = 0;
                foreach (var entryData in itemGroupData.Entries)
                {
                    entryData.Id = entryId;
                    entryId++;
                }
                groupCount++;
            }
     
            return items;
        }
    }
}