namespace SuperPowerfuls.Infraestructure.Implementations
{
    using SuperPowerfuls.Core.Models;
    using SuperPowerfuls.Core.Contracts;
    public class SuperPowerRepository:Repository<SuperPowerful>, ISuperPowerRepository
    {
        public SuperPowerRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }
    }
}
