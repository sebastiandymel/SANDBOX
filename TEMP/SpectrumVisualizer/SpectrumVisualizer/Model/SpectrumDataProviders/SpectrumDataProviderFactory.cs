using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumVisualizer.Model
{
    public class SpectrumDataProviderFactory
    {
        private IMircophoneAccess micAccess;
        private IApplicationSettingsManager settings;

        public SpectrumDataProviderFactory(IMircophoneAccess micAccess, IApplicationSettingsManager settings)
        {
            this.micAccess = micAccess;
            this.settings = settings;
        }

        public ISpectrumDataProvider Create()
        {
            switch (this.settings.SelectedSpectrumSource)
            {
                case "Mock":
                    return new SpectrumDataProviderMock();
                case "Mic":
                    return new MicrophoneSpectrumDataProvider(micAccess);
                default:
                    throw new InvalidOperationException("Unsupported spectrum source: " + this.settings.SelectedSpectrumSource);
            }
        }
    }
}
