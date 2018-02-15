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
    public class getmarkets : bittrexdata
    {
        public static void Call_bittrex_getmarkets()
        {
            JObject jobjs = JObject.Parse(bittrexparsing.Request_Json("public/getmarkets")); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            getmarkets_count = 0;
            trex_market.Clear();
            foreach (JObject jobj in jarr)
            {
                trex_market.Add("MarketCurrency" + getmarkets_count, jobj["MarketCurrency"].ToString());
                trex_market.Add("BaseCurrency" + getmarkets_count, jobj["BaseCurrency"].ToString());
                trex_market.Add("MarketCurrencyLong" + getmarkets_count, jobj["MarketCurrencyLong"].ToString());
                trex_market.Add("BaseCurrencyLong" + getmarkets_count, jobj["BaseCurrencyLong"].ToString());
                trex_market.Add("MinTradeSize" + getmarkets_count, jobj["MinTradeSize"].ToString());
                trex_market.Add("MarketName" + getmarkets_count, jobj["MarketName"].ToString());
                trex_market.Add("IsActive" + getmarkets_count, jobj["IsActive"].ToString());
                trex_market.Add("Created" + getmarkets_count, jobj["Created"].ToString());
                getmarkets_count++;
            }
        }
    }
}
