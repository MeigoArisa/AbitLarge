using System;
using System.Windows;

namespace AbitLarge.bithumb_Public
{
    public class orderbook : bithumbdata
    {
        /*
         *  timestamp	현재 시간 Timestamp
         *  order_currency	주문 화폐단위
         *  payment_currency	결제 화폐단위
         *  bids	구매요청
         *  asks	판매요청
         *  quantity	Currency 수량
         *  price	1Currency당 거래금액
         *//// <summary>
           /// bithumb 거래소 판/구매 등록 대기 또는 거래 중 내역 정보
           /// </summary>
           /// <param name="group_orders">Value : 0 ~ (Default : 0)</param>
           /// <param name="count">Value : 1 ~ 100 (Default : 20)</param>
           /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_Public(string group_orders, int count, string currency)
        {
            Humb_Public_order.Clear();
            string sParams = "group_orders=" + group_orders + "&count=" + count;
            JObj = hAPI_Svr.xcoinApiCall("/public/orderbook/" + currency, sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_Public_order.Add("Error","Error occurred!");
                Humb_Public_order.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    var tmp = JObj["data"]["bids"].ToString().Replace("}", "");
                    tmp = tmp.Replace("{", "");
                    tmp = tmp.Replace(",", "");
                    tmp = tmp.Replace("\r\n\r\n", "");
                    tmp = tmp.Replace("\"", "");
                    tmp = tmp.Replace("[", "");
                    tmp = tmp.Replace("]", "");
                    tmp = tmp.Replace(" ", "");
                    tmp = tmp.Replace(":", ": ");

                    var tmp2 = JObj["data"]["asks"].ToString().Replace("}", "");
                    tmp2 = tmp2.Replace("{", "");
                    tmp2 = tmp2.Replace("\r\n\r\n", "");
                    tmp2 = tmp2.Replace(",", "");
                    tmp2 = tmp2.Replace("\"", "");
                    tmp2 = tmp2.Replace("[", "");
                    tmp2 = tmp2.Replace("]", "");
                    tmp2 = tmp2.Replace(" ", "");
                    tmp2 = tmp2.Replace(":", ": ");

                    string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
                    string[] strResult = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string[] separator2 = new string[1] { "\r\n" };  //분리할 기준 문자열
                    string[] strResult2 = tmp2.Split(separator, StringSplitOptions.RemoveEmptyEntries);


                    Humb_Public_order.Add("status",                 JObj["status"].                     ToString());
                    Humb_Public_order.Add("timestamp",              JObj["data"]["timestamp"].          ToString());
                    Humb_Public_order.Add("order_currency",         JObj["data"]["order_currency"].     ToString());
                    Humb_Public_order.Add("payment_currency",       JObj["data"]["payment_currency"].   ToString());
                    for(int i = 0; i < strResult.Length; i++)
                        Humb_Public_order.Add("bids" + i, strResult[i]);
                    for (int i = 0; i < strResult2.Length; i++)
                        Humb_Public_order.Add("asks" + i, strResult2[i]);
                    Humb_Public_order.Add("Length bids", strResult.Length);
                    Humb_Public_order.Add("Length asks", strResult2.Length);
                }
            }
        }
    }
}
