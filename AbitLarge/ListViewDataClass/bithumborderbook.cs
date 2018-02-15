using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitLarge.ListViewDataClass
{
    public class bithumborderbook
    {
        public string timestamp { get; set; }
        public string order_currency { get; set; }
        public string payment_currency { get; set; }
        public string bids { get; set; }
        public string asks { get; set; }
    }
}
