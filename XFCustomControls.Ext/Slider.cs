using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class Slider : Xamarin.Forms.Slider
    {
        public static readonly BindableProperty ValueChangedCommandProperty =
            BindableProperty.Create("ValueChangedCommand",
                                    typeof(ICommand),
                                    typeof(Slider),
                                    null,
                                    propertyChanged: (bo, o, n) => ((Slider)bo).OnCommandChanged());
        public ICommand ValueChangedCommand
        {
            get { return (ICommand)GetValue(ValueChangedCommandProperty); }
            set { SetValue(ValueChangedCommandProperty, value); }
        }

        public static readonly BindableProperty ValueChangedCommandParameterProperty =
            BindableProperty.Create("ValueChangedCommandParameter",
                                    typeof(object),
                                    typeof(Slider),
                                    null,
                                    propertyChanged: (bindable, oldvalue, newvalue) => ((Slider)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));
        public object ValueChangedCommandParameter
        {
            get { return GetValue(ValueChangedCommandParameterProperty); }
            set { SetValue(ValueChangedCommandParameterProperty, value); }
        }

        void OnCommandChanged()
        {
            if (ValueChangedCommand != null)
            {
                ValueChangedCommand.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
                IsEnabled = true;
        }

        protected override void OnPropertyChanging(string propertyName = null)
        {
            if (propertyName == ValueChangedCommandProperty.PropertyName)
            {
                ICommand cmd = ValueChangedCommand;
                if (cmd != null)
                    cmd.CanExecuteChanged -= CommandCanExecuteChanged;
            }

            base.OnPropertyChanging(propertyName);
        }

        void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = ValueChangedCommand;
            if (cmd != null)
                IsEnabled = cmd.CanExecute(ValueChangedCommandParameter);
        }

        public Slider()
        {
            this.ValueChanged += (sender, e) => this.ValueChangedCommand?.Execute(e.NewValue);
        }
    }
}
