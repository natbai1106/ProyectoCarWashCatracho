using PRMOVIL2CARWASH.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}