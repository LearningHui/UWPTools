using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace WuBiApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Shell : Page
    {
        public MenuItem CurrentMenuItem { get; set; }

        public Shell()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Options
            HamburgerMenu.OptionsItemsSource = new[]
            {
                new Option { Glyph = "", Name = "Setting" },
                new Option { Glyph = "", Name = "About"}
            };
            NavigationFrame.Navigating += NavigationFrame_Navigating;
            NavigationFrame.Navigated += NavigationFrameOnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            NavigationFrame.Navigate(typeof(MainPage));
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if (backRequestedEventArgs.Handled)
            {
                return;
            }

            if (NavigationFrame.CanGoBack)
            {
                backRequestedEventArgs.Handled = true;

                NavigationFrame.GoBack();
            }
        }

        private void NavigationFrameOnNavigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = NavigationFrame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private void NavigationFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
