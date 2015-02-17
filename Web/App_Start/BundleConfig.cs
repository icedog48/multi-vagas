using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class AsIsBundleOrderer : IBundleOrderer
    {
        /// <summary>
        /// Ordena os scripts por funcao: Modules, Services e Controllers
        /// </summary>
        /// <param name="context"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            var modules = new List<BundleFile>();
            var services = new List<BundleFile>();
            var controllers = new List<BundleFile>();

            foreach (var item in files)
            {
                if (item.VirtualFile.Name.Contains("service"))
                {
                    services.Add(item);
                }
                else if (item.VirtualFile.Name.Contains("controller"))
                {
                    controllers.Add(item);
                }
                else
                {
                    modules.Add(item);
                }
            }

            var allFiles = new List<BundleFile>();
            
            allFiles.AddRange(modules);
            allFiles.AddRange(services);
            allFiles.AddRange(controllers);

            return allFiles;
        }
    }

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                        "~/Scripts/vendor/angular/angular.js",
                        "~/Scripts/vendor/angular/angular-route.js",
                        "~/Scripts/vendor/angular/angular-cookies.js"
                       ));

            var scriptBundle = new ScriptBundle("~/bundles/multi-vagas")
                .IncludeDirectory("~/Scripts/app/login", "*.js")
                .IncludeDirectory("~/Scripts/app/shared", "*.js")
                
                .Include("~/Scripts/app/multi-vagas.js")
                ;

            scriptBundle.Orderer = new AsIsBundleOrderer();

            bundles.Add(scriptBundle);
        }
    }
}
