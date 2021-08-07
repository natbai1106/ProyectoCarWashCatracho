using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Behaviors
{
   public class PasswordBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]{8,}$";

        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        //10 caracteres, con al menos: 1 digito, 1 minúscula, 1 mayúscula, 1 caracter especial
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var text = entry.Text;

            if (!string.IsNullOrWhiteSpace(Mask))

                // 1. Adding the MaxLength
                if (text.Length == _mask.Length)
                    entry.MaxLength = _mask.Length;

            // 2. Evaluating if the user is removing test
            if ((e.OldTextValue == null) || (e.OldTextValue.Length <= e.NewTextValue.Length))

                // 3. Evaluating mask positions
                for (int i = Mask.Length; i >= text.Length; i--)
                {
                    if (Mask[(text.Length - 1)] != 'X')
                    {
                        text = text.Insert((text.Length - 1), Mask[(text.Length - 1)].ToString());
                    }
                }
            entry.Text = text;

            bool valido = (Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = valido ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(Entry entry)
        {

            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
