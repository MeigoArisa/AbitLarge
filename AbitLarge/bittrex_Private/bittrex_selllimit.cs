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
    public class bittrex_selllimit : bittrexdata
    {
        public static string Call_bittrex_selllimit(string market, string quantity, string rate)
        {
            JObject jobjs = JObject.Parse(CallAPI(bittrexAPI_Key, bittrexSecret_Key, "market/selllimit", $"market={market}&quantity={quantity}&rate={rate}")); //json 객체로
            string result = "";
            if (jobjs["success"].ToString() == "true") result = "success";
            else result = jobjs["message"].ToString();
            return result;
        }
    }
}
