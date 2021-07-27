using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Phone;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PRMOVIL2CARWASH.Droid.HashKey;
using PRMOVIL2CARWASH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(HashKeyAndroid))]
namespace PRMOVIL2CARWASH.Droid.HashKey
{
    public class HashKeyAndroid : IHashService
    {
        [Obsolete]
        public string GenerateHash()
        {
            AppHashKeyHelper hashKeyGenerator = new AppHashKeyHelper();
            return hashKeyGenerator.GetAppHashKey(global::Android.App.Application.Context);

        }

        public void StartSMSRetriverReciber()
        {

            /*Se crea un instancia del recuperador de mensaje*/
            SmsRetrieverClient _cliente = SmsRetriever.GetClient(global::Android.App.Application.Context);
            /*
             Se inicializa el Retriever para esparar por difución la llegada de un nuevo mensaje
             */
            _cliente.StartSmsRetriever();

        }
    }

    internal class SuccessListener : Java.Lang.Object, IOnSuccessListener
    {
        public void OnSuccess(Java.Lang.Object result)
        {
            throw new NotImplementedException();
        }
    }

    internal class FailureListener : Java.Lang.Throwable, IOnFailureListener
    {

        public void OnFailure(Java.Lang.Exception e)
        {
            throw new NotImplementedException();
        }
    }
}