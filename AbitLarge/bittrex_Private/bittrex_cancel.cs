using AbitLarge.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows;
using static AbitLarge.bittrexparsing;

namespace AbitLarge.bittrex_Private
{
    public class bittrex_cancel : bittrexdata
    {
        public static string Call_bittrex_cancel(string uuid)
        {
            JObject jobjs = JObject.Parse(CallAPI(bittrexAPI_Key, bittrexSecret_Key, "market/selllimit", $"uuid={uuid}"));
            string result = "";
            if (jobjs["success"].ToString() == "true") result = "success";
            else result = jobjs["message"].ToString();
            return result;
        }
    }
}
