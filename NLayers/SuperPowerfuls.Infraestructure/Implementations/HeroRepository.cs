namespace SuperPowerfuls.Infraestructure.Implementations
{
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;
    public class HeroRepository:Repository<Hero>, IHeroRepository
    {
        public HeroRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }
    }
}
