using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class ListView : Xamarin.Forms.ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
          BindableProperty.Create(nameof(ItemTappedCommand),
                            typeof(ICommand),
                            typeof(ListView),
                            null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public static readonly BindableProperty InfiniteScrollCommandProperty =
            BindableProperty.Create(nameof(InfiniteScrollCommand),
                    typeof(ICommand),
                    typeof(ListView),
                    null);
        public ICommand InfiniteScrollCommand
        {
            get { return (ICommand)GetValue(InfiniteScrollCommandProperty); }
            set { SetValue(InfiniteScrollCommandProperty, value); }
        }

        public ListView()
            : base()
        {
            Initialize();
        }
        public ListView(ListViewCachingStrategy cachingStrategy)
            : base(cachingStrategy)
        {
            Initialize();
        }

        private void Initialize()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(null))
                ItemTappedCommand.Execute(e.Item);
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 3])
            {
                if (InfiniteScrollCommand != null && InfiniteScrollCommand.CanExecute(null))
                    InfiniteScrollCommand.Execute(null);
            }
        }
    }
}