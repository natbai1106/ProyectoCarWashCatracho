using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
   public class ListVehicles
    {
        public class Vehiculo
        {
            public int idVehiculos { get; set; }
            public string numeroPlaca { get; set; }
            public int anio { get; set; }
            public string fotoRuta { get; set; }
            public string observacion { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public string tipoCombustible { get; set; }
            public string nombreVehiculo { get; set; }
        }

        public class Respuesta
        {
            public string status { get; set; }
            public int codeStatus { get; set; }
            public string message { get; set; }
            public bool statusSession { get; set; }
            public object token { get; set; }
        }

        public class Example
        {
            public IList<Vehiculo> vehiculos { get; set; }
            public Respuesta respuesta { get; set; }
        }
    }
}
