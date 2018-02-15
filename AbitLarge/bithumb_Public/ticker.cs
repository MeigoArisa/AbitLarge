using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Windows;

namespace AbitLarge.bithumb_Public
{
    public class ticker : bithumbdata
    {
        /// <summary>
        /// bithumb 거래소 마지막 거래 정보
        /// </summary>
        /// <param name="currency">BTC, ETH, DASH, LTC, ETC, XRP, BCH, XMR, ZEC, QTUM, BTG, EOS (기본값: BTC), ALL(전체)</param>
        public static void Call_Public(string currency)
        {
            Humb_Public_ticker.Clear();
            string sParams = "";
            JObj = hAPI_Svr.xcoinApiCall("/public/ticker/" + currency, sParams, ref sRespBodyData);
            if (JObj == null)
            {
                Humb_Public_ticker.Add("Error","Error occurred!");
                Humb_Public_ticker.Add("Error2","HTTP Response JSON Data: {0}" + sRespBodyData);
            }
            else
            {
                if (String.Compare(JObj["status"].ToString(), "0000", true) == 0)
                {
                    Humb_Public_ticker.Add("status",            JObj["status"].                 ToString());
                    Humb_Public_ticker.Add("opening_price",     ((double)JObj["data"]["opening_price"]).  ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("closing_price",     ((double)JObj["data"]["closing_price"]).  ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("min_price",         ((double)JObj["data"]["min_price"]).      ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("max_price",         ((double)JObj["data"]["max_price"]).      ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("average_price",     ((double)JObj["data"]["average_price"]).  ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("units_traded",      ((double)JObj["data"]["units_traded"]).   ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("volume_1day",       ((double)JObj["data"]["volume_1day"]).    ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("volume_7day",       ((double)JObj["data"]["volume_7day"]).    ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("buy_price",         ((double)JObj["data"]["buy_price"]).      ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("sell_price",        ((double)JObj["data"]["sell_price"]).     ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                    Humb_Public_ticker.Add("date",              ((double)JObj["data"]["date"]).           ToString("G", CultureInfo.CreateSpecificCulture("es-ES")));
                }
            }
        }
    }
}
