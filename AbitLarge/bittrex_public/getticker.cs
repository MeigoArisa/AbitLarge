using AbitLarge.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge.bittrex_public
{
    public class getticker : bittrexdata
    {
        public static void Call_bittrex_getticker(string market)
        {
            JObject jobjs = JObject.Parse(bittrexparsing.Request_Json("public/getticker/?market=" + market)); //json 객체로
            trex_ticker.Clear();
            trex_ticker.Add("Bid", jobjs["result"]["Bid"].ToString());
            trex_ticker.Add("Ask", jobjs["result"]["Ask"].ToString());
            trex_ticker.Add("Last", jobjs["result"]["Last"].ToString());
        }
    }
}
