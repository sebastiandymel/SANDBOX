using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace UIFramework.Modules.Modules
{
    public abstract class ModuleBase<T> : IModule where T:class 
    {
        protected readonly IRegionManager regionManager;

        protected abstract string RegionName { get; }

        public ModuleBase(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionName, typeof(T));
        }
    }
}