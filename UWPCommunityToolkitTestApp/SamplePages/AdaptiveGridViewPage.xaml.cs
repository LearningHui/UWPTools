using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCommunityToolkitTestApp.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace UWPCommunityToolkitTestApp.SamplePages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AdaptiveGridViewPage : Page
    {
        public AdaptiveGridViewPage()
        {
            this.InitializeComponent();

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AdaptiveGridViewControl.ItemsSource = await new Data.PhotosDataSource().GetItemsAsync();
            AdaptiveGridViewControl.ItemClick += AdaptiveGridViewControl_ItemClick;
            AdaptiveGridViewControl.SelectionChanged += AdaptiveGridViewControl_SelectionChanged;
        }

        private void AdaptiveGridViewControl_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            SelectedItemCountTextBlock.Text = AdaptiveGridViewControl.SelectedItems.Any()
                ? $"You have selected {AdaptiveGridViewControl.SelectedItems.Count} items."
                : "You haven't selected any items";
        }

        private async void AdaptiveGridViewControl_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                await new MessageDialog($"You clicked {(e.ClickedItem as PhotoDataItem).Title}", "Item Clicked").ShowAsync();
            }
        }

    }
}
