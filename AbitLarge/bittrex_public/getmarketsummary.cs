using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge.bittrex_public
{
    class getmarketsummary
    {
        public static void Call_bittrex_getmarketsummary(string market)
        {
            JArray jarr = JArray.Parse(bittrexparsing.Request_Json("public/getmarketsummary?market=" + market)); //json 객체로
            foreach (JObject jobj in jarr)
            {
                MessageBox.Show(jobj["result"]["MarketCurrency"].ToString());
                MessageBox.Show(jobj["result"]["BaseCurrency"].ToString());
                MessageBox.Show(jobj["result"]["MarketCurrencyLong"].ToString());
                MessageBox.Show(jobj["result"]["BaseCurrencyLong"].ToString());
                MessageBox.Show(jobj["result"]["MinTradeSize"].ToString());
                MessageBox.Show(jobj["result"]["MarketName"].ToString());
                MessageBox.Show(jobj["result"]["IsActive"].ToString());
                MessageBox.Show(jobj["result"]["Created"].ToString());
            }
        }
    }
}
