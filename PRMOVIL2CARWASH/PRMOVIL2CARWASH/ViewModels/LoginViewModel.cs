using PRMOVIL2CARWASH.Services;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command NewUserCommand { get; }

        Page page;
        public LoginViewModel(Page pag)
        {
            page = pag;
            LoginCommand = new Command(OnLoginClicked);
            NewUserCommand = new Command(OnNewUserClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //
            // await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
          await page.DisplayAlert ("Existe un error", "esto es un ejemplo", "ok");
        }

        private async void OnNewUserClicked()
        {
            await page.Navigation.PushAsync(new CrearUsuario());
            
        }
    }
}
