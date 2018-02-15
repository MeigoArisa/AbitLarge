using AbitLarge.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static AbitLarge.bittrexparsing;
namespace AbitLarge.bittrex_Private
{
    public class bittrex_getopenorder : bittrexdata
    {
        public static void Call_bittrex_getopenrder(string market)
        {

            JObject jobjs = JObject.Parse(CallAPI(bittrexAPI_Key, bittrexSecret_Key, "market/getopenorders", $"market={market}")); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            getopenrder_count = 0;
            foreach (JObject jobj in jarr)
            {
                trex_getopenrder.Add("OrderUuid" + getopenrder_count, jobj["OrderUuid"].ToString());
                trex_getopenrder.Add("Exchange" + getopenrder_count, jobj["Exchange"].ToString());
                trex_getopenrder.Add("OrderType" + getopenrder_count, jobj["OrderType"].ToString());
                trex_getopenrder.Add("Quantity" + getopenrder_count, jobj["Quantity"].ToString());
                trex_getopenrder.Add("QuantityRemaining" + getopenrder_count, jobj["QuantityRemaining"].ToString());
                trex_getopenrder.Add("Limit" + getopenrder_count, jobj["Limit"].ToString());
                trex_getopenrder.Add("CommissionPaid" + getopenrder_count, jobj["CommissionPaid"].ToString());
                trex_getopenrder.Add("Price" + getopenrder_count, jobj["Price"].ToString());
                trex_getopenrder.Add("PricePerUnit" + getopenrder_count, jobj["PricePerUnit"].ToString());
                trex_getopenrder.Add("Opened" + getopenrder_count, jobj["Opened"].ToString());
                trex_getopenrder.Add("Closed" + getopenrder_count, jobj["Closed"].ToString());
                getopenrder_count++;
            }
        }
    }
}
