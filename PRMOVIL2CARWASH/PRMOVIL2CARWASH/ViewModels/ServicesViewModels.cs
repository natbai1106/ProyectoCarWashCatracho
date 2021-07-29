using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    class ServicesViewModels : BaseViewModel
    {
        public ObservableCollection<Service> Items { get; set; }

        public ServicesViewModels()
        {
            this.Items = new ObservableCollection<Service>
        {
            new Service { idServicios=1,  nombre_servicio= "Lavado General", descripcion = "Lavado Exterior Completo",disponible_domicilio =1 },
            new Service { idServicios=2,  nombre_servicio= "Limpieza Interior", descripcion = "Limpieza de toda la zona interior del vehiculo",disponible_domicilio =1 },
            new Service { idServicios=3,  nombre_servicio= "Lavado Completo", descripcion = "Lavado completo por dentro y por fuera",disponible_domicilio =1 },
            new Service { idServicios=4,  nombre_servicio= "Lavado de Motor", descripcion = "Limpieza completa del motor",disponible_domicilio =1 },
           
        };
        }

        

    }
}
