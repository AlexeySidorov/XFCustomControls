using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.ViewModels
{
    internal class ListViewExtViewModel : ViewModel
    {
        public ObservableCollection<string> Views { get; set; }
        public ICommand ItemTappedCommand { get; set; }

        public ListViewExtViewModel()
        {
            this.Views = new ObservableCollection<string>();
            this.Views.Add(nameof(XFCustomControls.Views.SliderExtView));
            this.Views.Add(nameof(XFCustomControls.Views.EntryExtView));

            this.ItemTappedCommand = new Command(item =>
            {
                switch (item.ToString())
                {
                    case nameof(XFCustomControls.Views.SliderExtView):
                        App.Current.MainPage.Navigation.PushAsync(new Views.SliderExtView());
                        break;
                    case nameof(XFCustomControls.Views.EntryExtView):
                        App.Current.MainPage.Navigation.PushAsync(new Views.EntryExtView());
                        break;
                    default:
                        break;
                }
            });
        }
    }
}