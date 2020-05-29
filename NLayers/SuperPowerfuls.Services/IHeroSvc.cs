namespace SuperPowerfuls.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using SuperPowerfuls.Core.Models;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IHeroSvc
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped, 
        UriTemplate="json")]
        List<Hero> GetSuperHeroesJson();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "xml")]
        List<Hero> GetSuperHeroesXml();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: agregue aquí sus operaciones de servicio
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    // Puede agregar archivos XSD al proyecto. Después de compilar el proyecto, puede usar directamente los tipos de datos definidos aquí, con el espacio de nombres "SuperPowerfuls.Services.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
