using System;
using System.Windows;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AbitLarge.bithumb_Private
{
    public class market_sell : bithumbdata
    {
        /*
         *  order_id	주문 번호
         *  cont_id	체결 번호
         *  units	총 판매 수량(수수료 포함)
         *  price	1Currency당 KRW 시세 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  total	판매 KRW
         *  fee	판매 수수료
         *  units	Float	주문 수량
         *
         *  - 1회 최소 수량 (BTC: 0.001 | ETH: 0.01 | DASH: 0.01 | LTC: 0.1 | ETC: 0.1 | XRP: 10 | BCH: 0.001 | XMR: 0.01 | ZEC: 0.001 | QTUM: 0.1 | BTG: 0.01 | EOS: 1)
         *  - 1회 거래 한도 : 1억원
         *  currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         */
        /// <summary>
        /// 시장가 판매
        /// </summary>
        /// <param name="units">총 판매 수량(수수료 포함)</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_market_sell(float units, string currency)
        {
            Humb_sell.Clear();
            string sParams = "units=" + units + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/trade/market_sell", sParams, ref sRespBodyData);
            
            if (JObj == null)
            {
                Humb_sell.Add("Error", "Error occurred!");
                Humb_sell.Add("Error2", "HTTP Response JSON Data: {0}" + sRespBodyData);
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
                    sell_count = 0;
                    for (int i = 0; i < (strResult.Length / 5); i++)
                    {
                        Humb_sell.Add("cont_id" + i, strResult[(sell_count *5) ]);
                        Humb_sell.Add("units" + i, strResult[(sell_count * 5) + 1]);
                        Humb_sell.Add("price" + i, strResult[(sell_count * 5) + 2]);
                        Humb_sell.Add("total" + i, strResult[(sell_count * 5) + 3]);
                        Humb_sell.Add("fee" + i, strResult[(sell_count * 5) + 4]);
                        sell_count += 1;
                    }

                    Humb_sell.Add("status",     JObj["status"].             ToString());
                    Humb_sell.Add("order_id",   JObj["order_id"].           ToString());
                }
            }
        }
    }
}
