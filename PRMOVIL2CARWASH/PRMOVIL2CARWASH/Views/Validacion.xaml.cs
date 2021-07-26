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
    public partial class Validacion : ContentPage
    {
        public Validacion()
        {
            InitializeComponent();
        }
        public System.Windows.Input.ICommand ClickCommand => new Command<string>((url) =>
        {
            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(url));
        });
    }
}