using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.ViewModels;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambioAceite : ContentPage
    {
        ItemsViewModel _viewModel;
        public CambioAceite()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}