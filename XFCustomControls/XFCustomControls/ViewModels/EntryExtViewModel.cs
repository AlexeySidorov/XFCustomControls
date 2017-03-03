namespace XFCustomControls.ViewModels
{
    public class EntryExtViewModel : ViewModel
    {
        private int _maxLength;
        public int MaxLength
        {
            get { return _maxLength; }
            set { SetProperty(ref _maxLength, value); }
        }

        public EntryExtViewModel()
        {
            this.MaxLength = 6;
        }
    }
}
