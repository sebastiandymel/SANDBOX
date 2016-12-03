using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UIFramework.Modules.Modules
{
    [ModuleExport(typeof(HostingFrameModule))]
    public class HostingFrameModule : ModuleBase<HostingFrame>
    {
        [ImportingConstructor]
        public HostingFrameModule(IRegionManager regionManager) : base(regionManager)
        {
        }

        protected override string RegionName
        {
            get { return "Application.Content"; }
        }
    }
}