using AbitLarge.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static AbitLarge.bittrexparsing;
namespace AbitLarge
{
    public class bittrex_balances : bittrexdata
    {
        public static void Call_bittrex_balances()
        {
            JObject jobjs = JObject.Parse(CallAPI(bittrexAPI_Key, bittrexSecret_Key, "account/getbalances", "")); //json 객체로
            JArray jarr = JArray.Parse(jobjs["result"].ToString());
            balances_count = 0;
            foreach (JObject jobj in jarr)
            {
                trex_balances.Add("Currency" + balances_count, jobj["Currency"].ToString());
                trex_balances.Add("Balance" + balances_count, ((double)jobj["Balance"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_balances.Add("Available" + balances_count, ((double)jobj["Available"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_balances.Add("Pending" + balances_count, ((double)jobj["Pending"]).ToString("F8", CultureInfo.CreateSpecificCulture("es-ES")));
                trex_balances.Add("CryptoAddress" + balances_count, jobj["CryptoAddress"].ToString());
                balances_count++;
            }
        }
    }
}
