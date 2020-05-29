using SuperPowerfuls.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SuperPowerfuls.Web.WCF_Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ISuperPowerfulsService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ISuperPowerfulsService
    {
        [OperationContract]
        List<Hero> GetSuperHeroes();
    }
}
