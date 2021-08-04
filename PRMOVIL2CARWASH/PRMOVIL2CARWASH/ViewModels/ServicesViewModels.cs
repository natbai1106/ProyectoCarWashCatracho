using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class ServicesViewModels : BaseViewModel
    {
        private ObservableCollection<Service> _services;

        //public ObservableCollection<Service> Services
        //{
        //    get { return _services; }
        //    set { _services = value; OnPropertyChanged(); }
        //}

        private Service _selectedServices;

        //public Service SelectServicies
        //{
        //    get { return _selectedServices; }
        //    set { _selectedServices = value; OnPropertyChanged(); }
        //}

        public ICommand GoToDetailsCommand { private set; get; }

        public INavigation Navigation { get; set; }

        //public ServicesViewModels(INavigation navigation)
        //{
        //    Navigation = navigation;
        //    //GoToDetailsCommand = new Command<Type>(async (pageType) => await GoToDetails(pageType));
            
        //    Services = new ObservableCollection<Service>();
        //    Services.Add(new Service ()
        //    {
        //        idServicios = 1,
        //        nombre_servicio = "Lavado General",
        //        descripcion = "Lavado Exterior Completo",
        //        disponible_domicilio = 1
        //    });

        //    Services.Add(new Service()
        //    {
        //        idServicios = 2,
        //        nombre_servicio = "Limpieza Interior",
        //        descripcion = "Limpieza de toda la zona interior del vehiculo",
        //        disponible_domicilio = 1
        //    });
            
        //    Services.Add(new Service()
        //    {
        //        idServicios = 3,
        //        nombre_servicio = "Lavado Completo",
        //        descripcion = "Lavado completo por dentro y por fuera",
        //        disponible_domicilio = 1
        //    });
            
        //    Services.Add(new Service()
        //    {
        //        idServicios = 4,
        //        nombre_servicio = "Lavado de Motor",
        //        descripcion = "Limpieza completa del motor",
        //        disponible_domicilio = 1
        //    });
           
        //}

            //async Task GoToDetails(Type pageType)
            //{
            //    if (SelectServicie != null)
            //    {
            //        var page = (Page)Activator.CreateInstance(pageType);

            //        page.BindingContext = new ServicesViewModels()
            //        {
            //            idServicios = SelectServicie.idServicios,
            //            nombre_servicio = SelectServicie.nombre_servicio,
            //            descripcion = SelectServicie.descripcion,
            //            disponible_domicilio= SelectServicie.disponible_domicilio
            //        };

            //        await Navigation.PushAsync(page);
            //    }
            //}
        }

        

    }

