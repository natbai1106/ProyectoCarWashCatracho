using PRMOVIL2CARWASH.ViewModels;
using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservacion : ContentPage
    {
        public Reservacion()
        {
            InitializeComponent();
            BindingContext = new ReservationViewModel(this);
            

            

        }

        private void SwitchMapa_Toggled(object sender, ToggledEventArgs e)
        {
          
        }
     
       

        private void ListServices_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var viewModel = BindingContext as ServicesViewModels;
            var service = e.Item as Service;
        }



        //private void Handle_SearchButtonPressed(object sender, EventArgs e)
        //{

        //    //var ServicieSearched = Services.Where(c => c.Contains(ServicieSearchBar.Text));
        //    ServicieSearchList.ItemsSource = Services;
        //}
    }
}