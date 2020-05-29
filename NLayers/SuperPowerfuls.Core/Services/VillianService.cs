namespace SuperPowerfuls.Core.Services
{
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class VillianService: IVillianService
    {
        private readonly IVillianRepository _villianRepository;
        public VillianService(IVillianRepository villianRepository)
        {
            _villianRepository = villianRepository;
        }
        public List<Villian> GetAllVillians()
        {
            return _villianRepository.GetAll().ToList();
        }
    }
}
