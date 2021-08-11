using Acr.UserDialogs;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        bool isDelivery;
        Vehiculo vehicleSelected;
        Servicio serviceSelected;
        double subtotal;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public ICommand DeleteServiceCommand { protected set; get; }
        ObservableCollection<Vehiculo> listVehicles;
        ObservableCollection<Servicio> listServices;
        ObservableCollection<Servicio> listServiceSelected;

        public Vehiculo VehicleSelected { get => vehicleSelected; set { SetProperty(ref vehicleSelected, value); GetServicios(); } }

        public ObservableCollection<Vehiculo> ListVehicles
        { get => listVehicles;
            set { SetProperty(ref listVehicles, value); } }

        public ObservableCollection<Servicio> ListServices { get => listServices; set { SetProperty(ref listServices, value); } }
        public bool IsDelivery { get => isDelivery; set {SetProperty(ref isDelivery, value); ChangeMethod(); } }
        public ObservableCollection<Servicio> ListServiceSelected { get => listServiceSelected; set => SetProperty(ref listServiceSelected, value); }
        public double Subtotal { get => subtotal; set => SetProperty(ref subtotal, value); }
        public Servicio ServiceSelected { get => serviceSelected; set { SetProperty(ref serviceSelected, value); AddToList(value); } }

        // internal ObservableCollection ListVehicles { get => listVehicles; set => SetProperty(ref listVehicles, value); }

        Page Page;
        ReservationService VehiclesServices;
        public ReservationViewModel(Page page)
        {
            Page = page;
            VehiclesServices = new ReservationService();
            ListServiceSelected = new ObservableCollection<Servicio>();
            SaveCommand = new Command(OnSaveReservation);
            DeleteServiceCommand = new Command<Servicio>((servicio)=> {
                if (ListServiceSelected != null)
                {
   
                    Subtotal -= servicio.Precio;
                    ListServiceSelected.Remove(servicio);
                }



            });
            LoadData();
        }

        async void LoadData()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            var result = await VehiclesServices.GetResponseVehiclessAsync(IsNotConnect,App.CurrentUser().IdUsuario);
            if (result != null)
                ListVehicles = new ObservableCollection<Vehiculo>(result);
           
            else
                await Page.DisplayAlert("Sin vehiculos","No tienes ningun auto registrado","Aceptar");
            UserDialogs.Instance.HideLoading();
        
        }

        async void GetServicios()
        {
            if(ListServices!=null && ListServices.Count>0 && ListServiceSelected!=null && ListServiceSelected.Count>0)
            {
                bool answer = await Page.DisplayAlert("Advertencia", "Estas reservando para el vehiculo "+VehicleSelected.NombreVehiculo+
                    ", si cambias de vehiculo, perderás los servicios agregados.", "Si", "No");
               if (answer)
                {
                    if(ListServices!=null)
                        ListServices.Clear();
                    
                    if (ListServiceSelected!=null)
                         ListServiceSelected.Clear();
                    LoadServices();
                }
            }

           else {

                LoadServices();
            }
            
            
        }
        async void AddToList(Servicio servicio)
        {
            if (ListServices != null)
            {
                int index = ListServiceSelected.IndexOf(servicio);
                if (index == -1)
                {
                    Subtotal += servicio.Precio;
                    ListServiceSelected.Add(servicio);
                }
                    
                else
                    UserDialogs.Instance.Toast("Ya tienes agregado este servicio.");
               // ListServiceSelected.Add(servicio);
               
            }

        }

        async void OnSaveReservation()
        {
          if(ListServiceSelected!=null || ListServiceSelected.Count>0)
            {

            }
          else
            {
                await Page.DisplayAlert("Advertencia", "No has seleccionado servicios para reservar", "Aceptar");
            }
        }
        async void LoadServices()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            int delivery = IsDelivery ? 1 : 0;

            var result = await VehiclesServices.GetServicesAsync(IsNotConnect, VehicleSelected.IdVehiculos, delivery);
            if (result != null)
            {
                if (ListServices != null)
                    ListServices.Clear();
                ListServices = new ObservableCollection<Servicio>(result);
            }


            else
                await Page.DisplayAlert("Sin servicio", "Lo sentimos, no tenemos servicios disponibles para", "Aceptar");
            UserDialogs.Instance.HideLoading();
        }

        async void ChangeMethod()
        {
            if (IsDelivery)
            {
                bool answer = await Page.DisplayAlert("Advertencia", "Si cambias " + (IsDelivery ? "a servicio a domicilio" : "en carwash") + "hay servicios que no estan disponible, ¿Deseas cambiar el tipo de servicio?", "Si", "No");
                if (answer)
                {
                        if (ListServices != null)
                            ListServices.Clear();

                        if (ListServiceSelected != null)
                            ListServiceSelected.Clear();
                        LoadServices();
                    
                }
            }
            else
            {

                LoadServices();
            }


        }


    }
}
