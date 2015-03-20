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
            #region Vendor
            
            //ANGULAR
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                        "~/Scripts/vendor/angular/angular.js",
                        "~/Scripts/vendor/angular/angular-ui-router.js",
                        "~/Scripts/vendor/angular/angular-cookies.js",
                        "~/Scripts/vendor/angular/angular-bootstrap-switch.js",
                        "~/Scripts/vendor/angular/angular-resource.js"
                       ));                        

            //BOOSTRAP
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                    "~/Scripts/vendor/bootstrap/bootstrap.js",
                    "~/Scripts/vendor/bootstrap/bootstrap-switch.js",
                    "~/Scripts/vendor/bootstrap/ui-bootstrap-tpls-0.12.1.js"
                ));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                            "~/Content/bootstrap/css/bootstrap.css",
                            "~/Content/bootstrap/css/bootstrap-switch.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery/jquery-{version}.js"
                       ));     

            //DASHGUM THEME
            bundles.Add(new StyleBundle("~/css/dashgum").Include(
                            "~/Content/dashgum/css/font-awesome.css"
                        ));

            #endregion Vendor

            #region multi-vagas

            var scriptBundle = new ScriptBundle("~/bundles/multi-vagas-modules")
                .IncludeDirectory("~/Scripts/app/shared", "*.js")
                .IncludeDirectory("~/Scripts/app/login", "*.js")
                .IncludeDirectory("~/Scripts/app/dashboard", "*.js")
                .IncludeDirectory("~/Scripts/app/estacionamento", "*.js")
                .IncludeDirectory("~/Scripts/app/vagas", "*.js")
                .IncludeDirectory("~/Scripts/app/admin", "*.js")
                ;

            scriptBundle.Orderer = new AsIsBundleOrderer();

            bundles.Add(scriptBundle);

            bundles.Add(new ScriptBundle("~/bundles/multi-vagas").Include("~/Scripts/app/multi-vagas.js"));

            bundles.Add(new StyleBundle("~/css/multi-vagas-dashgum").Include(
                            "~/Content/style.css",
                            "~/Content/style-responsive.css"
                        ));

            #endregion multi-vagas
        }
    }
}
