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
    class getmarkethistory : bittrexdata
    {
        public static void Call_bittrex_getmarkethistory(string market)
        {
            trex_history.Clear();
            JObject jobjs = JObject.Parse(bittrexparsing.Request_Json("public/getmarkethistory?market=" + market)); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            getmarkethistory_count = 0;
            foreach (JObject jobj in jarr)
            {
                trex_history.Add("Id" + getmarkethistory_count, jobj["Id"].ToString());
                trex_history.Add("TimeStamp" + getmarkethistory_count, jobj["TimeStamp"].ToString());
                trex_history.Add("Quantity" + getmarkethistory_count, jobj["Quantity"].ToString());
                trex_history.Add("Price" + getmarkethistory_count, jobj["Price"].ToString());
                trex_history.Add("Total" + getmarkethistory_count, jobj["Total"].ToString());
                trex_history.Add("FillType" + getmarkethistory_count, jobj["FillType"].ToString());
                trex_history.Add("OrderType" + getmarkethistory_count, jobj["OrderType"].ToString());
                getmarkethistory_count++;
            }
        }
    }
}
