using SuperPowerfuls.Core.Contracts;
using SuperPowerfuls.Core.Models;
using SuperPowerfuls.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SuperPowerfuls.Web.WCF_Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SuperPowerfulsService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SuperPowerfulsService.svc o SuperPowerfulsService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SuperPowerfulsService : ISuperPowerfulsService
    {
        private readonly IHeroService _heroService;
        public SuperPowerfulsService(IHeroService heroService)
        {
            _heroService = heroService;
        }
        public List<Hero> GetSuperHeroes()
        {
            return _heroService.GetAllHeroes();
        }
    }
}
