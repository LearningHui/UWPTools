
using SignApp.ViewModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace SignApp.Sign
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SignPage : Page
    {
        SignViewModel vm = VMLocator.Instance.SignVM;
        public SignPage()
        {
            this.InitializeComponent();         
            this.Loaded += SignPage_Loaded;
        }

        private async void SignPage_Loaded(object sender, RoutedEventArgs e)
        {
            vm.UserName = "邹鹏辉";
            await VMLocator.Instance.SignVM.DoSign("true");//查询打卡信息
        }

        private async void SignBtn_Click(object sender, RoutedEventArgs e)
        {
            await VMLocator.Instance.SignVM.DoSign("false");//发送打卡请求
            Storyboard1.Begin();
            Storyboard2.Begin();
        }
    }
}
