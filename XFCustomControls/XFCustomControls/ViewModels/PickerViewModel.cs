using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XFCustomControls.ViewModels
{
    public class PickerViewModel : ViewModel
    {
        private string _selectedItem;
        public string SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private EnumItems _enumSelectedItem;
        public EnumItems EnumSelectedItem
        {
            get { return _enumSelectedItem; }
            set { SetProperty(ref _enumSelectedItem, value); }
        }

        private string _newItem;
        public string NewItem
        {
            get { return _newItem; }
            set { SetProperty(ref _newItem, value); }
        }

        public ObservableCollection<string> Items { get; set; }
        public Command SelectedItemChangedCommand { get; set; }
        public Command AddNewItemCommand { get; set; }

        public EnumItems EnumItems { get; set; }
        public Command EnumSelectedItemChangedCommand { get; set; }
        public Command EnumAddNewItemCommand { get; set; }

        public PickerViewModel()
        {
            this.Items = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
                this.Items.Add($"Item {i}");

            this.SelectedItem = this.Items[0];
            this.EnumSelectedItem = EnumItems.Enum4;

            AddNewItemCommand = new Command(o =>
            {
                this.Items.Add(this.NewItem);
                this.NewItem = string.Empty;
            });

            SelectedItemChangedCommand = new Command(async item =>
            {
                await App.Current.MainPage.DisplayAlert("Picker", $"Selected Item {item.ToString()}", "Ok");
            });
        }
    }

    public enum EnumItems
    {
        Enum1,
        Enum2,
        Enum3,
        Enum4,
    }
}
