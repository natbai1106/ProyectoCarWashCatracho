using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class RegisterVehicles
    {
        public int IdVehiculos
        {
            get; set;
        }
        public string NumeroPlaca
        {
            get; set;
        }
        public ObservableCollection<Vehicles> GetRegisterVehicles()
        {
            var vNombre = new ObservableCollection<Vehicles>()
            {
                new Vehicles (){ IdVehiculos = 1, NumeroPlaca = "BAA0001 LOCAL"},
                new Vehicles (){ IdVehiculos = 2, NumeroPlaca = "HND0024"},
                new Vehicles (){ IdVehiculos = 3, NumeroPlaca = "SPS0098"}
            };
            return vNombre;
        }
    }
}
