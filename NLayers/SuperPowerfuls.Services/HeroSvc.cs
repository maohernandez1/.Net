namespace SuperPowerfuls.Services
{
    using System;
    using System.Collections.Generic;
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class HeroSvc : IHeroSvc
    {
        private readonly IHeroService _heroService;
        public HeroSvc(IHeroService heroService)
        {
            _heroService = heroService;
        }
        public List<Hero> GetSuperHeroesJson()
        {
            return GetSuperHeroes();
        }

        public List<Hero> GetSuperHeroesXml()
        {
            return GetSuperHeroes();
        }

        public List<Hero> GetSuperHeroes()
        {
            var heroes = _heroService.GetAllHeroes();
            return heroes;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        
    }
}
