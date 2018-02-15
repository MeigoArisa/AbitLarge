using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge.bittrex_public
{
    public class getcurrencies
    {
        public static void Call_bittrex_getcurrencies()
        {
            JArray jarr = JArray.Parse(bittrexparsing.Request_Json("public/getcurrencies")); //json 객체로
            foreach (JObject jobj in jarr)
            {
                MessageBox.Show(jobj["result"]["Currency"].ToString());
                MessageBox.Show(jobj["result"]["CurrencyLong"].ToString());
                MessageBox.Show(jobj["result"]["MinConfirmation"].ToString());
                MessageBox.Show(jobj["result"]["TxFee"].ToString());
                MessageBox.Show(jobj["result"]["IsActive"].ToString());
                MessageBox.Show(jobj["result"]["CoinType"].ToString());
                MessageBox.Show(jobj["result"]["BaseAddress"].ToString());
            }
        }
    }
}
