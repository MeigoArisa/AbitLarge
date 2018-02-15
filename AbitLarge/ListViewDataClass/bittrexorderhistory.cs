using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitLarge.ListViewDataClass
{
    public class bittrexorderhistory
    {
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public string TimeStamp { get; set; }
        public string OrderType { get; set; }
        public string Limit { get; set; }
        public string Quantity { get; set; }
        public string QuantityRemaining { get; set; }
        public string Commission { get; set; }
        public string Price { get; set; }
    }
}/*
"Exchange" : "BTC-LTC",
			"TimeStamp" : "2014-07-09T04:01:00.667",
			"OrderType" : "LIMIT_BUY",
			"Limit" : 0.00000001,
			"Quantity" : 100000.00000000,
			"QuantityRemaining" : 100000.00000000,
			"Commission" : 0.00000000,
			"Price" : 0.00000000,
			"PricePerUnit" : null,
			"IsConditional" : false,
			"Condition" : null,
			"ConditionTarget" : null,
			"ImmediateOrCancel" : false*/