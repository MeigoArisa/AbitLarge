using System;
using System.Windows;

namespace AbitLarge.bithumb_Private
{
    public class Wallet_address : bithumbdata
    {
        /*
         *  wallet_address	전자지갑 Address
         *  currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         */
        /// <summary>
        /// bithumb 거래소 회원 입금 주소
        /// </summary>
        /// <param name="currency">String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_wallet_address(string currency)
        {
            Humb_wallet.Clear();
            string sParams = "currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/wallet_address", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_wallet.Add("Error","Error occurred!");
                Humb_wallet.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_wallet.Add("status",           JObj["status"].                 ToString());
                    Humb_wallet.Add("wallet_address",   JObj["data"]["wallet_address"]. ToString());
                    Humb_wallet.Add("currency",         JObj["data"]["currency"].       ToString());
                }
            }
        }
    }
}
