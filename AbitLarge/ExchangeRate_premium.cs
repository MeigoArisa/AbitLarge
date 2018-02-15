using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge
{
    public static class ExchangeRate
    {
        public static string Request_Json(string currency, string money)
        {
            Hashtable currencys = new Hashtable();
            bithumb_Public.ticker.Call_Public(currency);
            double c_temp = Convert.ToDouble(bithumbdata.Humb_Public_ticker["min_price"]);
            string result = null;
            double CoinMarket = 0; // 코인마켓 시세
            double Other_countries_rate = 0; // 타 국가 화폐 환율
            double humb_close = Math.Truncate(c_temp - (c_temp * 0.0015)); // 빗썸 마지막 거래 시세



            JArray jarr = null;
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://bittrex.com/api/v1.1/public/getcurrencies"); //API 사이트에서 json 받아옴.
                JObject jarrr = null;
                jarrr = JObject.Parse(json);
                foreach (JObject jobj in jarrr["result"])
                {
                    currencys.Add(jobj["Currency"].ToString(), jobj["CurrencyLong"].ToString().Replace(" ","-"));
                }
            }


            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://api.coinmarketcap.com/v1/ticker/" + (string)currencys[currency] + "?convert=" + money); //API 사이트에서 json 받아옴
                jarr = JArray.Parse(json);
                foreach (JObject jobj in jarr)
                {
                    CoinMarket = (double)jobj["price_" + money.ToLower()];
                }
            }

            string url = "http://api.manana.kr/exchange/rate/KRW/" + money +".json";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                stream.Close();
                response.Close();
            }
            catch (Exception e)
            {
            }


            jarr = JArray.Parse(result); //json 객체로
            foreach (JObject jobj in jarr)
            {
                Other_countries_rate = (double)jobj["rate"];
            }

            result = Math.Round((humb_close / (CoinMarket * Other_countries_rate)), 2).ToString().Replace("1.","") + "%";
            return result;
        }
        
    }
}