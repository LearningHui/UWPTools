using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace SignApp.Sign
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
            TryGetUserInfo();
        }

        private async void TryGetUserInfo()
        {
            if(await VMLocator.Instance.SignVM.GetUserInfo() == false)
            {
                //todo:网络请求失败
            }
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            this.flipPanel.IsFlipped = true;
        }             

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            this.flipPanel.IsFlipped = true;

        }

        private void SetAdressBtn_Click(object sender, RoutedEventArgs e)
        {
            this.flipPanel.IsFlipped = false;
        }
    }
}
