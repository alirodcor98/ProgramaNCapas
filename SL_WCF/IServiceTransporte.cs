using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceTransporte" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceTransporte
    {
        [OperationContract]
        Result Add(ML.Transporte transporte);

        [OperationContract]
        Result Delete(int idTransporte);

        [OperationContract]
        Result Update(ML.Transporte transporte);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Transporte))]
        Result GetAll();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Transporte))]
        Result GetById(int idTransporte);

    }
}
