using System;

namespace AbitLarge.bithumb_Private
{
    public class balance : bithumbdata
    {
        /// <summary>
        /// 거래소 회원 지갑 정보
        /// </summary>
        /// <param name="cointype">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC), ALL(전체)</param>
        public void Call_balance(string cointype)
        {
            Humb_balance.Clear();
            string sParams = "currency="+ cointype;
            JObj = hAPI_Svr.xcoinApiCall("/info/balance", sParams, ref sRespBodyData);

            if (JObj == null)
            {
                Humb_balance.Add("Error","Error occurred!");
                Humb_balance.Add("Error2",("HTTP Response JSON Data: {0}" + "    " +sRespBodyData));
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_balance.Add("status",                  JObj["status"].                         ToString());
                    Humb_balance.Add("total_"+ cointype,        JObj["data"]["total_" + cointype].      ToString());
                    Humb_balance.Add("total_krw",               JObj["data"]["total_krw"].              ToString());
                    Humb_balance.Add("in_use_" + cointype,      JObj["data"]["in_use_" + cointype].     ToString());
                    Humb_balance.Add("in_use_krw",              JObj["data"]["in_use_krw"].             ToString());
                    Humb_balance.Add("available_" + cointype,   JObj["data"]["available_" + cointype].  ToString());
                    Humb_balance.Add("available_krw",           JObj["data"]["available_krw"].          ToString());
                    Humb_balance.Add("xcoin_last",              JObj["data"]["xcoin_last"].             ToString());

                        /*
                        *   status	결과 상태 코드 (정상 : 0000, 정상이외 코드는 에러 코드 참조)
                        *   total_{currency}	전체 Currency (btc, eth, dash, ltc, etc, xrp, bch, xmr, zec, qtum, btg, eos)
                        *   total_krw	전체 KRW
                        *   in_use_{currency}	사용중 Currency (btc, eth, dash, ltc, etc, xrp, bch, xmr, zec, qtum, btg, eos)
                        *   in_use_krw	사용중 KRW
                        *   available_{currency}	사용 가능 Currency (btc, eth, dash, ltc, etc, xrp, bch, xmr, zec, qtum, btg, eos)
                        *   available_krw	사용 가능 KRW
                        *   xcoin_last	bithumb 마지막 거래체결 금액
                        */
                }
            }
        }
    }
}
