using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    class Service
    {
        public int idServicios { get; set; }
        public string nombre_servicio { get; set; }
        public string descripcion { get; set; }
        public int disponible_domicilio { get; set; }
    }
}
