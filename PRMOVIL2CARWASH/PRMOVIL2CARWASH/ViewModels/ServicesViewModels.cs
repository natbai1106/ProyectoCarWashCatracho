using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using PRMOVIL2CARWASH.Utils;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class ServicesViewModels : BaseViewModel
    {
        Page Page;

        ObservableCollection<NameService> servicio;
        NameService serviceSelected;
        string nameService;
        string description;
        string availableHome;

        public ObservableCollection<NameService> Servicio
        {
            get => servicio;
            set => SetProperty(ref servicio, value);
        }

        public NameService ServiceSelected
        {
            get => serviceSelected;
            set => SetProperty(ref serviceSelected, value);
        }
        public string NameService
        {
            get => nameService;
            set => nameService = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
        public string AvaibleHome
        {
            get => availableHome;
            set => availableHome = value;
        }
        public ServicesViewModels(Page pag)
        {
            Page = pag;
            Cargar();
        }

        private async void Cargar()
        {
            NameService service = new NameService();
            Servicio = await service.ObtenerServicio();
        }
    }
}