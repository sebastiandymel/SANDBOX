using System;
namespace SpectrumVisualizer.Model
{
    public interface IApplicationSettingsReaderWriter
    {
        T ReadValue<T>(string settingToken);
        void SaveValue<T>(string settingToken, T value);
    }
}
