using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XFCustomControls.ViewModels
{
    public class ListViewExtViewModel : ViewModel
    {
        private int _index = 0;

        public ObservableCollection<string> Items { get; set; }
        public Command ItemTappedCommand { get; set; }
        public Command InfiniteScrollCommand { get; set; }

        public ListViewExtViewModel()
        {
            this.Items = new ObservableCollection<string>();
            this.AddItems(20);

            this.ItemTappedCommand = new Command(async item =>
            {
                await App.Current.MainPage.DisplayAlert("ListViewExt", item.ToString(), "Ok");
            });

            this.InfiniteScrollCommand = new Command(item =>
            {
                if (_index < 100)
                    this.AddItems(10);
            });
        }

        private void AddItems(int count)
        {
            for (int i = 0; i < count; i++)
                this.Items.Add($"Item {_index++}");
        }
    }
}