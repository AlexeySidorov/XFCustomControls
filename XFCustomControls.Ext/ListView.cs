using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class ListView : Xamarin.Forms.ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
          BindableProperty.Create("ItemTappedCommand",
                            typeof(ICommand),
                            typeof(ListView),
                            null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        public static readonly BindableProperty InfiniteScrollCommandProperty =
            BindableProperty.Create("InfiniteScrollCommand",
                    typeof(ICommand),
                    typeof(ListView),
                    null);
        public ICommand InfiniteScrollCommand
        {
            get { return (ICommand)GetValue(InfiniteScrollCommandProperty); }
            set { SetValue(InfiniteScrollCommandProperty, value); }
        }

        public ListView() : base()
        {
            Initialize();
        }
        public ListView(Xamarin.Forms.ListViewCachingStrategy cachingStrategy)
            : base(cachingStrategy)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ItemTapped += (sender, e) =>
            {
                if (this.ItemTappedCommand == null) return;
                if (e.Item == null) return;

                if (this.ItemTappedCommand.CanExecute(e))
                {
                    this.ItemTappedCommand.Execute(e.Item);
                    this.SelectedItem = null;
                }
            };

            this.ItemAppearing += (sender, e) => {
                var items = this.ItemsSource as IList;

                if (items == null) return;
                if (e.Item == null) return;
                if (this.InfiniteScrollCommand == null) return;

                if (e.Item == items[Math.Max(items.Count - 4, 0)] && this.InfiniteScrollCommand.CanExecute(e.Item))
                    this.InfiniteScrollCommand.Execute(e.Item);
            };
        }
    }
}
