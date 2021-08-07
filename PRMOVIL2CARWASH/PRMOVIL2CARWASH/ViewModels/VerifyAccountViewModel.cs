using Acr.UserDialogs;
using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    class VerifyAccountViewModel: BaseViewModel
    {
       public Command SaveUserCommand { get; }

       public Command ModifyDataCommand { get; }

       public  Command ResendCodeCommand { get; }

        User UserInCache { get; set; }
        string code;
        bool codeIsCorrect;
        public string Code { get => code;
            set {
                SetProperty(ref code, value);
                if (code.Length >=Constanst.LENGTH_CODE)
                    CodeIsCorrect = true;
                else
                   CodeIsCorrect = false;
                     }
            
        }
        public bool CodeIsCorrect { get => codeIsCorrect; set => SetProperty(ref codeIsCorrect, value); }

        Page Page;
        public VerifyAccountViewModel(Page page)
        {
            Page = page;
            ResendCodeCommand = new Command(OnResendCodeCliked);
            SaveUserCommand = new Command(OnSaveUserCliked);
            ModifyDataCommand = new Command(OnModifyDataClicked);
            GetUserInCache();
         
        }


        public async void OnResendCodeCliked()
        {
            if(!IsNotConnect)
            {
                UserDialogs.Instance.ShowLoading("Reenviado código");
                int respuesta = await UserInCache.ResendVerifyCode();
                UserDialogs.Instance.HideLoading();

                if (respuesta == Constanst.REQUEST_OK)
                    UserDialogs.Instance.Toast("Se enviado un código nuevo");
                else
                    UserDialogs.Instance.Toast("Lo sentimos no pudimos enviar un código nuevo");
            }
     
            else
                UserDialogs.Instance.Toast("Oops!, no tienes acceso a internet");
        }
        public async void OnSaveUserCliked()
        {
            if (!IsNotConnect)
            {
                UserDialogs.Instance.ShowLoading("Verificando");

                UserInCache.Codigo = Code;
                int respuesta = await UserInCache.RegisterUser();
                UserDialogs.Instance.HideLoading();

                if (respuesta == Constanst.REQUEST_OK)
                {

                    //Si el usuario se registro borramos el usuario guardado en cache 
                    Barrel.Current.Empty(Constanst.USER_CACHE);
                    Cache.SaveCache(UserInCache.Usuario, UserInCache, Constanst.EXPIRE_CURREN_USER);
                    Preferences.Set(Constanst.CURRENT_USER, UserInCache.Usuario);
                    Preferences.Set(Constanst.STATE_SESSION, true);
                    Preferences.Set(Constanst.RECENT_SESSION, true);
                    Application.Current.MainPage = new AppShell();

                }
                    
                //UserDialogs.Instance.Toast("Guardado con exito");
                else
                    UserDialogs.Instance.Toast("Error al verificar código.");
                
            }

            else
                UserDialogs.Instance.Toast("Oops!, no tienes acceso a internet");
        }

       public async void GetUserInCache()
        {
            UserInCache = Barrel.Current.Get<User>(Constanst.USER_CACHE);
        }

       public async void OnModifyDataClicked()
        {
            Page.Navigation.InsertPageBefore(new CrearUsuario(), Page);
            await  Page.Navigation.PopAsync();
        }
    }
}
