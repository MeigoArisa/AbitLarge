using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Windows;

namespace AbitLarge.bithumb_Public
{
    public class recent_transactions : bithumbdata
    {
        /// <summary>
        /// bithumb 거래소 거래 체결 완료 내역
        /// </summary>
        /// <param name="offset">Value : 0 또는 1 (Default : 1)</param>
        /// <param name="count">Value : 1 ~ 50 (Default : 20), ALL : 1 ~ 5(Default : 5)</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC), ALL(전체)</param>
        public static void Call_Public(string currency, int offset, int count)
        {
            Humb_Public_rec_trans.Clear();
            recent_transactions_count = 0;
            string sParams = "offset=" + offset + "&count=" + count;
            JObj = hAPI_Svr.xcoinApiCall("/public/recent_transactions/"+ currency, sParams, ref sRespBodyData);
            JArray jarr = JArray.Parse(JObj["data"].ToString());
            if (JObj == null)
            {
                Humb_Public_rec_trans.Add("Error","Error occurred!");
                Humb_Public_rec_trans.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    foreach (JObject jobj in jarr)
                    {

                        Humb_Public_rec_trans.Add("cont_no" + recent_transactions_count, jobj["cont_no"].ToString());
                        Humb_Public_rec_trans.Add("transaction_date" + recent_transactions_count, jobj["transaction_date"].ToString());
                        Humb_Public_rec_trans.Add("type" + recent_transactions_count, jobj["type"].ToString());
                        Humb_Public_rec_trans.Add("units_traded" + recent_transactions_count, ((double)jobj["units_traded"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                        Humb_Public_rec_trans.Add("price" + recent_transactions_count, ((double)jobj["price"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                        Humb_Public_rec_trans.Add("total" + recent_transactions_count, ((double)jobj["total"]).ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                        recent_transactions_count++;
                    }
                }
            }
        }
    }
}
