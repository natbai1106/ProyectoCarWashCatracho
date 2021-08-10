using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Triggers
{
    public class AgeVerificationTrigger : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            var entry = sender as Entry;
            var flag = int.TryParse(entry.Text, out int age);
            entry.BackgroundColor = ((!flag) || (age < 18 || age > 80)) ?
                Color.Red : Color.Default;
        }
    }
}
