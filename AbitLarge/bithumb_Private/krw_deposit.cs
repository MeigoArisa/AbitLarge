using System;

namespace AbitLarge.bithumb_Private
{
    public class krw_deposit : bithumbdata
    {
        //KRW 입금 가상계좌 정보 요청
        /*
         *  status	결과 상태 코드 (정상 : 0000, 정상이외 코드는 에러 코드 참조)
         *  account	가상계좌번호
         *  bank	신한은행(은행명)
         *  BankUser	비티씨코리아닷컴(입금자명)
        */
        /// <summary>
        /// bithumb 회원 KRW 입금 가상계좌 정보 요청
        /// </summary>
        public static void Call_krw_deposit()
        {
            Humb_KRW_deposit.Clear();
            string sParams = "";
            JObj = hAPI_Svr.xcoinApiCall("/trade/krw_deposit", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_KRW_deposit.Add("Error","Error occurred!");
                Humb_KRW_deposit.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_KRW_deposit.Add("status",      JObj["status"].             ToString());
                    Humb_KRW_deposit.Add("account",     JObj["data"]["created"].    ToString());
                    Humb_KRW_deposit.Add("bank",        JObj["data"]["account_id"]. ToString());
                    Humb_KRW_deposit.Add("BankUser",    JObj["data"]["trade_fee"].  ToString());
                }
            }
        }
    }
}
