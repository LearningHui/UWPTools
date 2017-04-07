using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SignApp.Converter
{
    public class ShortTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value as string;
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    DateTime sData = DateTime.Parse(value as string);
                    return string.Format("{0:D2}", sData.Hour) + ":" + string.Format("{0:D2}", sData.Minute);
                }
                catch
                {
                    return "待签";
                }
            }
            return "待签";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
