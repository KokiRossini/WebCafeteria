using System.Web.Optimization;

namespace TiendaVirtual.Web
{
    public class BundleConfig
    {
        // Para obtener m�s informaci�n sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versi�n de desarrollo de Modernizr para desarrollar y obtener informaci�n sobre los formularios.  De esta manera estar�
            // para la producci�n, use la herramienta de compilaci�n disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.js",
                      "~/Scripts/WebCafeteria.js",
                      "~/Scripts/fontawesome/all.min.js",
                      "~/Scripts/sweetalert2.all.min.js"


                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/PagedList.css",
                      "~/Content/all.min.css"));
        }
    }
}
