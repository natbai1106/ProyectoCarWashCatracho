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

        }


        List<string> Services = new List<string>
        {
                "Lavado General" , "Limpieza Interior" , "Lavado Completo" , "Lavado de Motor"
        };
        private void Handle_SearchButtonPressed(object sender, EventArgs e)
        {
            var ServicieSearched = Services.Where(c => c.Contains(ServicieSearchBar.Text));
            ServicieSearchList.ItemsSource = ServicieSearched;
        }
    }
}