using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ListaZakupow
{
    public interface IXmlSerializerDeserializer<T> where T : class
    {
        void SerializeData(T data);
        T DeserializeData();
        Task<T> DeserializeDataAsync();
    }

    public class XmlSerializerDeserializer<T> : IXmlSerializerDeserializer<T> where T: class
    {
        private readonly string directoryName;
        private readonly string fileName;
        private readonly DataContractSerializer serializer;
        private IsolatedStorageFile isolatedStorageFile;

        IsolatedStorageFile IsoFile
        {
            get
            {
                if (isolatedStorageFile == null)
                {
                    isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
                }
                return isolatedStorageFile;
            }
        }
        
        /// <summary>
        /// Creates new generic serializer.
        /// T type need to be marked with [DataContract] and all member need to be marked with [DataMember]
        /// </summary>
        /// <param name="directoryName">Name of the directory</param>
        /// <param name="fileName">Name of the file (NOT FULL PATH)</param>
        public XmlSerializerDeserializer(string directoryName, string fileName)
        {
            this.directoryName = directoryName;
            this.fileName = string.Format("{0}/{1}", directoryName, fileName);
            this.serializer = new DataContractSerializer(typeof(T));
        }

        public virtual void SerializeData(T data)
        {
            if (data == null)
            {
                if (IsoFile.FileExists(fileName))
                {
                    IsoFile.DeleteFile(fileName);
                }
                return;
            }

            if (!IsoFile.DirectoryExists(directoryName))
            {
                IsoFile.CreateDirectory(directoryName);
            }
            try
            {
                if (IsoFile.FileExists(fileName))
                {
                    IsoFile.DeleteFile(fileName);
                }
                using (var targetFile = IsoFile.CreateFile(fileName))
                {
                    serializer.WriteObject(targetFile, data);
                }
            }
            catch (Exception e)
            {
                IsoFile.DeleteFile(fileName);
            }
        }

        public virtual T DeserializeData()
        {
            T retVal = null;
            if (IsoFile.FileExists(fileName))
            {
                using (var sourceStream = IsoFile.OpenFile(fileName, FileMode.Open))
                {
                    retVal = (T)serializer.ReadObject(sourceStream);
                }
            }
            if (retVal != null)
            {
                return retVal;
            }
            return null;
        }

        public virtual async Task<T> DeserializeDataAsync()
        {
            return await Task.Factory.StartNew<T>(DeserializeData);
        }
    }
}