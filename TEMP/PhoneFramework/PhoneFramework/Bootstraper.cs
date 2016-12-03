using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace PhoneFramework
{
    public static class Bootstraper
    {
        private static UnityContainer internalContainer;
        
        public static void InitializeIoC(params string[] assemblyNames)
        {
            var container = new UnityContainer();

            //
            // Add all knows assemblies
            //
            var assemblyList = new List<Assembly>{Assembly.GetExecutingAssembly()};
            if (assemblyNames != null)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(c => assemblyNames.Contains(c.GetName().Name));
                assemblyList.AddRange(assemblies);
            }

            //
            // Register types from known assemblies
            //
            container.RegisterTypes(
                AllClasses.FromAssemblies(assemblyList),
                WithMappings.FromMatchingInterface,
                WithName.Default
                );

            //
            // Add special mappings
            // for example: container.RegisterType<IApplicationSettingsManager, ApplicationSettingsManager>(new ContainerControlledLifetimeManager());
            //

            Bootstraper.internalContainer = container;
        }

        /// <summary>
        /// Reference to the UnitityContainer initialized with InitializeIoC() method.
        /// </summary>
        public static UnityContainer Container
        {
            get { return Bootstraper.internalContainer; }
        }
    }
}