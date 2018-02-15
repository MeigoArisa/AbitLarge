using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbitLarge.Data
{
    public class bittrexdata
    {
        public static string bittrexAPI_Key = "";
        public static string bittrexSecret_Key = "";
        public static int getmarkets_count = 0;
        public static int getmarkethistory_count = 0;
        public static int getmarketsummaries_count = 0;
        public static int getorderbook_count = 0;
        public static int getorderbook_count2 = 0;
        public static int balances_count = 0;
        public static int getopenrder_count = 0;
        public static int getorderhistory_count = 0;
        private static Hashtable bittrexData_balances = new Hashtable();
        public static Hashtable trex_balances { get => bittrexData_balances; set => bittrexData_balances = value; }

        private static Hashtable bittrexData_cancel = new Hashtable();
        public static Hashtable trex_cancel { get => bittrexData_cancel; set => bittrexData_cancel = value; }

        private static Hashtable bittrexData_market_buy = new Hashtable();
        public static Hashtable trex_buy { get => bittrexData_market_buy; set => bittrexData_market_buy = value; }

        private static Hashtable bittrexData_market_sell = new Hashtable();
        public static Hashtable trex_sell { get => bittrexData_market_sell; set => bittrexData_market_sell = value; }

        private static Hashtable bittrexData_getmarket = new Hashtable();
        public static Hashtable trex_market { get => bittrexData_getmarket; set => bittrexData_getmarket = value; }

        private static Hashtable bittrexData_getmarkethistory = new Hashtable();
        public static Hashtable trex_history { get => bittrexData_getmarkethistory; set => bittrexData_getmarkethistory = value; }

        private static Hashtable bittrexData_getticker = new Hashtable();
        public static Hashtable trex_ticker { get => bittrexData_getticker; set => bittrexData_getticker = value; }

        private static Hashtable bittrexData_orderbook = new Hashtable();
        public static Hashtable trex_orderbook { get => bittrexData_orderbook; set => bittrexData_orderbook = value; }

        public static Dictionary<string, string> trex_getorderhistory = new Dictionary<string, string>();

        public static Dictionary<string, string> trex_getopenrder =  new Dictionary<string, string>();
        public static Dictionary<string,string> trex_summaries = new Dictionary<string, string>(); //하... 해시테이블쓰니까 스택오버플로우... 쉬벌,,,,
 
    }
}
