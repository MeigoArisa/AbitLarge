using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge
{
    public class bittrexparsing
    {
        public static string Request_Json(string bittrexurl)
        {
            string result = null;
            string url = $"https://bittrex.com/api/v1.1/{bittrexurl}";
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
            return result;
        }



        public static string CallAPI(string key, string secret, string method, string parameter)
        {
            string
                urlSign = "",
                url = $"https://bittrex.com/api/v1.1/{method}",
                nonce = DateTime.Now.Ticks.ToString();

            method = method.ToLower().Trim();
            parameter = parameter.Trim();

            WebClient wc = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (parameter != "")
            {
                url += $"?{parameter}";
            }
            url += (parameter == "" ? "?" : "&") + $"apikey={key}&nonce={nonce}";

            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            using (var hmac = new HMACSHA512(secretBytes))
            {
                //compute hash
                byte[] hashResult = hmac.ComputeHash(Encoding.UTF8.GetBytes(url));

                //convert to hex string
                for (int i = 0; i < hashResult.Length; i++)
                    urlSign += hashResult[i].ToString("X2");
            }
            wc.Headers.Add("apisign", urlSign);
            string result = wc.DownloadString(url);
            return result;
        }
    }
}
