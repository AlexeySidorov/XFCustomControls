using Xamarin.Forms;

namespace XFCustomControls
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.MasterView());
        }
    }
}
