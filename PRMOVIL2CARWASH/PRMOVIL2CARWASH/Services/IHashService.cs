using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Services
{
    public interface IHashService
    {

        //Se crean dos metodos para que deben ser implementados, uno para obtener el hash de nuestra aplicacion
        //Y el otro para iniciar el servicio de SMS Retriver Reciber
        string GenerateHash();
        void StartSMSRetriverReciber();

    }
}
