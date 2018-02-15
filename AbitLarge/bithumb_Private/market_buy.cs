using System;
using System.Windows;

namespace AbitLarge.bithumb_Private
{
    public class market_buy : bithumbdata
    {
        /*
         *  order_id 주문 번호
         *  cont_id 체결 번호
         *  units 총 구매 수량(수수료 포함)
         *  price	1Currency당 KRW 시세(BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  total 구매 KRW
         *  fee 구매 수수료
         *  units	Float	주문 수량
         *
         *  - 1회 최소 수량 (BTC: 0.001 | ETH: 0.01 | DASH: 0.01 | LTC: 0.1 | ETC: 0.1 | XRP: 10 | BCH: 0.001 | XMR: 0.01 | ZEC: 0.001 | QTUM: 0.1 | BTG: 0.01 | EOS: 1)
         *  - 1회 거래 한도 : 1억원
        */
        /// <summary>
        /// 시장가 구매
        /// </summary>
        /// <param name="units">주문 수량</param>
        /// <param name="currency">(BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)</param>
        public static void Call_market_buy(float units, string currency)
        {
            Humb_buy.Clear();
            string sParams = "units=" + units + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/trade/market_buy", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_buy.Add("Error","Error occurred!");
                Humb_buy.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    var tmp = JObj["data"].ToString().Replace("}", "");
                    tmp = tmp.Replace("{", "");
                    tmp = tmp.Replace(",", "");
                    tmp = tmp.Replace("\r\n\r\n", "");
                    tmp = tmp.Replace("\"", "");
                    tmp = tmp.Replace("[", "");
                    tmp = tmp.Replace("]", "");
                    tmp = tmp.Replace(" ", "");
                    tmp = tmp.Replace(":", "");
                    tmp = tmp.Replace("cont_id", "");
                    tmp = tmp.Replace("units", "");
                    tmp = tmp.Replace("price", "");
                    tmp = tmp.Replace("total", "");
                    string[] separator = new string[1] { "\r\n" };
                    string[] strResult = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    buy_count = 0;
                    for (int i = 0; i < (strResult.Length / 6); i++)
                    {

                        Humb_buy.Add("order_id" + i, strResult[(buy_count * 6)]);
                        Humb_buy.Add("cont_id" + i, strResult[(buy_count *  6) +1]);
                        Humb_buy.Add("units" + i, strResult[(buy_count *  6) + 2]);
                        Humb_buy.Add("price" + i, strResult[(buy_count *  6) + 3]);
                        Humb_buy.Add("total" + i, strResult[(buy_count *  6) + 4]);
                        Humb_buy.Add("fee" + i, strResult[(buy_count * 6) + 5]);
                        buy_count += 1;
                    }
                    Humb_buy.Add("status",          JObj["status"].             ToString());
                    Humb_buy.Add("order_id",        JObj["order_id"].           ToString());
                }
            }
        }
    }
}
