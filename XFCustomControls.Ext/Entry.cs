using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class Entry : Xamarin.Forms.Entry
    {
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength",
                            typeof(int?),
                            typeof(Entry),
                            default(int?),
                            BindingMode.TwoWay,
                            propertyChanged: (bindable, oldvalue, newvalue) =>
                            {
                                var entry = (Entry)bindable;
                                entry.TruncateText();
                            });

        public int? MaxLength
        {
            get { return (int?)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public Entry()
        {
            this.TextChanged += (sender, e) => TruncateText();
        }

        private void TruncateText()
        {
            if (string.IsNullOrEmpty(this.Text)) return;
            if (!this.MaxLength.HasValue) return;

            if (this.Text.Length > this.MaxLength.GetValueOrDefault())
            {
                var maxLength = this.MaxLength.GetValueOrDefault();
                var value = this.Text;
                value = value.Remove(value.Length - (value.Length - maxLength));
                this.Text = value;
            }
        }
    }
}
