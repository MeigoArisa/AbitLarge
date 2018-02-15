using System;

namespace AbitLarge.bithumb_Private
{
    public class ticker : bithumbdata
    {
        /*
            opening_price	최근 24시간 내 시작 거래금액
            closing_price	최근 24시간 내 마지막 거래금액
            min_price	최근 24시간 내 최저 거래금액
            max_price	최근 24시간 내 최고 거래금액
            average_price	최근 24시간 내 평균 거래금액
            units_traded	최근 24시간 내 Currency 거래량 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
            volume_1day	최근 1일간 Currency 거래량 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
            volume_7day	최근 7일간 Currency 거래량 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
            date	현재 시간 Timestamp
            order_currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
            payment_currency	String	KRW (현재 bithumb에서 제공하는 통화 KRW)
            */
        /// <summary>
        /// 회원 마지막 거래 정보
        /// </summary>
        /// <param name="order_currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        /// <param name="payment_currency">KRW (현재 bithumb에서 제공하는 통화 KRW)</param>
        public void Call_ticker(string order_currency, string payment_currency)
        {
            Humb_ticker.Clear();

            string sParams = "order_currency=" + order_currency + "&payment_currency=" + payment_currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/ticker", sParams, ref sRespBodyData);

            if (JObj == null)
            {
                Humb_ticker.Add("Error","Error occurred!");
                Humb_ticker.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_ticker.Add("status",           JObj["status"].                 ToString());
                    Humb_ticker.Add("opening_price",    JObj["data"]["opening_price"].  ToString());
                    Humb_ticker.Add("closing_price",    JObj["data"]["closing_price"].  ToString());
                    Humb_ticker.Add("min_price",        JObj["data"]["min_price"].      ToString());
                    Humb_ticker.Add("max_price",        JObj["data"]["max_price"].      ToString());
                    Humb_ticker.Add("average_price",    JObj["data"]["average_price"].  ToString());
                    Humb_ticker.Add("units_traded",     JObj["data"]["units_traded"].   ToString());
                    Humb_ticker.Add("volume_1day",      JObj["data"]["volume_1day"].    ToString());
                    Humb_ticker.Add("volume_7day",      JObj["data"]["volume_7day"].    ToString());
                    Humb_ticker.Add("date",             JObj["data"]["date"].           ToString());
                }
            }
        }
    }
}
