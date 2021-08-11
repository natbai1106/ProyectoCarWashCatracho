using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        Page Page { get; }
        User currentUser;
        public HomeViewModel(Page page)
        {
            Page = page;
            
          //  OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            CheckUserState();
            

        }

        public ICommand OpenWebCommand { get; }


        private async void CheckUserState()
        {
            var ls = Preferences.Get(Constanst.RECENT_SESSION, false);

            if (!Preferences.Get(Constanst.RECENT_SESSION, false))//Valida si se acaba de iniciar sesión, de ser así no se realiza ninguna acción
            {
                string user = Preferences.Get(Constanst.CURRENT_USER, Constanst.DEFAULT_USER);
                bool exisInCache = Barrel.Current.Exists(user) & !Barrel.Current.IsExpired(user);

                if (IsNotConnect && exisInCache)//Verifica si hay conexion a internet y esta guardado en cache, lo carga de cache
                {
                    
                        App.SetCurrentUser(Barrel.Current.Get<User>(user));
                        currentUser = App.CurrentUser();
                        Title = currentUser.Usuario;
                        return;
                        
                }
                else if(!IsNotConnect && exisInCache)//Si esta conectado y 
                {
                    App.SetCurrentUser(Barrel.Current.Get<User>(user));
                    
                    int respuesta = await App.CurrentUser().CheckStatusSession();
                    if (respuesta == Constanst.SESSION_CLOSED || respuesta == Constanst.REQUEST_ERROR)//Si la sesión esta iniciada 
                    {
                        Preferences.Set(Constanst.RECENT_SESSION, false);
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    }
                    else
                        Title = App.CurrentUser().Usuario;
                }
                else
                    Application.Current.MainPage = new NavigationPage(new LoginPage());

            }
            else
                Title = App.CurrentUser().Usuario;


            Preferences.Set(Constanst.RECENT_SESSION, false);

                
            
        }
    }
}