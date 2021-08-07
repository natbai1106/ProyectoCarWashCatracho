using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Plugin.CurrentActivity;
using Android.Content;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace PRMOVIL2CARWASH.Droid
{
    [Activity(Label = "PRMOVIL2CARWASH", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

                
            OneSignal.Current.StartInit("42b0cfb0-590b-4ade-983b-cc054e08d1f4")
             .InFocusDisplaying(OSInFocusDisplayOption.Notification)
             .EndInit();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());

            UserDialogs.Init(this);

            //Inicializa el servicio de Broadcast 
            Intent intent = new Intent(this, typeof(SMSBroadcastReceiver));
            StartService(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}