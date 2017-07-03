using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFCustomControls.Ext
{
    public class Picker : Xamarin.Forms.Picker
    {
        private IEnumerable<object> _objectItems;

        public static readonly new BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                            typeof(object),
                            typeof(Picker),
                            null,
                            propertyChanged: (bo, o, n) => ((Picker)bo).OnItemsSourcePropertyChanged());
        public new object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly new BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem),
                                    typeof(object),
                                    typeof(Picker),
                                    null,
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: (bo, o, n) => ((Picker)bo).OnSelectedItemPropertyChanged());
        public new object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedItemChangedCommandProperty =
                               BindableProperty.Create(nameof(SelectedItemChangedCommand),
                                                       typeof(ICommand),
                                                       typeof(Picker),
                                                       null,
                                                       propertyChanged: (bo, o, n) => ((Picker)bo).OnCommandChanged());
        public ICommand SelectedItemChangedCommand
        {
            get { return (ICommand)GetValue(SelectedItemChangedCommandProperty); }
            set { SetValue(SelectedItemChangedCommandProperty, value); }
        }

        public Picker()
        {
            this.SelectedIndexChanged += (sender, e) => this.SelectedItem = this._objectItems.ElementAt(this.SelectedIndex);
        }

        private void OnItemsSourcePropertyChanged()
        {
            this._objectItems = this.CastObjectToList();

            this.PopulatedByIEnumerableItems(this._objectItems);

            if (this.ItemsSource is INotifyCollectionChanged notifyCollection)
                this.AssignNotifyCollectionItems(notifyCollection);
        }

        private IEnumerable<object> CastObjectToList()
        {
            if (this.ItemsSource is Enum)
            {
                var items = new List<object>();
                foreach (var item in Enum.GetValues(this.ItemsSource.GetType()))
                    items.Add(item);

                return items;
            }
            else if (this.ItemsSource is IEnumerable)
                return ItemsSource as IEnumerable<object>;

            return null;
        }

        private void OnSelectedItemPropertyChanged()
        {
            if (this.ItemsSource == null) return;

            this.SelectedIndex = this.IndexOf(this.SelectedItem);
            if (this.SelectedItemChangedCommand != null && this.SelectedItemChangedCommand.CanExecute(this.SelectedItem))
                this.SelectedItemChangedCommand.Execute(this.SelectedItem);
        }

        private int IndexOf(object value)
        {
            var comparer = EqualityComparer<object>.Default;
            var found = this._objectItems
                .Select((a, i) => new { a, i })
                .FirstOrDefault(x => comparer.Equals(x.a, value));
            return found == null ? -1 : found.i;
        }

        private void AssignNotifyCollectionItems(INotifyCollectionChanged items)
        {
            items.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var item in args.NewItems)
                            this.AddItem(item);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in args.OldItems)
                            this.RemoveItem(item);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        this.PopulatedByIEnumerableItems(this._objectItems);
                        break;
                    //TODO
                    case NotifyCollectionChangedAction.Move:
                        break;
                    //TODO
                    case NotifyCollectionChangedAction.Replace:
                        break;
                }
            };
        }

        private void PopulatedByIEnumerableItems(IEnumerable<object> items)
        {
            this.Items.Clear();
            foreach (var item in items)
                this.AddItem(item);
        }

        private void AddItem(object item)
        {
            if (item != null)
                this.Items.Add(item.ToString());
        }

        private void RemoveItem(object item)
        {
            if (item != null)
                this.Items.Remove(item.ToString());
        }

        private void OnCommandChanged()
        {
            if (SelectedItemChangedCommand != null)
            {
                SelectedItemChangedCommand.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
                IsEnabled = true;
        }

        private void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = SelectedItemChangedCommand;
            if (cmd != null)
                IsEnabled = cmd.CanExecute(this.SelectedItem);
        }
    }
}