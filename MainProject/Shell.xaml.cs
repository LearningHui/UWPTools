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

namespace MainProject
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Shell : Page
    {
        public static Shell Current { get; private set; }
        private Sample _currentSample;
        public Shell()
        {
            this.InitializeComponent();
            Current = this;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Get list of samples
            var sampleCategories = (await Samples.GetCategoriesAsync()).ToList();
            HamburgerMenu.ItemsSource = sampleCategories;
            NavigationFrame.Navigating += NavigationFrame_Navigating;
            NavigationFrame.Navigated += NavigationFrameOnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        /// <summary>
        /// Called when [back requested] event is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="backRequestedEventArgs">The <see cref="BackRequestedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// When the frame has navigated this method is called.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="navigationEventArgs">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void NavigationFrameOnNavigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = NavigationFrame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private async void NavigationFrame_Navigating(object sender, NavigatingCancelEventArgs navigationEventArgs)
        {
            SampleCategory category;
            if (navigationEventArgs.SourcePageType == typeof(SamplePicker) || navigationEventArgs.Parameter == null)
            {
                DataContext = null;
                category = navigationEventArgs.Parameter as SampleCategory;                
            }
            else
            {
                var sampleName = navigationEventArgs.Parameter.ToString();
                var sample = await Samples.GetSampleByName(sampleName);
                if (sample == null)
                {
                    return;
                }

                category = await Samples.GetCategoryBySample(sample);
                DataContext = sample;
                _currentSample = sample;                
            }
            if (category == null && navigationEventArgs.SourcePageType == typeof(SamplePicker))
            {
                // This is a search
                HamburgerMenu.SelectedItem = null;
                HamburgerMenu.SelectedOptionsItem = null;
            }
            else
            {
                if (HamburgerMenu.Items.Contains(category))
                {
                    HamburgerMenu.SelectedItem = category;
                    HamburgerMenu.SelectedOptionsItem = null;
                }
                else
                {
                    //HamburgerMenu.SelectedItem = null;
                    //HamburgerMenu.SelectedOptionsIndex = category != null ? 0 : 1;
                }
            }
        }

        public void NavigateToSample(Sample sample)
        {
            var pageType = Type.GetType("MainProject.Test." + sample.Type);

            if (pageType != null)
            {
                NavigationFrame.Navigate(pageType, sample.Name);
            }
        }
        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var category = e.ClickedItem as SampleCategory;

            if (category != null)
            {
                while (NavigationFrame.BackStack.Count > 0)
                {
                    if (NavigationFrame.CanGoBack)
                        NavigationFrame.GoBack();
                }                
                NavigationFrame.Navigate(typeof(SamplePicker), category);
            }
        }

        private void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs e)
        {
            var option = e.ClickedItem as Option;
            if (option == null)
            {
                return;
            }

            if (option.Tag != null)
            {
                //NavigationFrame.Navigate(typeof(SamplePicker), option.Tag);
                return;
            }

            if (NavigationFrame.CurrentSourcePageType != option.PageType)
            {
                NavigationFrame.Navigate(option.PageType);
            }
        }

    }
}
