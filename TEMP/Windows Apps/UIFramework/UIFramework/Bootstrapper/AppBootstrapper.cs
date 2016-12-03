using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Windows;

namespace UIFramework
{
    public class AppBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Window>("Shell");
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // This is current assembly 
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AppBootstrapper).Assembly));
            
            // All modules placed inside \DirectoryModules
            var modulesPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DirectoryModules");
            foreach (var module in Directory.GetFiles(modulesPath, "*.dll"))
            {
                this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(module));
            }
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.ComposeExportedValue(Container);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var modulesPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DirectoryModules");
            return new DirectoryModuleCatalog(){ModulePath = modulesPath};
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
    }
}