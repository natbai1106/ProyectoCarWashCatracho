using PRMOVIL2CARWASH.ViewModels;
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
    public partial class ResetPassPage : ContentPage
    {
        public ResetPassPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(this);

        }
    }
}