# XF Custom Controls Ext

This library adds some non-existent functionality to controls such as ListView, Entry, Picker, and Slider.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="XFCustomControls.Views.EntryExtView"
             xmlns:ext="clr-namespace:XFCustomControls.Ext;assembly=XFCustomControls.Ext"
             Title="EntryExtView"
             >
    <StackLayout Padding="40">
        <ext:Entry MaxLength="{Binding MaxLength}" Placeholder="MaxLength" Text="1234" />
        
        <ext:Slider ValueChangedCommand="{Binding ValueChangedCommand}" />
        
        <ext:ListView ItemsSource="{Binding Items}" 
                      ItemTappedCommand="{Binding ItemTappedCommand}"
                      InfiniteScrollCommand="{Binding InfiniteScrollCommand}"
                      >
            
        </ext:ListView>
    </StackLayout>    
</ContentPage>
```

**Entry.MaxLength** it's a bindable property which you can set a Max Length of Text in an Entry Control!

**Slider.ValueChangedCommand** A command that is fired when the Slider's Value is Changed. Perfect to use with MVVM!

**ListView.ItemTappedCommand** A command that is fired when a listview item is tapped...

**ListView.InfiniteScrollCommand** A command that is fired when a listview show the last item in the screen
