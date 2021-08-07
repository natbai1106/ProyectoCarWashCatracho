using System;
using System.Collections.Generic;
using PRMOVIL2CARWASH.Utils;
using Xamarin.Forms;
using System.Linq;
using PRMOVIL2CARWASH.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class DetailQuoteViewModel : BaseViewModel
    {

        Page Page;
        ObservableCollection<Service> _service;
        ObservableCollection<RegisterVehicles> _vehicles;

        int idDetalleCotizacion;
        int cantidad;
        int idCotizacion;
        int idProductos;

        Service serviceSelected;
        Vehicles vehicleSelected;

        //private Service _selectedServices;

        public Service ServiceSelected
        {
            get => serviceSelected;
            set => SetProperty(ref serviceSelected, value);
        }

        public ObservableCollection<Service> Service
        {
            get => _service;
            set => _service = value;
        }

        public Vehicles VehicleSelected
        {
            get => vehicleSelected;
            set => SetProperty(ref vehicleSelected, value);
        }

        public ObservableCollection<RegisterVehicles> Vehicle
        {
            get => _vehicles;
            set => _vehicles = value;
        }

        public DetailQuoteViewModel(Page Pag)
        {
            

            NameService nameService = new NameService();
            RegisterVehicles registerVehicles = new RegisterVehicles();

            Service = nameService.ObtenerServicios().Result;
            Vehicle = registerVehicles.GetVehicles().Result;
            Page = Pag;
        }


    }
}
