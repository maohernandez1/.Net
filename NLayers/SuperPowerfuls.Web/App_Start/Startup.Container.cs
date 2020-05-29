namespace SuperPowerfuls.Web
{
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using SuperPowerfuls.Web.Services;
    using Owin;
    using SuperPowerfuls.Core.Services;
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Infraestructure.Implementations;
    using SuperPowerfuls.Infraestructure.TXTContext;
    using SuperPowerfuls.Web.WCF_Services;

    /// <summary>
    /// Register types into the Autofac Inversion of Control (IOC) container. Autofac makes it easy to register common 
    /// MVC types like the <see cref="UrlHelper"/> using the <see cref="AutofacWebTypesModule"/>. Feel free to change 
    /// this to another IoC container of your choice but ensure that common MVC types like <see cref="UrlHelper"/> are 
    /// registered. See http://autofac.readthedocs.org/en/latest/integration/aspnet.html.
    /// </summary>
    public partial class Startup
    {
        public static void ConfigureContainer(IAppBuilder app)
        {
            IContainer container = CreateContainer();
            app.UseAutofacMiddleware(container);

            // Register MVC Types 
            app.UseAutofacMvc();
        }

        private static IContainer CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetExecutingAssembly();

            RegisterServices(builder);
            RegisterMvc(builder, assembly);

            IContainer container = builder.Build();

            SetMvcDependencyResolver(container);

            return container;
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<BrowserConfigService>().As<IBrowserConfigService>().InstancePerRequest();
            builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();
            builder.RegisterType<FeedService>().As<IFeedService>().InstancePerRequest();
            builder.RegisterType<LoggingService>().As<ILoggingService>().SingleInstance();
            builder.RegisterType<ManifestService>().As<IManifestService>().InstancePerRequest();
            builder.RegisterType<OpenSearchService>().As<IOpenSearchService>().InstancePerRequest();
            builder.RegisterType<RobotsService>().As<IRobotsService>().InstancePerRequest();
            builder.RegisterType<SitemapService>().As<ISitemapService>().InstancePerRequest();
            builder.RegisterType<SitemapPingerService>().As<ISitemapPingerService>().InstancePerRequest();
            builder.RegisterType<SuperPowersfulService>().As<ISuperPowersService>().InstancePerRequest();
            builder.RegisterType<SuperPowerRepository>().As<ISuperPowerRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<VillianRepository>().As<IVillianRepository>().InstancePerRequest();
            builder.RegisterType<HeroRepository>().As<IHeroRepository>().InstancePerRequest();
            builder.RegisterType<HeroService>().As<IHeroService>().InstancePerRequest();
            builder.RegisterType<SuperPowerfulsService>().As<ISuperPowerfulsService>().InstancePerRequest();
        }

        private static void RegisterMvc(ContainerBuilder builder, Assembly assembly)
        {
            // Register Common MVC Types
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register MVC Filters
            builder.RegisterFilterProvider();

            // Register MVC Controllers
            builder.RegisterControllers(assembly);
        }

        /// <summary>
        /// Sets the ASP.NET MVC dependency resolver.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void SetMvcDependencyResolver(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}