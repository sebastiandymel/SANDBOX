using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace UIFramework.Modules.Modules.Menu
{
    [ModuleExport(typeof (MenuModule))]
    public class MenuModule : ModuleBase<Menu>
    {
        [ImportingConstructor]
        public MenuModule(IRegionManager regionManager)
            : base(regionManager)
        {
        }

        protected override string RegionName
        {
            get { return "Application.Menu"; }
        }

    }
}