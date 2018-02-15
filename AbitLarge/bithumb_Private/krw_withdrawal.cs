using System;

namespace AbitLarge.bithumb_Private
{
    public class krw_withdrawal : bithumbdata
    {
        /*
        bank
        002 산업은행 
        003 기업은행 
        004 국민은행 
        007 수협중앙회 
        011 농협은행 
        012 지역농축협 
        020 우리은행 
        023 SC제일은행 
        027 한국씨티은행 
        031 대구은행 
        032 부산은행 
        034 광주은행 
        035 제주은행 
        037 전북은행 
        039 경남은행 
        045 새마을금고중앙회 
        048 신협중앙회 
        050 상호저축은행 
        054 HSBC은행 
        055 도이치은행 
        057 제이피모간체이스은행 
        060 BOA은행 
        061 비엔피파리바은행 
        062 중국공상은행 
        064 산림조합중앙회 
        071 우체국 
        081 KEB하나은행 
        088 신한은행 
        089 K뱅크 
        090 카카오뱅크 
        097 전자화폐 중계센터,RK 
        209 유안타증권 
        218 KB증권 
        227 KTB투자증권 
        238 미래에셋대우 
        240 삼성증권 
        243 한국투자증권 
        247 NH투자증권 
        261 교보증권 
        262 하이투자증권 
        263 현대차투자증권 
        264 키움증권 
        265 이베스트투자증권 
        266 SK증권 
        267 대신증권 
        269 한화투자증권 
        270 하나금융투자 
        278 신한금융투자 
        279 동부증권 
        280 유진투자증권 
        287 메리츠종합금융증권 
        290 부국증권 
        291 신영증권 
        292 케이프투자증권 
        294 펀드온라인코리아 
        account String  출금계좌번호
        price   Int 출금 금액
        */
        /// <summary>
        /// bithumb 회원 KRW 출금 신청
        /// </summary>
        /// <param name="bank">은행 코드</param>
        /// <param name="account">출금계좌번호</param>
        /// <param name="price">출금 금액</param>
        public void Call_krw_withdrawal(string bank, string account, double price)
        {
            Humb_KRW_with.Clear();
            string sParams = "bank=" + bank + "&account=" + account + "&price=" + price;
            JObj = hAPI_Svr.xcoinApiCall("/trade/krw_withdrawal", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_KRW_with.Add("Error","Error occurred!");
                Humb_KRW_with.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_KRW_with.Add("status", JObj["status"].ToString());
                }
            }
        }
    }
}
