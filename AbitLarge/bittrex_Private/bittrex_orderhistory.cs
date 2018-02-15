using AbitLarge.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbitLarge.bittrexparsing;
namespace AbitLarge.bittrex_Private
{
    class bittrex_orderhistory : bittrexdata
    {
        public static void Call_bittrex_getorderhistory(string market)
        {
            JObject jobjs = JObject.Parse(CallAPI(bittrexAPI_Key, bittrexSecret_Key, "market/getopenorders", $"market={market}")); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            getorderhistory_count = 0;
            foreach (JObject jobj in jarr)
            {
                trex_getorderhistory.Add("OrderUuid" + getorderhistory_count, jobj["OrderUuid"].ToString());
                trex_getorderhistory.Add("Exchange" + getorderhistory_count, jobj["Exchange"].ToString());
                trex_getorderhistory.Add("TimeStamp" + getorderhistory_count, jobj["TimeStamp"].ToString());
                trex_getorderhistory.Add("OrderType" + getorderhistory_count, jobj["OrderType"].ToString());
                trex_getorderhistory.Add("Limit" + getorderhistory_count, ((double)jobj["Limit"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_getorderhistory.Add("Quantity" + getorderhistory_count, ((double)jobj["Quantity"]).ToString());
                trex_getorderhistory.Add("QuantityRemaining" + getorderhistory_count, ((double)jobj["QuantityRemaining"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_getorderhistory.Add("Commission" + getorderhistory_count, jobj["Commission"].ToString());
                trex_getorderhistory.Add("Price" + getorderhistory_count, ((double)jobj["Price"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_getorderhistory.Add("PricePerUnit" + getorderhistory_count, jobj["PricePerUnit"].ToString());
                getorderhistory_count++;
            }
        }
    }
}
