using AbitLarge.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge.bittrex_public
{
    class getorderbook : bittrexdata
    {
        //type both, buy, sell
        public static void Call_bittrex_getorderbook(string market)
        {
            JObject jobjs = JObject.Parse(bittrexparsing.Request_Json("public/getorderbook?market=" + market + "&type=" + "both")); //json 객체로;
            JArray jarr = JArray.Parse(jobjs["result"]["buy"].ToString());
            JArray jarr2 = JArray.Parse(jobjs["result"]["sell"].ToString());

            getorderbook_count = 0;
            getorderbook_count2 = 0;

            trex_orderbook.Clear();

            foreach (JObject jobj in jarr)
            {
                trex_orderbook.Add("buy_Quantity" + getorderbook_count, ((double)jobj["Quantity"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_orderbook.Add("buy_Rate" + getorderbook_count, ((double)jobj["Rate"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                getorderbook_count++;
            }

            foreach (JObject jobj in jarr2)
            {
                trex_orderbook.Add("sell_Quantity" + getorderbook_count2, ((double)jobj["Quantity"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_orderbook.Add("sell_Rate" + getorderbook_count2, ((double)jobj["Rate"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                getorderbook_count2++;
            }
        }
    }
}
