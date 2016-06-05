[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NALOrder.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NALOrder.App_Start.NinjectWebCommon), "Stop")]

namespace NALOrder.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using NALOrder.Utilities;
    using NALOrder.Model;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
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
            kernel.Bind<ILogService>().To<LogService>();
            var logService = kernel.Get<ILogService>();

            kernel.Bind<IProductRepository>().To<ProductRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<IOrderRepository>().To<OrderRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<IOrderDetailRepository>().To<OrderDetailRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<IRolesRepository>().To<RolesRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<IUsersRepository>().To<UserRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>().WithConstructorArgument<ILogService>(logService);
            kernel.Bind<ICountryRepository>().To<CountryRepository>().WithConstructorArgument<ILogService>(logService);
        }
    }
}
