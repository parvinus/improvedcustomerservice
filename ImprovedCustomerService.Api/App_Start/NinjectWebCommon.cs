using ImprovedCustomerService.Data.Model;
using ImprovedCustomerService.Data.Repository;
using ImprovedCustomerService.Data.UnitOfWork;
using ImprovedCustomerService.Services.ContactService;
using ImprovedCustomerService.Services.CustomerService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ImprovedCustomerService.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ImprovedCustomerService.Api.App_Start.NinjectWebCommon), "Stop")]

namespace ImprovedCustomerService.Api.App_Start
{
    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Data.Entity;
    using System.Web;

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
                kernel.Bind<IMapper>().ToMethod(ctx => Mapper.Instance);

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
        ///     configure interface-to-implementation mappings so that Ninject can perform dependency injections
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            /* bind repository interface to customer and contact-specific concrete implementations */
            kernel.Bind<IRepository<Customer>>().To<Repository<Customer>>().InRequestScope();
            kernel.Bind<IRepository<Contact>>().To<Repository<Contact>>().InRequestScope();

            /* bind IUnitOfWork interface to concrete implementation */
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            /* bind customer and contact service interfaces to their implementations */
            kernel.Bind<ICustomerService>().To<CustomerService>().InRequestScope();
            kernel.Bind<IContactService>().To<ContactService>().InRequestScope();
        }        
    }
}
