using PRMOVIL2CARWASH.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    public partial class CambioContra : ContentPage
    {
        public CambioContra(string tipo,string user)
        {
            InitializeComponent();
            BindingContext = new ChangePasswordViewModel(this, tipo,user);
        }
    }
}