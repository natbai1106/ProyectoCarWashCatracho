using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Behaviors
{
    class EmailValidationBehavior : Behavior<Entry>
    {
        //evento para empezar a escribir
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        //
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);           
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender; 
            if (!string.IsNullOrEmpty(entry.Text))
            {
                string emailRegEx= @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                bool isMacthed = Regex.IsMatch(entry.Text, emailRegEx);

                if (isMacthed)
                    entry.TextColor = Color.Black;
                else
                    entry.TextColor = Color.Red;
            }
            
        }
    }
}
