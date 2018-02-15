using System;

namespace AbitLarge.bithumb_Private
{
    public class place : bithumbdata
    {
        /*
         *  order_id	주문번호
         *  cont_id	체결번호
         *  units	체결 수량
         *  price	1Currency당 체결가 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  total	KRW 체결가
         *  fee	수수료
         *  order_currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         *  Payment_currency	String	KRW (기본값)
         *  units	Float	주문 수량
         *
         *   - 1회 최소 수량 (BTC: 0.001 | ETH: 0.01 | DASH: 0.01 | LTC: 0.1 | ETC: 0.1 | XRP: 10 | BCH: 0.001 | XMR: 0.01 | ZEC: 0.001 | QTUM: 0.1 | BTG: 0.01 | EOS: 1)
         *   - 1회 최대 수량 (BTC: 300 | ETH: 2,500 | DASH: 4,000 | LTC: 15,000 | ETC: 30,000 | XRP: 2,500,000 | BCH: 1,200 | XMR: 10,000 | ZEC: 2,500 | QTUM: 30,000 | BTG: 1,200 | EOS: 100,000)
         *   price	Int	1Currency당 거래금액 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *   type	String	거래유형 (bid : 구매, ask : 판매)
         */
        /// <summary>
        /// bithumb 회원 판/구매 거래 주문 등록 및 체결
        /// </summary>
        /// <param name="order_currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        /// <param name="Payment_currency">KRW (기본값)</param>
        /// <param name="units">주문 수량</param>
        /// <param name="price">1Currency당 거래금액 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)</param>
        /// <param name="types">거래유형 (bid : 구매, ask : 판매)</param>
        public void Call_place(string order_currency, string Payment_currency, float units, double price, bool types)
        {
            Humb_place.Clear();

            string type = "";
            if (types == true) type = "bid"; else type = "ask";

            string sParams = "order_currency=" + order_currency + "&Payment_currency=" + Payment_currency + "&units=" + units + "&price=" + price + "&type=" + type;
            JObj = hAPI_Svr.xcoinApiCall("/trade/place", sParams, ref sRespBodyData);

            if (JObj == null)
            {
                Humb_place.Add("Error","Error occurred!");
                Humb_place.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_place.Add("status",            JObj["status"].             ToString());
                    Humb_place.Add("order_id",          JObj["data"]["order_id"].   ToString());
                    Humb_place.Add("cont_id",           JObj["data"]["cont_id"].    ToString());
                    Humb_place.Add("units",             JObj["data"]["units"].      ToString());
                    Humb_place.Add("price",             JObj["data"]["price"].      ToString());
                    Humb_place.Add("total",             JObj["data"]["total"].      ToString());
                    Humb_place.Add("fee",               JObj["data"]["fee"].        ToString());
                }
            }
        }
    }
}
