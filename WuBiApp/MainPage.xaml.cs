using Microsoft.Toolkit.Uwp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace WuBiApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();           
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            using (var jsonStream = await StreamHelper.GetPackagedFileStreamAsync("Data/字库.txt"))
            {
                var str = await jsonStream.ReadTextAsync(Encoding.UTF8);
                str = str.Replace("\n", " ");
                string[] values = str.Split(':');
                Dictionary<string, string> dictionnary = new Dictionary<string, string>();
                Dictionary<string, Dictionary<string, string>> wubiDic = new Dictionary<string, Dictionary<string, string>>();
                string pinyin = "";
                for (int i=1;i<values.Count();i++)
                {
                    dictionnary = new Dictionary<string, string>();
                    if (i==1)
                        pinyin = values[i-1];
                    else
                    {
                        pinyin = values[i - 1].Substring(values[i - 1].LastIndexOf(" "), values[i - 1].Length - values[i - 1].LastIndexOf(" "));
                    }

                    string strNext = values[i];
                    string subString = "";
                    if (i != values.Count() - 1)
                        subString = strNext.Substring(0, strNext.LastIndexOf(" "));
                    else
                        subString = strNext;
                    string[] strs = subString.Split('，');
                    for (int j = 0; j < strs.Length; j++)
                    {
                        string strTrimed = strs[j].Trim();
                        if (strTrimed.Length == 0)
                            continue;
                        string key = strTrimed[0].ToString();
                        string value = strTrimed.Substring(1, strTrimed.Length - 1);
                        dictionnary[key] = value;
                    }
                    wubiDic[pinyin] = dictionnary;

                }
                var dd = dictionnary.Where(c => c.Value.Length > 4);
                string json = "";
                int k = 0;
                int h = 0;
                foreach(var py in wubiDic.Keys)
                {
                    k++;
                    h = 0;
                    foreach (var item in wubiDic[py])
                    {
                        h++;
                        string str1 = "";
                        if (h==1)
                        {
                            str1 += string.Format("\"{0}\":[");
                        }
                        string key = item.Key;
                        string value = item.Value;
                        str1 += string.Format("{\"hanzi\":\"{0}\",\"code\":\"{1}\"}", key, value);
                        //if(h!=p.)
                    }
                }
            }
        }
    }
}
