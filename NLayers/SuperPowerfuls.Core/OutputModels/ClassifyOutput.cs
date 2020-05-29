namespace SuperPowerfuls.Core.OutputModels
{
    using SuperPowerfuls.Core.Models;
    using System.Collections.Generic;
    public class ClassifyOutput
    {
        public List<Hero> Heroes { get; set; }
        public List<Villian> Villians { get; set; }
        public List<SuperPowerful> SuperPowerFul { get; set; }
    }
}
