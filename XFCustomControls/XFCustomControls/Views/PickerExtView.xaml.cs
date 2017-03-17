using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFCustomControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerExtView : ContentPage
    {
        public PickerExtView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.PickerViewModel();
        }
    }
}
