using System.Collections.Generic;

namespace ListaZakupow
{
    public class SerializerDeserializer : XmlSerializerDeserializer<RootContainer>
    {
        public SerializerDeserializer()
            : base("TodoList", "todolistdata")
        {
            
        }

        public void SerializeData(List<ItemGroupData> itemGroupDatas)
        {
            SerializeData(new RootContainer() {Groups = itemGroupDatas});
        }

        public List<ItemGroupData> Deserialize()
        {
            var baseData = base.DeserializeData();
            if (baseData != null)
            {
                return baseData.Groups;
            }
            return null;
        }
    }

    //public class SerializerDeserializer
    //{
    //    private const string TargetFileName = TargetFolderName + "/todolistdata.dat";
    //    private const string TargetFolderName = "TodoList";
    //    private DataContractSerializer serializer;
    //    private IsolatedStorageFile isolatedStorageFile;

    //    public SerializerDeserializer()
    //    {
    //        serializer = new DataContractSerializer(typeof(RootContainer)); 
    //    }

    //    IsolatedStorageFile IsoFile
    //    {
    //        get
    //        {
    //            if (isolatedStorageFile == null)
    //            {
    //                isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
    //            }
    //            return isolatedStorageFile;
    //        }
    //    }

    //    public void SerializeData(List<ItemGroupData> itemGroupDatas)
    //    {
    //        if (itemGroupDatas == null)
    //        {
    //            if (IsoFile.FileExists(TargetFileName))
    //            {
    //                IsoFile.DeleteFile(TargetFileName);
    //            }
    //            return;
    //        }
            
    //        var sourceData = new RootContainer()
    //                         {
    //                             Groups = itemGroupDatas
    //                         };
            

    //        if (!IsoFile.DirectoryExists(TargetFolderName))
    //        {
    //            IsoFile.CreateDirectory(TargetFolderName);
    //        }
    //        try
    //        {
    //            if (IsoFile.FileExists(TargetFileName))
    //            {
    //                IsoFile.DeleteFile(TargetFileName);
    //            }
    //            using (var targetFile = IsoFile.CreateFile(TargetFileName))
    //            {
    //                serializer.WriteObject(targetFile, sourceData);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            IsoFile.DeleteFile(TargetFileName);
    //        } 
    //    }

    //    public List<ItemGroupData> DeserializeData()
    //    {
    //        RootContainer retVal = null;
    //        if (IsoFile.FileExists(TargetFileName))
    //        {
    //            using (var sourceStream = IsoFile.OpenFile(TargetFileName, FileMode.Open))
    //            {
    //                retVal = (RootContainer) serializer.ReadObject(sourceStream);
    //            }
    //        }

    //        if (retVal != null)
    //        {
    //            return retVal.Groups;
    //        }

    //        return null;
    //    }
    //}
}