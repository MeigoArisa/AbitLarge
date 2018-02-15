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
    class getmarketsummaries : bittrexdata
    {
        public static void Call_bittrex_getmarketsummaries()
        {
            JObject jobjs = JObject.Parse(bittrexparsing.Request_Json("public/getmarketsummaries")); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            getmarketsummaries_count = 0;
            trex_summaries.Clear();
            foreach (JObject jobj in jarr)
            {
                trex_summaries.Add("MarketName" + getmarketsummaries_count, jobj["MarketName"].ToString());
                trex_summaries.Add("High" + getmarketsummaries_count, ((double)jobj["High"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("Low" + getmarketsummaries_count, ((double)jobj["Low"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("Volume" + getmarketsummaries_count, ((double)jobj["Volume"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("Last" + getmarketsummaries_count, ((double)jobj["Last"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("BaseVolume" + getmarketsummaries_count, ((double)jobj["BaseVolume"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("TimeStamp" + getmarketsummaries_count, jobj["TimeStamp"].ToString());
                trex_summaries.Add("Bid" + getmarketsummaries_count, ((double)jobj["Bid"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("Ask" + getmarketsummaries_count, ((double)jobj["Ask"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("OpenBuyOrders" + getmarketsummaries_count, jobj["OpenBuyOrders"].ToString());
                trex_summaries.Add("OpenSellOrders" + getmarketsummaries_count, jobj["OpenSellOrders"].ToString());
                trex_summaries.Add("PrevDay" + getmarketsummaries_count, ((double)jobj["PrevDay"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_summaries.Add("Created" + getmarketsummaries_count, jobj["Created"].ToString());
                getmarketsummaries_count++;
            }
        }
    }
}
