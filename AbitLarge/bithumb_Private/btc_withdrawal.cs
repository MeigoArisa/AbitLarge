using System;

namespace AbitLarge.bithumb_Private
{
    public class btc_withdrawal : bithumbdata
    {
        /*  빗썸 출금
         *  units   Float	Currency 출금 하고자 하는 수량
         *  - 1회 최소 수량 (BTC: 0.003 | ETH: 0.01 | DASH: 0.01 | LTC: 0.01 | ETC: 0.01 | XRP: 21 | BCH: 0.005 | XMR: 0.1 | ZEC: 0.01 | QTUM: 0.1)
         *  - 1회 최대 수량 : 회원등급수량
         *  address	String	Currency 출금 주소 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM)
         *  destination	Integer	Currency 출금 Destination Tag (XRP 출금시)
         *  String	Currency 출금 Payment Id (XMR 출금시)
         *   currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM (기본값: BTC)
        */
        /// <summary>
        /// bithumb 회원 Currency 출금(회원등급에 따른 BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM 출금)
        /// </summary>
        /// <param name="units">Currency를 출금 하고자 하는 수량</param>
        /// <param name="address">Currency 출금 주소 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM)</param>
        /// <param name="destination">Integer	Currency 출금 Destination Tag (XRP 출금시)</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM (기본값: BTC)</param>
        public void Call_btc_withdrawal(float units, string address, string destination, string currency)
        {
            string sParams = "units=" + units + "&address=" + address + "&destination=" + destination+ "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/trade/btc_withdrawal", sParams, ref sRespBodyData);

            Humb_BTC_with.Clear();

            if (JObj == null)
            {
                Humb_BTC_with.Add("Error",("Error occurred!"));
                Humb_BTC_with.Add("Error2",("HTTP Response JSON Data: {0}" + sRespBodyData));
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
