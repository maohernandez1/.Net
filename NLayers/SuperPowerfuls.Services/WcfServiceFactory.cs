namespace SuperPowerfuls.Services
{
    using Microsoft.Practices.Unity;
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Services;
    using SuperPowerfuls.Infraestructure.Implementations;
    using SuperPowerfuls.Infraestructure.TXTContext;
    using Unity.Wcf;
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
			// register all your components with the container here
            container.RegisterType<IHeroSvc, HeroSvc>();
            container.RegisterType<IHeroService, HeroService>();
            container.RegisterType<IHeroRepository, HeroRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }    
}