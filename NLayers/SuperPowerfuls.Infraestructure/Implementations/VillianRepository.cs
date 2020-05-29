namespace SuperPowerfuls.Infraestructure.Implementations
{
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;
    public class VillianRepository: Repository<Villian>, IVillianRepository
    {
        public VillianRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }
    }
}
