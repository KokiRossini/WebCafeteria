[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebCafeteria.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebCafeteria.Web.App_Start.NinjectWebCommon), "Stop")]

namespace WebCafeteria.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using WebCafeteria.Datos;
    using WebCafeteria.Datos.Interfaces;
    using WebCafeteria.Datos.Repositorios;
    using WebCafeteria.Servicios.Interfaces;
    using WebCafeteria.Servicios.Servicios;
    using WebCafeteria.Servicios;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepositorioPaises>().To<RepositorioPaises>().InRequestScope();
            kernel.Bind<IServiciosPaises>().To<ServiciosPaises>().InRequestScope();
            kernel.Bind<IRepositorioCategorias>().To<RepositorioCategorias>().InRequestScope();
            kernel.Bind<IServiciosCategorias>().To<ServiciosCategorias>().InRequestScope();
            kernel.Bind<IRepositorioTamaños>().To<RepositorioTamaños>().InRequestScope();
            kernel.Bind<IServiciosTamaños>().To<ServiciosTamaños>().InRequestScope();
            kernel.Bind<IRepositorioCiudades>().To<RepositorioCiudades>().InRequestScope();
            kernel.Bind<IServiciosCiudades>().To<ServiciosCiudades>().InRequestScope();
            kernel.Bind<IRepositorioProveedores>().To<RepositorioProveedores>().InRequestScope();
            kernel.Bind<IServiciosProveedores>().To<ServiciosProveedores>().InRequestScope();
            kernel.Bind<IRepositorioProductos>().To<RepositorioProductos>().InRequestScope();
            kernel.Bind<IServiciosProductos>().To<ServiciosProductos>().InRequestScope();
            kernel.Bind<IRepositorioVentas>().To<RepositorioVentas>().InRequestScope();
            kernel.Bind<IServiciosVentas>().To<ServiciosVentas>().InRequestScope();
            kernel.Bind<IRepositorioDetalleVentas>().To<RepositorioDetalleVentas>().InRequestScope();
            kernel.Bind<IRepositorioTamañosProductos>().To<RepositorioTamañosProductos>().InRequestScope();
            kernel.Bind<IServiciosTamañosProductos>().To<ServiciosTamañosProductos>().InRequestScope();
            kernel.Bind<IRepositorioClientes>().To<RepositorioClientes>().InRequestScope();
            kernel.Bind<IServiciosClientes>().To<ServiciosClientes>().InRequestScope();
            kernel.Bind<IRepositorioCarritos>().To<RepositorioCarritos>().InRequestScope();
            kernel.Bind<IServiciosCarrito>().To<ServiciosCarritos>().InRequestScope();




            kernel.Bind<CafeteriaDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
        }
    }
}