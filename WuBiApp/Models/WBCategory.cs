using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuBiApp.Models
{
    public class WBCategory
    {
        public string PinYin { get; set; }
        public WBWord[] WBList { get; set; }
    }
}
