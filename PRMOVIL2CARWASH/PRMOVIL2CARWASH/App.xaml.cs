using Com.OneSignal;
using Com.OneSignal.Abstractions;
using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Services;
using PRMOVIL2CARWASH.Utils;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH
{
    public partial class App : Application
    {
        //Creamos una instancia global que existira mientras la app este con vida
        private static User currenUser;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            SwitchMainPage();
            // MainPage = new NavigationPage( new LoginPage());
            // MainPage = new AppShell();
            // Remove this method to stop OneSignal Debugging
            // OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);
            OneSignal.Current.StartInit("42b0cfb0-590b-4ade-983b-cc054e08d1f4")
            .Settings(new Dictionary<string, bool>() {
                { IOSSettings.kOSSettingsKeyAutoPrompt, false },
                { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
            .InFocusDisplaying(OSInFocusDisplayOption.Notification)
            .EndInit();
            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();
           
        }

        protected override void OnStart()
        {
         
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        private void SwitchMainPage()
        {
            Barrel.ApplicationId = "com.companyname.prmovil2carwash";
            string usuario = Preferences.Get(Constanst.CURRENT_USER, Constanst.DEFAULT_USER);
            if (Barrel.Current.Exists(usuario))//Valida si existe un usuario en cache
            {
                if (Barrel.Current.IsExpired(usuario)) //Si ya esta expirado lo envia al login y limpia los usuarios expirados
                {
                    MainPage = new NavigationPage(new LoginPage());
                    Barrel.Current.EmptyExpired();
                }
                else if(Preferences.Get(Constanst.STATE_SESSION,false)) //valida si el estado de la sesión quedo activo si es asi lo envia al appshell
                {
                    MainPage = new AppShell();
                }
                else
                    MainPage = new NavigationPage(new LoginPage());
            }
            
            else if(Barrel.Current.Exists(Constanst.USER_CACHE))//Si no hay un usuario que haya iniciado sesion pero ha empezado el proceso de registro lo llevara
            { 
  
                if (!Barrel.Current.IsExpired(Constanst.USER_CACHE)) //Si ya esta expirado lo envia al login y limpia los usuarios expirados
                {
                    User userAux = Barrel.Current.Get<User>(Constanst.USER_CACHE);
                    if(!string.IsNullOrEmpty(userAux.Token)) //Si el usuario guardado en cache esta listo ya tiene un token, de ser asi esta esperando una verificacion
                        MainPage = new NavigationPage(new Validacion());
                    else
                        MainPage = new NavigationPage(new CrearUsuario());

                }
            }
            else
                MainPage = new NavigationPage(new LoginPage());

        }

        public static User CurrentUser()//Almacenar al usuario que esta registrado
        {
            if (currenUser == null)
                currenUser = new User();
            return currenUser;
        }

        public static void SetCurrentUser(User user)
        {
            currenUser = user;
        }


    }

}
