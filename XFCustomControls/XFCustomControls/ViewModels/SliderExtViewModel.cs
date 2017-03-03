using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.ViewModels
{
    public class SliderExtViewModel : ViewModel
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetProperty(ref _isEnabled, value);
                this.ValueChangedCommand.ChangeCanExecute();
            }
        }

        public Command ValueChangedCommand { get; set; }
        public SliderExtViewModel()
        {
            this.ValueChangedCommand = new Command(Execute, CanExecute);
        }

        private void Execute(object value) => this.Text = value.ToString();

        private bool CanExecute(object value) => this.IsEnabled;
    }
}
