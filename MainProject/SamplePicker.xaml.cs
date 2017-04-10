using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class SamplePicker : INotifyPropertyChanged
    {
        /// <summary>
        /// The view model for the sample picker
        /// </summary>
        private SampleCategory _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePicker"/> class.
        /// </summary>
        public SamplePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public SampleCategory ViewModel
        {
            get
            {
                return _viewModel;
            }

            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame. Setting the View Model so that controls can bind to it.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel = e.Parameter as SampleCategory;
        }

        /// <summary>
        /// Handles the OnItemClick event of the SamplesList control. This navigates to the appropriate sample.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private void SamplesList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var sample = e.ClickedItem as Sample;

            if (sample != null)
            {
                Shell.Current.NavigateToSample(sample);
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SamplePicker_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 600)
            {
                var small = (DataTemplate)Resources["SmallSampleTemplate"];

                if (SamplesList.ItemTemplate == small)
                {
                    return;
                }

                SamplesList.ItemTemplate = small;
            }
            else
            {
                var normal = (DataTemplate)Resources["SampleTemplate"];

                if (SamplesList.ItemTemplate == normal)
                {
                    return;
                }

                SamplesList.ItemTemplate = normal;
            }
        }
    }
}
