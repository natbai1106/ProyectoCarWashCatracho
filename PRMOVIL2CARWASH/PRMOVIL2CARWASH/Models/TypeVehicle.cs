using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class TypeVehicle
    {
        public int IdTipoVehicle
        {
            get; set;
        }
        public string NombreTipoVehicle
        {
            get; set;
        }
        public List<TypeVehicle> GetTipos()
        {
            var vTipoVehiculo = new List<TypeVehicle>()
            {
                new TypeVehicle (){ IdTipoVehicle = 1, NombreTipoVehicle = "Turismo"},
                new TypeVehicle (){ IdTipoVehicle = 2, NombreTipoVehicle = "Camioneta"},
                new TypeVehicle (){ IdTipoVehicle = 3, NombreTipoVehicle = "Autobus"},
                new TypeVehicle (){ IdTipoVehicle = 4, NombreTipoVehicle = "PickUp"}
            };
            return vTipoVehiculo;
        }
    }
}
