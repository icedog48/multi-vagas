using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DependencyResolution;
using Web.DependencyResolution.Registries;

namespace Tests.Unity.Helpers
{
    public static class IoCHelper
    {
        public static IContainer Initialize () 
        {
            var container = IoC.Initialize();
                container.Configure(c => c.AddRegistry(new NHibernateRegistry(container)));

            return container;
        }
    }
}
