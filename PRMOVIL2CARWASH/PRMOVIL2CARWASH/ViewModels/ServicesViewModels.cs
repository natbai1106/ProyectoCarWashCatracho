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
        ObservableCollection<Service> Items ;

        string date;
        string time;
        Boolean home;
        Service serviceSelected;

        //private Service _selectedServices;

        public Service ServiceSelected
        {
            get => serviceSelected;
            set => SetProperty(ref serviceSelected, value);
        }

        public ObservableCollection<Service> Service
        {
            get => Items;
            set => Items= value;
        }
        
        //public ServicesViewModels(Page Pag)
        //{
        //    Page = Pag;

        //    NameService nameService = new NameService();
        //    Service = nameService.ObtenerServicios();

        //}
        
        //public ICommand GoToDetailsCommand { private set; get; }

        //public INavigation Navigation { get; set; }


     }
        

    }

