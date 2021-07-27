using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Phone;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new[] { SmsRetriever.SmsRetrievedAction })]
    public class SMSBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Type != SmsRetriever.SmsRetrievedAction)//Se valida que la difusión sea de tipo mensaje
                return;

            var extrasBunble = intent.Extras;
            if (extrasBunble == null)
                return;
            var status = (Statuses)extrasBunble.Get(SmsRetriever.ExtraStatus);
            switch (status.StatusCode)
            {
                case CommonStatusCodes.Success:
                    //Si el codig se ejecuto correctamente entonces se envia un mensaje para poder recibir el mensaje 
                    var mensaje = (string)extrasBunble.Get(SmsRetriever.ExtraSmsMessage);
                    MessagingCenter.Send<string>(mensaje, "MessageOTP");
                    break;
                case CommonStatusCodes.Timeout:
                    break;
            }
            // Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
        }
    }
}