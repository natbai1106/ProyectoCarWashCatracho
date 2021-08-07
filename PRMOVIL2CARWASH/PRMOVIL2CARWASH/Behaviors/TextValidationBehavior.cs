using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Behaviors
{
    class TextValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
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
                string TextRegEx = "[0-9]"; 
                bool isMacthed = Regex.IsMatch(entry.Text, TextRegEx);

                if (!isMacthed)
                    entry.TextColor = Color.Black;
                else
                    entry.TextColor = Color.Red;
            }

        }
    }
}
