using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public  class ReservationViewModel : BaseViewModel 
    {
        Vehiculo vehicleSelected;

        ObservableCollection<Vehiculo> listVehicles;
       public Vehiculo VehicleSelected { get => vehicleSelected; set { SetProperty(ref vehicleSelected, value); } }
       
        public ObservableCollection<Vehiculo> ListVehicles 
        { get => listVehicles;
         set { SetProperty(ref listVehicles, value); } }

        // internal ObservableCollection ListVehicles { get => listVehicles; set => SetProperty(ref listVehicles, value); }

        Page Page;
        ReservationService VehiclesServices;
       public ReservationViewModel(Page page)
        {
            Page = page;
            VehiclesServices = new ReservationService();
            LoadData();
        }

        async void LoadData()
        {
           
            var result = await VehiclesServices.GetResponseVehiclessAsync(IsNotConnect,App.CurrentUser().IdUsuario);
            if (result != null)
                ListVehicles = new ObservableCollection<Vehiculo>(result);
            else
                await Page.DisplayAlert("Sin vehiculos","No tienes ningun auto registrado","Aceptar");

        
        }
    }
}
