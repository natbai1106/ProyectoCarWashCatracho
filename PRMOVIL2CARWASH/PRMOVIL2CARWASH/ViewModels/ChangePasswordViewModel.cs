using Acr.UserDialogs;
using PRMOVIL2CARWASH.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
   public class ChangePasswordViewModel:BaseViewModel
    {
        string user;
        string oldPassword;
        string newPassword;
        string confirmPassword;
        string typeChange;//Establece si es un reseteo o un cambio de contrasena
        public Command ChangePasswordCommand { get; }
        public string OldPassword { get => oldPassword; set => SetProperty(ref oldPassword, value); }
        public string NewPassword { get => newPassword; set => SetProperty(ref newPassword, value); }
        public string ConfirmPassword { get => confirmPassword; set => SetProperty(ref confirmPassword, value); }
        Page page;

        public ChangePasswordViewModel(Page pag,string type,string userChange)
        {
            page = pag;
            typeChange = type;
            user = userChange;
            ChangePasswordCommand = new Command(OnChangePasswordClicked);
        }

        private async void OnChangePasswordClicked()
        {
            bool isFill = !string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmPassword);
            if (isFill)
            {
                int respuesta = 0;
                if (newPassword.Equals(confirmPassword))
                {
                    UserDialogs.Instance.ShowLoading("Verificando");
                    if(typeChange.Equals(Constanst.TYPE_CHANGE))
                    {
                        user = App.CurrentUser().Usuario;//Se obtiene el usuario actual
                        respuesta = await App.CurrentUser().ChangePassword(user, typeChange, newPassword, oldPassword);
                        if (respuesta == Constanst.REQUEST_OK)
                        {//Se actuallia en cache
                            App.CurrentUser().Contrasena = newPassword;
                            Cache.SaveCache(user, App.CurrentUser(), Constanst.EXPIRE_CURREN_USER);
                            await Shell.Current.GoToAsync("..");

                        }
                        else
                            UserDialogs.Instance.Toast("La contraseña no se pudo actualizar");
                    }
                    else if (typeChange.Equals(Constanst.TYPE_RESET))
                    {
                        respuesta = await App.CurrentUser().ChangePassword(user, typeChange, newPassword, oldPassword);
                        if (respuesta == Constanst.REQUEST_OK)
                        {//Se obtiene el usuario y se inicia sesión
                            await App.CurrentUser().GetUser(user, newPassword);
                            Application.Current.MainPage = new AppShell();
                        }
                        else
                         await   page.DisplayAlert("Error", "No se pudo cambiar tu contraseña, asegurate que el código se te envió sea correcto", "Aceptar");
                    }

                    UserDialogs.Instance.HideLoading();
                    
                }
                else
                    UserDialogs.Instance.Toast("No coinciden las contraseñas");

            }
            else
                UserDialogs.Instance.Toast("Debe llenar todos los campos");
        }
    }

}
