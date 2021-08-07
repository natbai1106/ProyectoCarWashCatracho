using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Triggers
{
    public class ButtonEventTriggers: TriggerAction<Button>
    {
        public event EventHandler Pressed;
        public event EventHandler Released;

        protected virtual void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

        protected override void Invoke(Button btn)
        {
            btn.ImageSource = "eye.png";
            
           // btn.BackgroundColor = Color.DarkOrange;
        }


    }
}
