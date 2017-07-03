using Xamarin.Forms;

namespace XFCustomControls.ViewModels
{
    public class EntryExtViewModel : ViewModel
    {
        public Command CompletedCommand { get; set; }


        private int _maxLength = 6;
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                SetProperty(ref _maxLength, value);
                this.CompletedCommand.ChangeCanExecute();
            }
        }

        private string _text = "123456789";
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public EntryExtViewModel()
        {
            this.CompletedCommand = new Command(async item =>
            {
                await App.Current.MainPage.DisplayAlert("Entry", item.ToString(), "Ok");
            }, item => !string.IsNullOrEmpty(Text));
        }
    }
}
