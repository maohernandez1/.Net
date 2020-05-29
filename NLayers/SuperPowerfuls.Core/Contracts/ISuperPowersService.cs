namespace SuperPowerfuls.Core.Contracts
{
    using SuperPowerfuls.Core.Models;
    using SuperPowerfuls.Core.OutputModels;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ISuperPowersService
    {
        List<Hero> ClassifyByHeroes(List<SuperPowerful> superPowerful);
        List<Villian> ClassifyByVillians(List<SuperPowerful> superPowerful);
        ClassifyOutput GetAllSuperPowerfulsAndClassify();
        int AddVillian(List<Villian> villians);
        int AddHeroes(List<Hero> heroes);
        string AddVilliansAndHeroes(List<Villian> villians, List<Hero> heroes);
    } 
}
