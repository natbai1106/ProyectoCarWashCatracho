using PRMOVIL2CARWASH.ViewModels;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ConfigUsuario), typeof(ConfigUsuario));
        
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
