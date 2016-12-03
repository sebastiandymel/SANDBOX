using System;
using System.IO.IsolatedStorage;

namespace SpectrumVisualizer.Model
{
    public class ApplicationSettingsReaderWriter : SpectrumVisualizer.Model.IApplicationSettingsReaderWriter
    {
        private static object lockObj = new object();

        public T ReadValue<T>(string settingToken)
        {
            lock (ApplicationSettingsReaderWriter.lockObj)
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains(settingToken))
                {
                    return (T)Convert.ChangeType(IsolatedStorageSettings.ApplicationSettings[settingToken], typeof(T));
                }
                else if (settingToken == "SelectedSpectrumSource")
                {
                    return (T)Convert.ChangeType("Mock", typeof(T));
                }
                else if (settingToken == "NumberOfBandsMock" || settingToken == "NumberOfBandsMic")
                {
                    return (T)(object) 50;
                }

                throw new NotImplementedException();
            }
        }

        public void SaveValue<T>(string settingToken, T value)
        {
            lock (ApplicationSettingsReaderWriter.lockObj)
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains(settingToken))
                {
                    IsolatedStorageSettings.ApplicationSettings[settingToken] = value;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add(settingToken, value);
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}
