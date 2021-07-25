using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Facturacion : ContentPage
    {
        public Facturacion()
        {
            InitializeComponent();

            pckServicio.Items.Add("Cambio de Aceite");
            pckServicio.Items.Add("Lavado y Aspirado");
            pckServicio.Items.Add("Shamposeado");
            pckServicio.Items.Add("Pasteado");
        }

        private void pckServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = pckServicio.Items[pckServicio.SelectedIndex];

            DisplayAlert("Haz seleccionado", name, "Ok");
        }
    }
}