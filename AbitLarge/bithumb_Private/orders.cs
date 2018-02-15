using System;

namespace AbitLarge.bithumb_Private
{
    public class orders : bithumbdata
    {
        /*
         *  order_currency	주문 화폐단위
         *  order_date	주문일시 Timestamp
         *  payment_currency	결제 화폐단위
         *  type	주문요청 구분 (bid : 구매, ask : 판매)
         *  status	주문상태(placed : 거래 진행 중)
         *  units	거래요청 Currency (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  units_remaining	주문 체결 잔액
         *  price	1Currency당 거래금액 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  fee	거래 수수료
         *  total	거래체결 완료 총 거래금액, 완료상태가 아닌 경우 NULL
         *  date_completed	거래체결 완료일시 Timestamp, 완료상태가 아닌 경우 NULL
         *  order_id	String	판/구매 주문 등록된 주문번호
         *  type	String	거래유형(bid : 구매, ask : 판매)
         *  count	Int	Value : 1 ~1000 (default : 100)
         *  after	Int	YYYY-MM-DD hh:mm:ss 의 UNIX Timestamp
         *  (2014-11-28 16:40:01 = 1417160401000)
         *  currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         */
        /// <summary>
        /// 판/구매 거래 주문 등록 또는 진행 중인 거래
        /// </summary>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="types">주문요청 구분 (bid : 구매, ask : 판매)</param>
        /// <param name="count">Value : 1 ~1000 (default : 100)</param>
        /// <param name="after">	YYYY-MM-DD hh:mm:ss 의 UNIX Timestamp(2014-11-28 16:40:01 = 1417160401000)</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_orders(string order_id, bool types, int count, int after, string currency)
        {
            Humb_orders.Clear();
            string type = "";
            if (types == true) type = "bid"; else type = "ask";
            string sParams = "order_id=" + order_id + "&type=" + type + "&count=" + count + "&after=" + after + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/orders", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_orders.Add("Error","Error occurred!");
                Humb_orders.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_orders.Add("status",               JObj["status"].                     ToString());
                    Humb_orders.Add("order_currency",       JObj["data"]["order_currency"].     ToString());
                    Humb_orders.Add("order_date",           JObj["data"]["order_date"].         ToString());
                    Humb_orders.Add("payment_currency",     JObj["data"]["payment_currency"].   ToString());
                    Humb_orders.Add("data_status",          JObj["data"]["status"].             ToString()); // 주문상태
                    Humb_orders.Add("units",                JObj["data"]["units"].              ToString());
                    Humb_orders.Add("units_remaining",      JObj["data"]["units_remaining"].    ToString());
                    Humb_orders.Add("price",                JObj["data"]["price"].              ToString());
                    Humb_orders.Add("fee",                  JObj["data"]["fee"].                ToString());
                    Humb_orders.Add("total",                JObj["data"]["total"].              ToString());
                    Humb_orders.Add("date_completed",       JObj["data"]["date_completed"].     ToString());
                }
            }
        }
    }
}
