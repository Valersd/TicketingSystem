[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TicketingSystem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TicketingSystem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TicketingSystem.Web.App_Start
{
    using System;
    using System.Web;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using TicketingSystem.Models;
    using TicketingSystem.Data;

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
            kernel.Bind<ITicketingSystemData>().To<TicketingSystemData>();
            kernel.Bind<DbContext>().To<TicketingSystemDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));

            kernel.Bind(typeof(IUserStore<User>)).To(typeof(UserStore<User>)).WithConstructorArgument("context", kernel.Get<DbContext>());
            //kernel.Bind<UserManager<User>>().ToSelf();

            kernel.Bind(typeof(IRoleStore<IdentityRole>)).To(typeof(RoleStore<IdentityRole>)).WithConstructorArgument("context", kernel.Get<DbContext>());
            kernel.Bind<RoleManager<IdentityRole>>().ToSelf().WithConstructorArgument("store",kernel.Get<RoleStore<IdentityRole>>());
        }        
    }
}
