using Acr.UserDialogs;
using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Services;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command NewUserCommand { get; }
        public Command ResetPasswordCommand { get; }

        public Command SendUserToResetCommand { get; }
        //Inicio de sesion
        string user;
        string password;
        // Reseteo de contraseña
        string userToRecovery;
        public string User { get => user;
            set => SetProperty(ref user, value); }
        public string Password { get => password; 
            set => SetProperty(ref password, value); }
 

        Page page;
        User userInstance;
        public LoginViewModel(Page pag)
        {
            page = pag;
            userInstance = new User();
            LoginCommand = new Command(OnLoginClicked);
            NewUserCommand = new Command(OnNewUserClicked);
            ResetPasswordCommand = new Command(OnResetPassClicked);
            SendUserToResetCommand = new Command(OnSendUserClicked);


        }

        private async void OnSendUserClicked(object obj)
        {
            if (!IsNotConnect)
            {
                if (!string.IsNullOrEmpty(User))
                {
                    UserDialogs.Instance.ShowLoading();
                    int repuesta = await userInstance.ResetPassword(User);
                    if (repuesta == Constanst.REQUEST_OK)
                    {
                        await page.Navigation.PushAsync(new CambioContra(Constanst.TYPE_RESET, User));
                    }
                    else
                    {
                        await page.DisplayAlert("Error", "Asegura que tu usurio es correcto.", "Aceptar");
                    }
                    UserDialogs.Instance.HideLoading();

                }
                else
                {
                    UserDialogs.Instance.Toast("Debe llernar todos los campos");
                }
            }
            else
            {
                UserDialogs.Instance.Toast("Necesita estar conectado a internet.");
            }

        }

        private async void OnLoginClicked(object obj)
        {
          if(!IsNotConnect)
            {
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                {
                    UserDialogs.Instance.ShowLoading("Verificando");
                    int repuesta = await userInstance.LogIn(User, Password);
                    if (repuesta == Constanst.REQUEST_OK)
                    { 
                        Preferences.Set(Constanst.RECENT_SESSION, true);
                        var m = Preferences.Get(Constanst.RECENT_SESSION, false);
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                      await  page.DisplayAlert("Error", "Usuario o contraseña incorrectos","Ok");
                    }
                    UserDialogs.Instance.HideLoading();

                }
                else
                {
                    UserDialogs.Instance.Toast("Debe llernar todos los campos");
                }
            }
            else
            {
                UserDialogs.Instance.Toast("Necesita estar conectado a internet.");
            }

        }

        //Estos comando se utilizan en el login
        private async void OnNewUserClicked()
        {
            UserDialogs.Instance.ShowLoading("");
            await page.Navigation.PushAsync(new CrearUsuario());
            UserDialogs.Instance.HideLoading();
        }
        
        private async void OnResetPassClicked()
        {
            await page.Navigation.PushAsync(new ResetPassPage());
        }
    }
}
