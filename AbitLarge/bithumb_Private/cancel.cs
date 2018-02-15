using System;

namespace AbitLarge
{
    public class cancel : bithumbdata
    {
        //  구매, 판매 거래 취소
        //  true = 구매 false = 판매
        /*  
         *  type String  거래유형(bid : 구매, ask : 판매)
         *  order_id String  판/구매 주문 등록된 주문번호
         *  currency String  BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS(기본값: BTC) 
        */
        /// <summary>
        /// bithumb 회원 판/구매 거래 취소
        /// </summary>
        /// <param name="types">거래유형(bid : 구매, ask : 판매)</param>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS(기본값: BTC)</param>
        public static void Call_cancel(bool types, string order_id,string currency)
        {
            string type = "";
            Humb_BTC_with.Clear();
            if (types == true) type = "bid"; else type = "ask";
            string sParams = "type=" + type + "&order_id=" + order_id + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/trade/cancel", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_cancel.Add("Error","Error occurred!");
                Humb_cancel.Add("Error2", "HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_BTC_with.Add("status", JObj["status"].ToString());
                }
            }
        }
    }
}
