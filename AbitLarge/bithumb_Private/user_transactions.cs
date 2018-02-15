using System;
using System.Windows;

namespace AbitLarge.bithumb_Private
{
    public class user_transactions : bithumbdata
    {
        /*
         *
         *  search	검색 구분 (0 : 전체, 1 : 구매완료, 2 : 판매완료, 3 : 출금중, 4 : 입금, 5 : 출금, 9 : KRW입금중)
         *  transfer_date	거래 일시 Timestamp
         *  units	거래 Currency 수량 (BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS)
         *  price	거래금액
         *  {currency}1krw	1Currency당 거래금액 (btc, eth, dash, ltc, etc, xrp, bch, xmr, zec, qtum, btg, eos)
         *  fee	거래수수료
         *  {currency}_remain	거래 후 Currency 잔액 (btc, eth, dash, ltc, etc, xrp, bch, xmr, zec, qtum, btg, eos)
         *  krw_remain	거래 후 KRW 잔액
         *  offset	Int	Value : 0 ~ (default : 0)
         *  count	Int	Value : 1 ~ 50 (default : 20)
         *  searchGb	String	0 : 전체, 1 : 구매완료, 2 : 판매완료, 3 : 출금중, 4 : 입금, 5 : 출금, 9 : KRW입금중
         *  currency	String	BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)
         */
        /// <summary>
        /// 회원 거래 내역
        /// </summary>
        /// <param name="offset">Value : 0 ~ (default : 0)</param>
        /// <param name="count">Value : 1 ~ 50 (default : 20)</param>
        /// <param name="searchGb">0 : 전체, 1 : 구매완료, 2 : 판매완료, 3 : 출금중, 4 : 입금, 5 : 출금, 9 : KRW입금중</param>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC)</param>
        public static void Call_user_transactions(int offset, int count, string searchGb, string currency)
        {
            Humb_user_trans.Clear();
            string sParams = "offset=" + offset + "&count=" + count + "&searchGb="+ searchGb + "&currency=" + currency;
            JObj = hAPI_Svr.xcoinApiCall("/info/user_transactions", sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_user_trans.Add("Error","Error occurred!");
                Humb_user_trans.Add("Error2", "HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    try
                    {
                        var tmp = JObj["data"].ToString().Replace("}", "");
                        tmp = tmp.Replace("{", "");
                        tmp = tmp.Replace(",", "");
                        tmp = tmp.Replace("\r\n\r\n", "");
                        tmp = tmp.Replace("\"", "");
                        tmp = tmp.Replace("[", "");
                        tmp = tmp.Replace("]", "");
                        tmp = tmp.Replace(" ", "");
                        tmp = tmp.Replace(":", "");
                        tmp = tmp.Replace("search", "");
                        tmp = tmp.Replace("units", "");
                        tmp = tmp.Replace("transfer_date", "");
                        tmp = tmp.Replace("price", "");
                        tmp = tmp.Replace("fee", "");
                        string[] separator = new string[1] { "\r\n" };
                        string[] strResult = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        user_count = 0;
                        for (int i = 0; i < (strResult.Length / 8); i++)
                        {
                            Humb_user_trans.Add("search" + i, strResult[(user_count * 8)]);
                            Humb_user_trans.Add("transfer_date" + i, strResult[(user_count * 8) + 1]);
                            Humb_user_trans.Add("units" + i, strResult[(user_count * 8) + 2]);
                            Humb_user_trans.Add("price" + i, strResult[(user_count * 8) + 3]);
                            Humb_user_trans.Add(currency + "1krw" + i, strResult[(user_count * 8) + 4]);
                            Humb_user_trans.Add("fee" + i, strResult[(user_count * 8) + 5]);
                            Humb_user_trans.Add(currency + "_remain" + i, strResult[(user_count * 8) + 6]);
                            Humb_user_trans.Add("krw_remain" + i, strResult[(user_count * 8) + 7]);
                            user_count += 1;
                        }
                        Humb_user_trans.Add("status", JObj["status"].ToString());
                    }catch(Exception e)
                    {

                    }
                }
            }
        }
    }
}
