using JsonHelper;
using LLBT_SignLib;
using SignApp.Model;
using SignApp.Sign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSLib;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SignApp.ViewModel
{
    public class SignViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region NotifyProperty

        //用户名
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged("UserName"); }
        }

        //当前系统时间
        private string _systemTime;
        public string SystemTime
        {
            get { return _systemTime; }
            set
            {
                if (_systemTime != value)
                {
                    _systemTime = value;
                    RaisePropertyChanged("SystemTime");
                }
            }
        }

        //签到时间
        private string signInTime;
        public string SignInTime
        {
            get { return signInTime; }
            set { signInTime = value; RaisePropertyChanged("SignInTime"); }
        }

        //签退时间
        private string signOutTime;
        public string SignOutTime
        {
            get { return signOutTime; }
            set { signOutTime = value; RaisePropertyChanged("SignOutTime"); }
        }
        #endregion

        #region Method
        private async void BindDevice()
        {
            BindDeviceRequest sParams = new BindDeviceRequest();
            sParams.deviceId = Configuration.GetDeviceUniqueId();
            sParams.userName = "zou.penghui";

            HttpSerializeData<BindDeviceRequest> httpSerializeData = new HttpSerializeData<BindDeviceRequest>("bindDevice.html", sParams);
            string url = httpSerializeData.GetURL();
            NetworkRequest networkRequest = NetworkRequest.CreateHttp(httpSerializeData.GetURL());
            var result = await networkRequest.PostAsync<string>(JsonAnalysis.Serialize(httpSerializeData));
        }

        public async Task<bool> GetUserInfo()
        {
            GetUserInforRequestParams sParams = new GetUserInforRequestParams();
            sParams.deviceId = Configuration.GetDeviceUniqueId();
            HttpSerializeData<GetUserInforRequestParams> httpSerializeData = new HttpSerializeData<GetUserInforRequestParams>("getUserInfo.html", sParams);
            NetworkRequest networkRequest = NetworkRequest.CreateHttp(httpSerializeData.GetURL());
            var resultJson = await networkRequest.PostAsync<string>(JsonAnalysis.Serialize(httpSerializeData));
            if(!string.IsNullOrEmpty(resultJson))
            {
                HttpDeserializeData<GetUserInforResponseData> sData = JsonAnalysis.Deserialize<HttpDeserializeData<GetUserInforResponseData>>(resultJson);

                if (sData.result.error == "01")   // 未绑定账户
                {
                    //todo:跳转到绑定页面，进行用户绑定
                    (Window.Current.Content as Frame).Navigate(typeof(BindDevicePage));                    
                }
                else if (sData.status.code.Equals("0000"))
                {
                    //todo:跳转到签到页面
                    (Window.Current.Content as Frame).Navigate(typeof(SignPage));                    
                }
                else
                {

                }
                return true;
            }
            return false;
        }

        private async void InitData()
        {
            //DoSignRequestParams sParams = new DoSignRequestParams();
            //sParams.deviceId = "111111";
            //sParams.queryOny = "false";
            //HttpSerializeData<DoSignRequestParams> httpSerializeData = new HttpSerializeData<DoSignRequestParams>("doSign.html", sParams);
            //httpSerializeData.GetURL();//

            //NetworkRequest networkRequest = NetworkRequest.CreatHttp(httpSerializeData.GetURL());
            //networkRequest.Body= JsonAnalysis.Serialize(httpSerializeData);
            //var result = await networkRequest.PostAsync<string>();
            string str = "{\"result\": {\"signInTime\": \"2016/10/13\",\"userName\": \"zouph\"}}";

            //result_tbk.Text = JsonAnalysis.Serialize(httpSerializeData);//
            ////var obj = JsonAnalysis.Deserialize<DoSignRequestParams>(result_tbk.Text);


            HttpDeserializeData<DoSignResponseData> sData = JsonAnalysis.Deserialize<HttpDeserializeData<DoSignResponseData>>(str);
        }

        private async void Sign()
        {
            await DoSign("true");
        }

        /// <summary>
        /// 签到，查询
        /// <param name="queryOnly">true:查询 false:签到</param>
        /// </summary>
        /// <returns></returns>
        public async Task<DoSignResponseData> DoSign(string queryOnly)
        {
            DoSignRequestParams sParams = new DoSignRequestParams();
            sParams.deviceId = Configuration.GetDeviceUniqueId();
            sParams.queryOny = queryOnly;
            HttpSerializeData<DoSignRequestParams> httpSerializeData = new HttpSerializeData<DoSignRequestParams>("doSign.html", sParams);
            NetworkRequest networkRequest = NetworkRequest.CreateHttp(httpSerializeData.GetURL());
            var resultJson = await networkRequest.PostAsync<string>(JsonAnalysis.Serialize(httpSerializeData));
            HttpDeserializeData<DoSignResponseData> sData = JsonAnalysis.Deserialize<HttpDeserializeData<DoSignResponseData>>(resultJson);
            if(sData == null)
            {
                //todo:报错
                var res = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var msg = new MessageDialog("网络请求错误！").ShowAsync();
                });
            }
            if (queryOnly.Equals("false"))
            {
            }
            if (sData.result.error == "00")// 签到成功
            {
                this.SystemTime = sData.result.sysTime;
                this.SignInTime = sData.result.signInTime;
                this.SignOutTime = sData.result.signOutTime;
                return sData.result;
            }
            else if (sData.status.code.Equals("0000"))
            {
                this.SystemTime = sData.result.sysTime;
                this.SignInTime = sData.result.signInTime;
                this.SignOutTime = sData.result.signOutTime;
                if (queryOnly.Equals("false"))
                {
                    //todo:报错
                    var res = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        var msg = new MessageDialog(sData.result.errorMsg).ShowAsync();
                    });
                }
                return sData.result;
            }
            else
            {
                return null;
            }

        }
        #endregion
    }
}
