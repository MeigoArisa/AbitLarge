using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace AbitLarge
{
    public class bithumbdata
    {
        public static string sAPI_Key = "";
        public static string sAPI_Secret = "";
        public static string sRespBodyData = String.Empty;
        public static JObject JObj = null;

        public static int user_count = 0;
        public static int sell_count = 0;
        public static int buy_count = 0;
        public static int ticker_count = 0;
        public static int recent_transactions_count = 0;
        public static bithumbparsing hAPI_Svr { get => new bithumbparsing(sAPI_Key, sAPI_Secret); set => new bithumbparsing(sAPI_Key, sAPI_Secret); }
        private static Hashtable BithumbData_account = new Hashtable();
        public static Hashtable Humb_acc { get => BithumbData_account; set => BithumbData_account = value; }

        private static Hashtable BithumbData_balance = new Hashtable();
        public static Hashtable Humb_balance { get => BithumbData_balance; set => BithumbData_balance = value; }

        private static Hashtable BithumbData_btc_withdrawal = new Hashtable();
        public static Hashtable Humb_BTC_with { get => BithumbData_btc_withdrawal; set => BithumbData_btc_withdrawal = value; }

        private static Hashtable BithumbData_krw_withdrawal = new Hashtable();
        public static Hashtable Humb_KRW_with { get => BithumbData_krw_withdrawal; set => BithumbData_krw_withdrawal = value; }

        private static Hashtable BithumbData_cancel = new Hashtable();
        public static Hashtable Humb_cancel { get => BithumbData_cancel; set => BithumbData_cancel = value; }

        private static Hashtable BithumbData_krw_deposit = new Hashtable();
        public static Hashtable Humb_KRW_deposit { get => BithumbData_krw_deposit; set => BithumbData_krw_deposit = value; }

        private static Hashtable BithumbData_market_buy = new Hashtable();
        public static Hashtable Humb_buy { get => BithumbData_market_buy; set => BithumbData_market_buy = value; }

        private static Hashtable BithumbData_market_sell = new Hashtable();
        public static Hashtable Humb_sell { get => BithumbData_market_sell; set => BithumbData_market_sell = value; }

        private static Hashtable BithumbData_order_detail = new Hashtable();
        public static Hashtable Humb_detail { get => BithumbData_order_detail; set => BithumbData_order_detail = value; }

        private static Hashtable BithumbData_orders = new Hashtable();
        public static Hashtable Humb_orders { get => BithumbData_orders; set => BithumbData_orders = value; }

        private static Hashtable BithumbData_place = new Hashtable();
        public static Hashtable Humb_place { get => BithumbData_place; set => BithumbData_place = value; }

        private static Hashtable BithumbData_ticker = new Hashtable();
        public static Hashtable Humb_ticker { get => BithumbData_ticker; set => BithumbData_ticker = value; }

        private static Hashtable BithumbData_user_transactions = new Hashtable();
        public static Hashtable Humb_user_trans { get => BithumbData_user_transactions; set => BithumbData_user_transactions = value; }

        private static Hashtable BithumbData_wallet_address = new Hashtable();
        public static Hashtable Humb_wallet { get => BithumbData_wallet_address; set => BithumbData_wallet_address = value; }

        private static Hashtable BithumbData_public_ticker = new Hashtable();
        public static Hashtable Humb_Public_ticker { get => BithumbData_public_ticker; set => BithumbData_public_ticker = value; }

        private static Hashtable BithumbData_public_recent_transactions = new Hashtable();
        public static Hashtable Humb_Public_rec_trans { get => BithumbData_public_recent_transactions; set => BithumbData_public_recent_transactions = value; }

        private static Hashtable BithumbData_public_orderbook = new Hashtable();
        public static Hashtable Humb_Public_order { get => BithumbData_public_orderbook; set => BithumbData_public_orderbook = value; }

    }
}
