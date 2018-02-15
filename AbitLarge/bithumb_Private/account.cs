using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbitLarge.bithumb_Private
{
    public class account : bithumbdata
    {
        /// <summary>
        /// 거래소 회원 정보
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_account(string currency)
        {
            Humb_acc.Clear();

            string sParams = "currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/account", sParams, ref sRespBodyData);

            if (JObj == null)
            {
                Humb_acc.Add("Error",("Error occurred!"));
                Humb_acc.Add("Error2",("HTTP Response JSON Data: {0}" + "    " + sRespBodyData));
                //에러코드
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_acc.Add("status",              JObj["status"]                  .ToString());
                    Humb_acc.Add("Created",             JObj["data"]    ["created"]     .ToString());
                    Humb_acc.Add("Account",             JObj["data"]    ["account_id"]  .ToString());
                    Humb_acc.Add("Trade Fee",           JObj["data"]    ["trade_fee"]   .ToString());
                    Humb_acc.Add("balance",                JObj["data"]    ["balance"]     .ToString());
                    /*
                    *   status 결과 상태 코드(정상 : 0000, 정상이외 코드는 에러 코드 참조)
                    *   created 회원가입 일시 Timestamp
                    *   account_id 회원ID
                    *   trade_fee 거래 수수료
                    *   balance 1Currency 잔액(BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
                    */
                }
            }
        }
    }
}
