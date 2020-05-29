namespace SuperPowerfuls.Core.Services
{
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroReposiory;
        public HeroService(IHeroRepository heroReposiory)
        {
            _heroReposiory = heroReposiory;
        }
        public List<Hero> GetAllHeroes()
        {
            return _heroReposiory.GetAll().ToList();
        }
    }
}
