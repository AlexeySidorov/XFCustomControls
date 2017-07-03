using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class Entry : Xamarin.Forms.Entry
    {
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength),
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

        public static readonly BindableProperty CompletedCommandProperty =
            BindableProperty.Create(nameof(CompletedCommand),
                            typeof(ICommand),
                            typeof(Entry),
                            null,
                            propertyChanged: (bo, o, n) => ((Entry)bo).OnCommandChanged());
        public ICommand CompletedCommand
        {
            get { return (ICommand)GetValue(CompletedCommandProperty); }
            set { SetValue(CompletedCommandProperty, value); }
        }

        public static readonly BindableProperty CompletedCommandParameterProperty =
            BindableProperty.Create(nameof(CompletedCommandParameter),
                                    typeof(object),
                                    typeof(Entry),
                                    null,
                                    propertyChanged: (bindable, oldvalue, newvalue) => ((Entry)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));
        public object CompletedCommandParameter
        {
            get { return GetValue(CompletedCommandParameterProperty); }
            set { SetValue(CompletedCommandParameterProperty, value); }
        }

        public Entry()
        {
            this.TextChanged += (sender, e) => this.TruncateText();
            this.Completed += (sender, e) => this.ExecuteCompletedCommand();
        }

        private void ExecuteCompletedCommand()
        {
            if (this.CompletedCommand != null)
                if (this.CompletedCommand.CanExecute(this.CompletedCommandParameter))
                    this.CompletedCommand.Execute(this.CompletedCommandParameter);
        }

        private void OnCommandChanged()
        {
            if (CompletedCommand != null)
            {
                CompletedCommand.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
                IsEnabled = true;
        }

        private void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = CompletedCommand;
            if (cmd != null)
                IsEnabled = cmd.CanExecute(CompletedCommandParameter);
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
