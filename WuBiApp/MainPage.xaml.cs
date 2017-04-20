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
using WuBiApp.Models;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace WuBiApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<WBCategory> WBCategories;
        public MainPage()
        {
            this.InitializeComponent();           
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            #region Old
            //using (var jsonStream = await StreamHelper.GetPackagedFileStreamAsync("Data/字库.txt"))
            //{
            //    var str = await jsonStream.ReadTextAsync(Encoding.UTF8);
            //    str = str.Replace("\n", " ");
            //    string[] values = str.Split(':');
            //    Dictionary<string, string> dictionnary = new Dictionary<string, string>();
            //    Dictionary<string, Dictionary<string, string>> wubiDic = new Dictionary<string, Dictionary<string, string>>();
            //    string pinyin = "";
            //    for (int i=1;i<values.Count();i++)
            //    {
            //        dictionnary = new Dictionary<string, string>();
            //        if (i==1)
            //            pinyin = values[i-1];
            //        else
            //        {
            //            pinyin = values[i - 1].Substring(values[i - 1].LastIndexOf(" "), values[i - 1].Length - values[i - 1].LastIndexOf(" "));
            //        }

            //        string strNext = values[i];
            //        string subString = "";
            //        if (i != values.Count() - 1)
            //            subString = strNext.Substring(0, strNext.LastIndexOf(" "));
            //        else
            //            subString = strNext;
            //        string[] strs = subString.Split('，');
            //        for (int j = 0; j < strs.Length; j++)
            //        {
            //            string strTrimed = strs[j].Trim();
            //            if (strTrimed.Length == 0)
            //                continue;
            //            string key = strTrimed[0].ToString();
            //            string value = strTrimed.Substring(1, strTrimed.Length - 1);
            //            dictionnary[key] = value;
            //        }
            //        wubiDic[pinyin] = dictionnary;

            //    }
            //    var dd = dictionnary.Where(c => c.Value.Length > 4);
            //    //string json = "{";
            //    //int k = 0;
            //    //int h = 0;                

            //    //foreach (var py in wubiDic)
            //    //{
            //    //    k++;
            //    //    h = 0;
            //    //    string str1 = "";
            //    //    foreach (var item in py.Value)
            //    //    {
            //    //        h++;

            //    //        if (h == 1)
            //    //        {
            //    //            str1 = "";
            //    //            str1 += string.Format("\"{0}\":[", py.Key.Trim());
            //    //        }
            //    //        string key = item.Key.Trim();
            //    //        string value = item.Value.Trim();
            //    //        str1 += "{" + string.Format("\"hanzi\":\"{0}\",\"code\":\"{1}\"", key, value) + "}";
            //    //        if (h != py.Value.Count)
            //    //        {
            //    //            str1 += ",";
            //    //        }
            //    //        else
            //    //        {
            //    //            str1 += "]";
            //    //            json += str1;
            //    //            if(k!=wubiDic.Values.Count)
            //    //            {
            //    //                json += ",";
            //    //            }
            //    //        }
            //    //    }
            //    //}
            //    //json += "}";

            //    string json = "[";
            //    int k = 0;
            //    int h = 0;

            //    foreach (var py in wubiDic)
            //    {
            //        k++;
            //        h = 0;
            //        string str1 = "";
            //        foreach (var item in py.Value)
            //        {
            //            h++;

            //            if (h == 1)
            //            {
            //                str1 = "{\"PinYin\":" + "\"" + py.Key.Trim() + "\",\"WBList\":[";

            //                //str1 += string.Format("\"{0}\":[", py.Key.Trim());
            //            }
            //            string key = item.Key.Trim();
            //            string value = item.Value.Trim();
            //            str1 += "{" + string.Format("\"HanZi\":\"{0}\",\"Code\":\"{1}\"", key, value) + "}";
            //            if (h != py.Value.Count)
            //            {
            //                str1 += ",";
            //            }
            //            else
            //            {
            //                str1 += "]}";
            //                json += str1;
            //                if (k != wubiDic.Values.Count)
            //                {
            //                    json += ",";
            //                }
            //            }
            //        }
            //    }
            //    json += "]";
            //    var obj = JsonConvert.DeserializeObject<List<WBCategory>>(json);

            //} 
            #endregion
            if (WBCategories == null)
            {
                using (var jsonStream = await StreamHelper.GetPackagedFileStreamAsync("Data/WB.json"))
                {
                    var jsonString = await jsonStream.ReadTextAsync(Encoding.UTF8);
                    WBCategories = JsonConvert.DeserializeObject<List<WBCategory>>(jsonString);
                }
            }

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string hanzi = "";
            if (string.IsNullOrEmpty(hanZiTB.Text))
                return;
            hanzi = hanZiTB.Text;
            foreach(var category in WBCategories)
            {
                bool isFound = false;
                for(int i=0;i<category.WBList.Count();i++)
                {
                    if(category.WBList[i].HanZi.Equals(hanzi))
                    {
                        this.codeTbk.Text = category.WBList[i].Code;
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                    break;
            }
        }
    }
}
