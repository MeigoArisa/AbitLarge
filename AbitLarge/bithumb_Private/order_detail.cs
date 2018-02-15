using System;

namespace AbitLarge.bithumb_Private
{
    public class order_detail : bithumbdata
    {
        /*  
         *  transaction_date	채결 시간 Timestamp
         *  type	bid(구매), ask(판매)
         *  order_currency	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS
         *  payment_currency	KRW
         *  units_traded	체결 수량
         *  price	1Currency당 체결가 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  fee	수수료
         *  total	체결가
         *  order_id	String	판/구매 주문 등록된 주문번호
         *  type	String	거래유형 (bid : 구매, ask : 판매)
         *  order_id	String	판/구매 주문 등록된 주문번호
         *  type	String	거래유형 (bid : 구매, ask : 판매)
         *  currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         */
        /// <summary>
        /// bithumb 회원 판/구매 체결 내역
        /// </summary>
        /// <param name="order_id">판/구매 주문 등록된 주문번호</param>
        /// <param name="types">거래유형 (bid : 구매, ask : 판매)</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public void Call_order_detail(string order_id, bool types,string currency)
        {
            Humb_detail.Clear();
            string type = "";
            if (types == true) type = "bid"; else type = "ask";

            string sParams = "order_id=" + order_id + "&type=" + type + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/order_detail", sParams, ref sRespBodyData);

            if (JObj == null)
            {
                Humb_detail.Add("Error","Error occurred!");
                Humb_detail.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_detail.Add("status",               JObj["status"].                     ToString());
                    Humb_detail.Add("transaction_date",     JObj["data"]["transaction_date"].   ToString());
                    Humb_detail.Add("type",                 JObj["data"]["type"].               ToString());
                    Humb_detail.Add("order_currency",       JObj["data"]["order_currency"].     ToString());
                    Humb_detail.Add("payment_currency",     JObj["data"]["payment_currency"].   ToString());
                    Humb_detail.Add("units_traded",         JObj["data"]["units_traded"].       ToString());
                    Humb_detail.Add("price",                JObj["data"]["price"].              ToString());
                    Humb_detail.Add("fee",                  JObj["data"]["fee"].                ToString());
                    Humb_detail.Add("total",                JObj["data"]["total"].              ToString());
                }
            }
        }
    }
}
