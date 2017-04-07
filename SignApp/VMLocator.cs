using SignApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SignApp
{
    public class VMLocator
    {
        private static VMLocator instance;
        public static VMLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Application.Current.Resources["Locator"] as VMLocator;
                }
                return instance;
            }
        }

        private SignViewModel signVM;
        public SignViewModel SignVM
        {
            get
            {
                return signVM ?? (signVM = new SignViewModel());
            }
        }

        public void ClearSignVM()
        {
            //SignVM.Cleanup();
            signVM = null;
        }
    }
}
