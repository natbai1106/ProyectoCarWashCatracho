using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PRMOVIL2CARWASH.Models
{
    public class NameService
    {
        public int IdServicios
        {
            get; set;
        }
        public string NombreServicio
        {
            get; set;
        }
        public ObservableCollection<Service> GetNameService()
        {
            var vNombre = new ObservableCollection<Service>()
            {
                new Service (){ IdServicios = 1, NombreServicio = "Lavado Genral"},
                new Service (){ IdServicios = 2, NombreServicio = "Cambio de aceite"},
                new Service (){ IdServicios = 3, NombreServicio = "Lavado Completo"}
            };
            return vNombre;
        }
    }
}
