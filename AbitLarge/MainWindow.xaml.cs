using System;
using System.Windows;
using CoinMarketCap.Api;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarketCap.Models;
using System.Windows.Controls;
using System.Windows.Data;
using AbitLarge.bithumb_Private;
using AbitLarge.ListViewDataClass;
using AbitLarge.bithumb_Public;
using System.Windows.Threading;
using AbitLarge.Data;
using AbitLarge.bittrex_public;
using AbitLarge.bittrex_Private;
using System.Text.RegularExpressions;

namespace AbitLarge
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<TickerModel> CoinMarket_Ticker_tmp;
        public static GlobalDataModel CoinMarket_Global_tmp;
        public static bithumborderbook bithumborderbook_tmp;
        public static bithumbrecent_transactions bithumbrecent_transactions_tmp;
        public static bithumbTicker bithumbTicker_tmp;
        public static CoinMarketItems CoinMarketItems_tmp;
        string coinmarket_currency = null;
        DispatcherTimer btimer = new DispatcherTimer();

        /// <summary>
        /// 메인 로드
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                btimer.Interval = TimeSpan.FromSeconds(4);
                btimer.Tick += new EventHandler(timer_Tick);

                #region bithumb_tradelist grid
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn search = new GridViewColumn();
                search.DisplayMemberBinding = new Binding("search");
                search.Header = "search";
                search.Width = 100;
                grdView.Columns.Add(search);

                GridViewColumn transfer_date = new GridViewColumn();
                transfer_date.DisplayMemberBinding = new Binding("transfer_date");
                transfer_date.Header = "transfer_date";
                transfer_date.Width = 100;
                grdView.Columns.Add(transfer_date);

                GridViewColumn units = new GridViewColumn();
                units.DisplayMemberBinding = new Binding("units");
                units.Header = "units";
                units.Width = 100;
                grdView.Columns.Add(units);

                GridViewColumn price = new GridViewColumn();
                price.DisplayMemberBinding = new Binding("price");
                price.Header = "type ";
                price.Width = 100;
                grdView.Columns.Add(price);

                GridViewColumn fee = new GridViewColumn();
                fee.DisplayMemberBinding = new Binding("fee ");
                fee.Header = "total";
                fee.Width = 100;
                grdView.Columns.Add(fee);

                bithumb_tradelist.View = grdView;
                #endregion
                #region bittrex_tradelist grid
                GridView grdView2 = new GridView();
                grdView2.AllowsColumnReorder = true;

                GridViewColumn OrderUuid = new GridViewColumn();
                OrderUuid.DisplayMemberBinding = new Binding("OrderUuid");
                OrderUuid.Header = "OrderUuid";
                OrderUuid.Width = 100;
                grdView2.Columns.Add(OrderUuid);

                GridViewColumn Exchange = new GridViewColumn();
                Exchange.DisplayMemberBinding = new Binding("Exchange");
                Exchange.Header = "Exchange";
                Exchange.Width = 100;
                grdView2.Columns.Add(Exchange);

                GridViewColumn TimeStamp = new GridViewColumn();
                TimeStamp.DisplayMemberBinding = new Binding("TimeStamp");
                TimeStamp.Header = "TimeStamp";
                TimeStamp.Width = 100;
                grdView2.Columns.Add(TimeStamp);

                GridViewColumn OrderType = new GridViewColumn();
                OrderType.DisplayMemberBinding = new Binding("OrderType");
                OrderType.Header = "OrderType";
                OrderType.Width = 100;
                grdView2.Columns.Add(OrderType);

                GridViewColumn Limit = new GridViewColumn();
                Limit.DisplayMemberBinding = new Binding("Limit");
                Limit.Header = "Limit";
                Limit.Width = 100;
                grdView2.Columns.Add(Limit);

                GridViewColumn Quantity = new GridViewColumn();
                Quantity.DisplayMemberBinding = new Binding("Quantity");
                Quantity.Header = "Quantity";
                Quantity.Width = 100;
                grdView2.Columns.Add(Quantity);

                GridViewColumn QuantityRemaining = new GridViewColumn();
                QuantityRemaining.DisplayMemberBinding = new Binding("QuantityRemaining");
                QuantityRemaining.Header = "QuantityRemaining";
                QuantityRemaining.Width = 100;
                grdView2.Columns.Add(QuantityRemaining);

                GridViewColumn Commission = new GridViewColumn();
                Commission.DisplayMemberBinding = new Binding("Commission");
                Commission.Header = "Commission";
                Commission.Width = 100;
                grdView2.Columns.Add(Commission);

                GridViewColumn Price = new GridViewColumn();
                Price.DisplayMemberBinding = new Binding("Price");
                Price.Header = "Price";
                Price.Width = 100;
                grdView2.Columns.Add(Price);

                bittrex_tradelist.View = grdView2;
                #endregion
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 5초 타이머, 리스트 새로고침
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                #region 빗썸 판매알림 item
                user_transactions.Call_user_transactions(0, 50, "0", (string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
                for (int i = 0; i < bithumbdata.user_count; i++)
                {
                    if ((string)bithumbdata.Humb_user_trans["search" + i] == "1" || (string)bithumbdata.Humb_user_trans["search" + i] == "2")
                    {
                        //MessageBox.Show((string)bithumbdata.Humb_user_trans["transfer_date" + i] + " 건 거래완료");
                    }
                }
                if (bithumb_SellCheck.IsChecked == true)
                {
                    List<bithumbuser_trans> items = new List<bithumbuser_trans>();
                    for (int i = 0; i < bithumbdata.user_count; i++)
                        items.Add(new bithumbuser_trans()
                        {
                            search = (string)bithumbdata.Humb_user_trans["search" + i],
                            transfer_date = (string)bithumbdata.Humb_user_trans["transfer_date" + i],
                            units = (string)bithumbdata.Humb_user_trans["units" + i],
                            price = (string)bithumbdata.Humb_user_trans["price" + i],
                            fee = (string)bithumbdata.Humb_user_trans["fee" + i],
                        });
                    bithumb_tradelist.ItemsSource = items;
                }
                #endregion
                #region 비트렉스 판매알림 item
                if (bittrex_sellcheck.IsChecked == true)
                {
                        List<bittrexorderhistory> items = new List<bittrexorderhistory>();
                    for (int i = 0; i < bittrexdata.getorderhistory_count; i++)
                        items.Add(new bittrexorderhistory()
                        {
                            OrderUuid = bittrexdata.trex_getorderhistory["OrderUuid" + i],
                            Exchange = bittrexdata.trex_getorderhistory["Exchange" + i],
                            TimeStamp = bittrexdata.trex_getorderhistory["TimeStamp" + i],
                            OrderType = bittrexdata.trex_getorderhistory["OrderType" + i],
                            Limit = bittrexdata.trex_getorderhistory["Limit" + i],
                            Quantity = bittrexdata.trex_getorderhistory["Quantity" + i],
                            QuantityRemaining = bittrexdata.trex_getorderhistory["QuantityRemaining" + i],
                            Commission = bittrexdata.trex_getorderhistory["Commission" + i],
                            Price = bittrexdata.trex_getorderhistory["Price" + i],
                        });
                    bithumb_tradelist.ItemsSource = items;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 비동기 호출
        /// </summary>
        /// <returns></returns>
        private async Task CoinMarket_GetTickersAsync()
        {
            await PublicApi.GetTickersAsync(coinmarket_currency, 100);
            IList<TickerModel> x = await PublicApi.GetTickersAsync(coinmarket_currency, 100);
            CoinMarket_Ticker_tmp = (List<TickerModel>)x;
        }

        /// <summary>
        /// 코인마켓 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoinMarket_Tick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid setting
                                GridView grdView = new GridView();
                                grdView.AllowsColumnReorder = true;

                                GridViewColumn id = new GridViewColumn();
                                id.DisplayMemberBinding = new Binding("id");
                                id.Header = "ID";
                                id.Width = 60;
                                grdView.Columns.Add(id);

                                GridViewColumn name = new GridViewColumn();
                                name.DisplayMemberBinding = new Binding("name");
                                name.Header = "Name";
                                name.Width = 60;
                                grdView.Columns.Add(name);

                                GridViewColumn symbol = new GridViewColumn();
                                symbol.DisplayMemberBinding = new Binding("symbol");
                                symbol.Header = "Sympol";
                                symbol.Width = 50;
                                grdView.Columns.Add(symbol);

                                GridViewColumn rank = new GridViewColumn();
                                rank.DisplayMemberBinding = new Binding("rank");
                                rank.Header = "rank";
                                rank.Width = 50;
                                grdView.Columns.Add(rank);

                                GridViewColumn price_usd = new GridViewColumn();
                                price_usd.DisplayMemberBinding = new Binding("price_usd");
                                price_usd.Header = "price";
                                price_usd.Width = 100;
                                grdView.Columns.Add(price_usd);

                                GridViewColumn price_btc = new GridViewColumn();
                                price_btc.DisplayMemberBinding = new Binding("price_btc");
                                price_btc.Header = "price btc";
                                price_btc.Width = 100;
                                grdView.Columns.Add(price_btc);

                                GridViewColumn volume_24h = new GridViewColumn();
                                volume_24h.DisplayMemberBinding = new Binding("volume_24h");
                                volume_24h.Header = "volume 24h";
                                volume_24h.Width = 100;
                                grdView.Columns.Add(volume_24h);

                                GridViewColumn market_cap = new GridViewColumn();
                                market_cap.DisplayMemberBinding = new Binding("market_cap");
                                market_cap.Header = "market cap";
                                market_cap.Width = 100;
                                grdView.Columns.Add(market_cap);

                                GridViewColumn available_supply = new GridViewColumn();
                                available_supply.DisplayMemberBinding = new Binding("available_supply");
                                available_supply.Header = "available supply";
                                available_supply.Width = 100;
                                grdView.Columns.Add(available_supply);

                                GridViewColumn total_supply = new GridViewColumn();
                                total_supply.DisplayMemberBinding = new Binding("total_supply");
                                total_supply.Header = "total supply";
                                total_supply.Width = 100;
                                grdView.Columns.Add(total_supply);

                                GridViewColumn percent_change_1h = new GridViewColumn();
                                percent_change_1h.DisplayMemberBinding = new Binding("percent_change_1h");
                                percent_change_1h.Header = "1h";
                                percent_change_1h.Width = 50;
                                grdView.Columns.Add(percent_change_1h);

                                GridViewColumn percent_change_24h = new GridViewColumn();
                                percent_change_24h.DisplayMemberBinding = new Binding("percent_change_24h");
                                percent_change_24h.Header = "24h";
                                percent_change_24h.Width = 50;
                                grdView.Columns.Add(percent_change_24h);

                                GridViewColumn percent_change_7d = new GridViewColumn();
                                percent_change_7d.DisplayMemberBinding = new Binding("percent_change_7d");
                                percent_change_7d.Header = "7d";
                                percent_change_7d.Width = 50;
                                grdView.Columns.Add(percent_change_7d);

                                GridViewColumn last_updated = new GridViewColumn();
                                last_updated.DisplayMemberBinding = new Binding("last_updated");
                                last_updated.Header = "last updated";
                                last_updated.Width = 80;
                                grdView.Columns.Add(last_updated);
                MainListView.View = grdView;
                #endregion

                Task.Run(async () => { await CoinMarket_GetTickersAsync(); }).Wait();

                List<CoinMarketItems> items = new List<CoinMarketItems>();
                switch (coinmarket_currency)
                {
                    case null:
                        break;
                    #region EUR
                    case "EUR":
                        for (int i = 0; i < CoinMarket_Ticker_tmp.Count; i++)
                            items.Add(new CoinMarketItems()
                            {
                                id = CoinMarket_Ticker_tmp[i].Id,
                                name = CoinMarket_Ticker_tmp[i].Name,
                                symbol = CoinMarket_Ticker_tmp[i].Symbol,
                                rank = CoinMarket_Ticker_tmp[i].Rank.ToString(),
                                price_usd = CoinMarket_Ticker_tmp[i].PriceEUR.ToString(),
                                price_btc = CoinMarket_Ticker_tmp[i].PriceBTC.ToString(),
                                volume_24h = CoinMarket_Ticker_tmp[i].VolumeUSD24Hrs.ToString(),
                                market_cap = CoinMarket_Ticker_tmp[i].MarketCapEUR.ToString(),
                                available_supply = CoinMarket_Ticker_tmp[i].AvailableSupply.ToString(),
                                total_supply = CoinMarket_Ticker_tmp[i].TotalSupply.ToString(),
                                percent_change_1h = CoinMarket_Ticker_tmp[i].PercentChange1Hr.ToString(),
                                percent_change_24h = CoinMarket_Ticker_tmp[i].PercentChange24Hr.ToString(),
                                percent_change_7d = CoinMarket_Ticker_tmp[i].PercentChange7Days.ToString(),
                                last_updated = CoinMarket_Ticker_tmp[i].LastUpdated.ToString()
                            });
                        break;
                    #endregion
                    #region KRW
                    case "KRW":
                        for (int i = 0; i < CoinMarket_Ticker_tmp.Count; i++)
                            items.Add(new CoinMarketItems()
                            {
                                id = CoinMarket_Ticker_tmp[i].Id,
                                name = CoinMarket_Ticker_tmp[i].Name,
                                symbol = CoinMarket_Ticker_tmp[i].Symbol,
                                rank = CoinMarket_Ticker_tmp[i].Rank.ToString(),
                                price_usd = CoinMarket_Ticker_tmp[i].PriceKRW.ToString(),
                                price_btc = CoinMarket_Ticker_tmp[i].PriceBTC.ToString(),
                                volume_24h = CoinMarket_Ticker_tmp[i].VolumeKRW24Hr.ToString(),
                                market_cap = CoinMarket_Ticker_tmp[i].MarketCapKRW.ToString(),
                                available_supply = CoinMarket_Ticker_tmp[i].AvailableSupply.ToString(),
                                total_supply = CoinMarket_Ticker_tmp[i].TotalSupply.ToString(),
                                percent_change_1h = CoinMarket_Ticker_tmp[i].PercentChange1Hr.ToString(),
                                percent_change_24h = CoinMarket_Ticker_tmp[i].PercentChange24Hr.ToString(),
                                percent_change_7d = CoinMarket_Ticker_tmp[i].PercentChange7Days.ToString(),
                                last_updated = CoinMarket_Ticker_tmp[i].LastUpdated.ToString()
                            });
                        break;
                    #endregion
                    #region GBP
                    case "GBP":
                        for (int i = 0; i < CoinMarket_Ticker_tmp.Count; i++)
                            items.Add(new CoinMarketItems()
                            {
                                id = CoinMarket_Ticker_tmp[i].Id,
                                name = CoinMarket_Ticker_tmp[i].Name,
                                symbol = CoinMarket_Ticker_tmp[i].Symbol,
                                rank = CoinMarket_Ticker_tmp[i].Rank.ToString(),
                                price_usd = CoinMarket_Ticker_tmp[i].PriceGBP.ToString(),
                                price_btc = CoinMarket_Ticker_tmp[i].PriceBTC.ToString(),
                                volume_24h = CoinMarket_Ticker_tmp[i].VolumeGBP24Hr.ToString(),
                                market_cap = CoinMarket_Ticker_tmp[i].MarketCapGBP.ToString(),
                                available_supply = CoinMarket_Ticker_tmp[i].AvailableSupply.ToString(),
                                total_supply = CoinMarket_Ticker_tmp[i].TotalSupply.ToString(),
                                percent_change_1h = CoinMarket_Ticker_tmp[i].PercentChange1Hr.ToString(),
                                percent_change_24h = CoinMarket_Ticker_tmp[i].PercentChange24Hr.ToString(),
                                percent_change_7d = CoinMarket_Ticker_tmp[i].PercentChange7Days.ToString(),
                                last_updated = CoinMarket_Ticker_tmp[i].LastUpdated.ToString()
                            });
                        break;
                    #endregion
                    #region USD
                    case "USD":
                        for (int i = 0; i < CoinMarket_Ticker_tmp.Count; i++)
                            items.Add(new CoinMarketItems()
                            {
                                id = CoinMarket_Ticker_tmp[i].Id,
                                name = CoinMarket_Ticker_tmp[i].Name,
                                symbol = CoinMarket_Ticker_tmp[i].Symbol,
                                rank = CoinMarket_Ticker_tmp[i].Rank.ToString(),
                                price_usd = CoinMarket_Ticker_tmp[i].PriceUSD.ToString(),
                                price_btc = CoinMarket_Ticker_tmp[i].PriceBTC.ToString(),
                                volume_24h = CoinMarket_Ticker_tmp[i].VolumeUSD24Hrs.ToString(),
                                market_cap = CoinMarket_Ticker_tmp[i].MarketCapUSD.ToString(),
                                available_supply = CoinMarket_Ticker_tmp[i].AvailableSupply.ToString(),
                                total_supply = CoinMarket_Ticker_tmp[i].TotalSupply.ToString(),
                                percent_change_1h = CoinMarket_Ticker_tmp[i].PercentChange1Hr.ToString(),
                                percent_change_24h = CoinMarket_Ticker_tmp[i].PercentChange24Hr.ToString(),
                                percent_change_7d = CoinMarket_Ticker_tmp[i].PercentChange7Days.ToString(),
                                last_updated = CoinMarket_Ticker_tmp[i].LastUpdated.ToString()
                            });
                        break;
                        #endregion
                }
                MainListView.ItemsSource = items;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 코인마켓 글로벌데이터 비동기호출
        /// </summary>
        /// <returns></returns>
        private async Task CoinMarket_GetGlobalDataAsync()
        {
            await PublicApi.GetGlobalDataAsync(coinmarket_currency);
            GlobalDataModel x = await PublicApi.GetGlobalDataAsync(coinmarket_currency);
            CoinMarket_Global_tmp = x;
        }


        private void callbutton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                                GridView grdView = new GridView();
                                grdView.AllowsColumnReorder = true;

                                GridViewColumn total_market_cap = new GridViewColumn();
                                total_market_cap.DisplayMemberBinding = new Binding("total_market_cap");
                                total_market_cap.Header = "total market cap";
                                total_market_cap.Width = 130;
                                grdView.Columns.Add(total_market_cap);

                                GridViewColumn total_24h_volume = new GridViewColumn();
                                total_24h_volume.DisplayMemberBinding = new Binding("total_24h_volume");
                                total_24h_volume.Header = "total 24h volume";
                                total_24h_volume.Width = 130;
                                grdView.Columns.Add(total_24h_volume);

                                GridViewColumn bitcoin_percentage_of_market_cap = new GridViewColumn();
                                bitcoin_percentage_of_market_cap.DisplayMemberBinding = new Binding("bitcoin_percentage_of_market_cap");
                                bitcoin_percentage_of_market_cap.Header = "bitcoin percentage of market cap";
                                bitcoin_percentage_of_market_cap.Width = 130;
                                grdView.Columns.Add(bitcoin_percentage_of_market_cap);

                                GridViewColumn active_currencies = new GridViewColumn();
                                active_currencies.DisplayMemberBinding = new Binding("active_currencies");
                                active_currencies.Header = "active currencies";
                                active_currencies.Width = 110;
                                grdView.Columns.Add(active_currencies);

                                GridViewColumn active_assets = new GridViewColumn();
                                active_assets.DisplayMemberBinding = new Binding("active_assets");
                                active_assets.Header = "active assets";
                                active_assets.Width = 90;
                                grdView.Columns.Add(active_assets);

                                GridViewColumn active_markets = new GridViewColumn();
                                active_markets.DisplayMemberBinding = new Binding("active_markets");
                                active_markets.Header = "active markets";
                                active_markets.Width = 90;
                                grdView.Columns.Add(active_markets);

                                MainListView.View = grdView;
                #endregion
                Task.Run(async () => { await CoinMarket_GetGlobalDataAsync(); }).Wait();
                List<CoinMarketGlobalItems> items = new List<CoinMarketGlobalItems>();
                switch (coinmarket_currency)
                {
                    case null:
                        break;
                    #region EUR
                    case "EUR":
                        items.Add(new CoinMarketGlobalItems()
                        {
                            total_market_cap = CoinMarket_Global_tmp.TotalMarketCapEUR.ToString(),
                            total_24h_volume = CoinMarket_Global_tmp.Total24HrVolumeEUR.ToString(),
                            bitcoin_percentage_of_market_cap = CoinMarket_Global_tmp.BitcoinPercentageOfMarketCap.ToString(),
                            active_currencies = CoinMarket_Global_tmp.ActiveCurrencies.ToString(),
                            active_assets = CoinMarket_Global_tmp.ActiveAssets.ToString(),
                            active_markets = CoinMarket_Global_tmp.ActiveMarkets.ToString(),
                        });
                        break;
                    #endregion
                    #region KRW
                    case "KRW":
                        items.Add(new CoinMarketGlobalItems()
                        {
                            total_market_cap = CoinMarket_Global_tmp.TotalMarketCapKRW.ToString(),
                            total_24h_volume = CoinMarket_Global_tmp.Total24HrVolumeKRW.ToString(),
                            bitcoin_percentage_of_market_cap = CoinMarket_Global_tmp.BitcoinPercentageOfMarketCap.ToString(),
                            active_currencies = CoinMarket_Global_tmp.ActiveCurrencies.ToString(),
                            active_assets = CoinMarket_Global_tmp.ActiveAssets.ToString(),
                            active_markets = CoinMarket_Global_tmp.ActiveMarkets.ToString(),
                        });
                        break;

                    #endregion
                    #region GBP
                    case "GBP":

                        items.Add(new CoinMarketGlobalItems()
                        {
                            total_market_cap = CoinMarket_Global_tmp.TotalMarketCapGBP.ToString(),
                            total_24h_volume = CoinMarket_Global_tmp.Total24HrVolumeGBP.ToString(),
                            bitcoin_percentage_of_market_cap = CoinMarket_Global_tmp.BitcoinPercentageOfMarketCap.ToString(),
                            active_currencies = CoinMarket_Global_tmp.ActiveCurrencies.ToString(),
                            active_assets = CoinMarket_Global_tmp.ActiveAssets.ToString(),
                            active_markets = CoinMarket_Global_tmp.ActiveMarkets.ToString(),
                        });
                        break;
                    #endregion
                    #region USD
                    case "USD":
                        items.Add(new CoinMarketGlobalItems()
                        {
                            total_market_cap = CoinMarket_Global_tmp.TotalMarketCapUSD.ToString(),
                            total_24h_volume = CoinMarket_Global_tmp.Total24HrVolumeUSD.ToString(),
                            bitcoin_percentage_of_market_cap = CoinMarket_Global_tmp.BitcoinPercentageOfMarketCap.ToString(),
                            active_currencies = CoinMarket_Global_tmp.ActiveCurrencies.ToString(),
                            active_assets = CoinMarket_Global_tmp.ActiveAssets.ToString(),
                            active_markets = CoinMarket_Global_tmp.ActiveMarkets.ToString(),
                        });
                        break;
                        #endregion

                }
                MainListView.ItemsSource = items;
            }
            catch (Exception ex)
            {

            }
        }

        private void currency_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            coinmarket_currency = (string)((ComboBoxItem)currency_comboBox.SelectedItem).Content;
        }

        /// <summary>
        /// 지갑주소조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bithumbcheck_Click(object sender, RoutedEventArgs e)
        {
            bithumbdata.sAPI_Key = BithumbAPI.Text;
            bithumbdata.sAPI_Secret = BithumbSecret.Text;

            account.Call_account((string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
            bithumb_balance_coin.Content = (string)bithumbdata.Humb_acc["balance"];

            #region BTC
                        try
                        {
                            Wallet_address.Call_wallet_address("BTC");
                            bithumbwallet.Content = "BTC : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
            
            catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region ETH
                        try
                        {
                            Wallet_address.Call_wallet_address("ETH");
                            bithumbwallet.Content += "\r\nETH : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region DASH
                        try
                        {
                            Wallet_address.Call_wallet_address("DASH");
                            bithumbwallet.Content += "\r\nDASH : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region LTC
                            try
                            {
                                Wallet_address.Call_wallet_address("LTC");
                                bithumbwallet.Content += "\r\nLTC : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                            }
                            catch (Exception) { }
                #endregion
            Task.Delay(200);

            #region ETC
                        try
                        {
                            Wallet_address.Call_wallet_address("ETC");
                            bithumbwallet.Content += "\r\nETC : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region XRP
                        try
                        {
                            Wallet_address.Call_wallet_address("XRP");
                            bithumbwallet.Content += "\r\nXRP : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
                        #endregion
            Task.Delay(200);

            #region XMR
            try
            {
                Wallet_address.Call_wallet_address("XMR");
                bithumbwallet.Content += "\r\nXMR : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
            }
            catch (Exception) { }
#endregion
            Task.Delay(200);

            #region ZEC
                        try
                        {
                            Wallet_address.Call_wallet_address("ZEC");
                            bithumbwallet.Content += "\r\nZEC : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region QTUM
                        try
                        {
                            Wallet_address.Call_wallet_address("QTUM");
                            bithumbwallet.Content += "\r\nQTUM : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region BTG
                        try
                        {
                            Wallet_address.Call_wallet_address("BTG");
                            bithumbwallet.Content += "\r\nBTG : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion
            Task.Delay(200);

            #region EOS
                        try
                        {
                            Wallet_address.Call_wallet_address("EOS");
                            bithumbwallet.Content += "\r\nEOS : " + bithumbdata.Humb_wallet["wallet_address"].ToString();
                        }
                        catch (Exception) { }
            #endregion

        }

        private void bittrexcheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bittrexdata.bittrexAPI_Key = BittrexAPI.Text;
                bittrexdata.bittrexSecret_Key = bittrex_secret_key.Text;
                bittrex_balances.Call_bittrex_balances();
                bittrexwallet.Content = "";
                for (int i = 0; i < bittrexdata.balances_count; i++)
                    bittrexwallet.Content += bittrexdata.trex_balances["Currency" + i] + ": " + bittrexdata.trex_balances["CryptoAddress" + i] + "\r\n";
            }
            catch(Exception ex)
            {

            }
        }

        private void Bithumb_buy_request_Click(object sender, RoutedEventArgs e)
        {
            bithumbdata.sAPI_Key = BithumbAPI.Text;
            bithumbdata.sAPI_Secret = BithumbSecret.Text;
            timer_Tick(sender, e);
            switch (bithumb_buy.IsChecked)
            {
                case true:
                    market_buy.Call_market_buy(Convert.ToSingle(bithumb_price.Text), (string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
                    break;
                case false:
                    market_sell.Call_market_sell(Convert.ToSingle(bithumb_price.Text), (string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
                    break;
            }
            btimer.Start();
        }

        private void bithumbTicker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                                GridView grdView = new GridView();
                                grdView.AllowsColumnReorder = true;

                                GridViewColumn Name = new GridViewColumn();
                                Name.DisplayMemberBinding = new Binding("Name");
                                Name.Header = "Name";
                                Name.Width = 100;
                                grdView.Columns.Add(Name);

                                GridViewColumn opening_price = new GridViewColumn();
                                opening_price.DisplayMemberBinding = new Binding("opening_price");
                                opening_price.Header = "opening_price";
                                opening_price.Width = 100;
                                grdView.Columns.Add(opening_price);

                                GridViewColumn closing_price = new GridViewColumn();
                                closing_price.DisplayMemberBinding = new Binding("closing_price");
                                closing_price.Header = "closing_price";
                                closing_price.Width = 100;
                                grdView.Columns.Add(closing_price);

                                GridViewColumn min_price = new GridViewColumn();
                                min_price.DisplayMemberBinding = new Binding("min_price");
                                min_price.Header = "min_price";
                                min_price.Width = 100;
                                grdView.Columns.Add(min_price);

                                GridViewColumn max_price = new GridViewColumn();
                                max_price.DisplayMemberBinding = new Binding("max_price");
                                max_price.Header = "max_price";
                                max_price.Width = 100;
                                grdView.Columns.Add(max_price);

                                GridViewColumn average_price = new GridViewColumn();
                                average_price.DisplayMemberBinding = new Binding("average_price");
                                average_price.Header = "average_price";
                                average_price.Width = 100;
                                grdView.Columns.Add(average_price);

                                GridViewColumn units_traded = new GridViewColumn();
                                units_traded.DisplayMemberBinding = new Binding("units_traded");
                                units_traded.Header = "units_traded";
                                units_traded.Width = 100;
                                grdView.Columns.Add(units_traded);

                                GridViewColumn volume_1day = new GridViewColumn();
                                volume_1day.DisplayMemberBinding = new Binding("volume_1day");
                                volume_1day.Header = "volume_1day";
                                volume_1day.Width = 100;
                                grdView.Columns.Add(volume_1day);

                                GridViewColumn volume_7day = new GridViewColumn();
                                volume_7day.DisplayMemberBinding = new Binding("volume_7day");
                                volume_7day.Header = "volume_7day";
                                volume_7day.Width = 100;
                                grdView.Columns.Add(volume_7day);

                                GridViewColumn buy_price = new GridViewColumn();
                                buy_price.DisplayMemberBinding = new Binding("buy_price");
                                buy_price.Header = "buy_price";
                                buy_price.Width = 100;
                                grdView.Columns.Add(buy_price);

                                GridViewColumn sell_price = new GridViewColumn();
                                sell_price.DisplayMemberBinding = new Binding("sell_price");
                                sell_price.Header = "average_price";
                                sell_price.Width = 100;
                                grdView.Columns.Add(sell_price);

                                GridViewColumn date = new GridViewColumn();
                                date.DisplayMemberBinding = new Binding("date");
                                date.Header = "date";
                                date.Width = 100;
                                grdView.Columns.Add(date);
                                #endregion
                List<bithumbTicker> items = new List<bithumbTicker>();
                #region Ticker items
                bithumb_Public.ticker.Call_Public("BTC");
                items.Add(new bithumbTicker()
                {
                    Name = "BTC",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);


                bithumb_Public.ticker.Call_Public("ETH");
                items.Add(new bithumbTicker()
                {
                    Name = "ETH",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("DASH");
                items.Add(new bithumbTicker()
                {
                    Name = "DASH",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("LTC");
                items.Add(new bithumbTicker()
                {
                    Name = "LTC",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("ETC");
                items.Add(new bithumbTicker()
                {
                    Name = "ETC",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("XRP");
                items.Add(new bithumbTicker()
                {
                    Name = "XRP",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("BCH");
                items.Add(new bithumbTicker()
                {
                    Name = "BCH",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("XMR");
                items.Add(new bithumbTicker()
                {
                    Name = "XMR",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("ZEC");
                items.Add(new bithumbTicker()
                {
                    Name = "ZEC",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("QTUM");
                items.Add(new bithumbTicker()
                {
                    Name = "QTUM",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("BTG");
                items.Add(new bithumbTicker()
                {
                    Name = "BTG",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                bithumb_Public.ticker.Call_Public("EOS");
                items.Add(new bithumbTicker()
                {
                    Name = "EOS",
                    opening_price = (string)bithumbdata.Humb_Public_ticker["opening_price"],
                    closing_price = (string)bithumbdata.Humb_Public_ticker["closing_price"],
                    min_price = (string)bithumbdata.Humb_Public_ticker["min_price"],
                    max_price = (string)bithumbdata.Humb_Public_ticker["max_price"],
                    average_price = (string)bithumbdata.Humb_Public_ticker["average_price"],
                    units_traded = (string)bithumbdata.Humb_Public_ticker["units_traded"],
                    volume_1day = (string)bithumbdata.Humb_Public_ticker["volume_1day"],
                    volume_7day = (string)bithumbdata.Humb_Public_ticker["volume_7day"],
                    buy_price = (string)bithumbdata.Humb_Public_ticker["buy_price"],
                    sell_price = (string)bithumbdata.Humb_Public_ticker["sell_price"],
                    date = (string)bithumbdata.Humb_Public_ticker["date"],
                });
                MainListView.ItemsSource = items;
                Task.Delay(100);

                MainListView.View = grdView;
#endregion
            }catch(Exception ex)
            {

            }
        }

        private void bithumborderbook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                orderbook.Call_Public("0", 10, (string)((ComboBoxItem)bithumb_ord_currency.SelectedItem).Content);
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn timestamp = new GridViewColumn();
                timestamp.DisplayMemberBinding = new Binding("timestamp");
                timestamp.Header = "timestamp";
                timestamp.Width = 100;
                grdView.Columns.Add(timestamp);

                GridViewColumn order_currency = new GridViewColumn();
                order_currency.DisplayMemberBinding = new Binding("order_currency");
                order_currency.Header = "order_currency";
                order_currency.Width = 100;
                grdView.Columns.Add(order_currency);

                GridViewColumn payment_currency = new GridViewColumn();
                payment_currency.DisplayMemberBinding = new Binding("payment_currency");
                payment_currency.Header = "payment_currency";
                payment_currency.Width = 100;
                grdView.Columns.Add(payment_currency);

                GridViewColumn bids = new GridViewColumn();
                bids.DisplayMemberBinding = new Binding("bids");
                bids.Header = "bids";
                bids.Width = 150;
                grdView.Columns.Add(bids);

                GridViewColumn asks = new GridViewColumn();
                asks.DisplayMemberBinding = new Binding("asks");
                asks.Header = "asks";
                asks.Width = 150;
                grdView.Columns.Add(asks);

                MainListView.View = grdView;
                #endregion
                List<bithumborderbook> items = new List<bithumborderbook>();
                #region orderbook items
                items.Add(new bithumborderbook()
                {
                    timestamp = (string)bithumbdata.Humb_Public_order["timestamp"],
                    order_currency = (string)bithumbdata.Humb_Public_order["order_currency"],
                    payment_currency = (string)bithumbdata.Humb_Public_order["payment_currency"],
                });
                for (int i = 0; i < (int)bithumbdata.Humb_Public_order["Length bids"]; i++)
                    items.Add(new bithumborderbook()
                    {
                        bids = (string)bithumbdata.Humb_Public_order["bids" + i],
                        asks = (string)bithumbdata.Humb_Public_order["asks" + i]
                    });
                MainListView.ItemsSource = items;
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void bithumbrecent_transactions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn cont_no = new GridViewColumn();
                cont_no.DisplayMemberBinding = new Binding("cont_no");
                cont_no.Header = "cont_no";
                cont_no.Width = 180;
                grdView.Columns.Add(cont_no);

                GridViewColumn transaction_date = new GridViewColumn();
                transaction_date.DisplayMemberBinding = new Binding("transaction_date");
                transaction_date.Header = "transaction_date";
                transaction_date.Width = 230;
                grdView.Columns.Add(transaction_date);

                GridViewColumn type = new GridViewColumn();
                type.DisplayMemberBinding = new Binding("type");
                type.Header = "type";
                type.Width = 100;
                grdView.Columns.Add(type);

                GridViewColumn units_traded = new GridViewColumn();
                units_traded.DisplayMemberBinding = new Binding("units_traded");
                units_traded.Header = "units_traded";
                units_traded.Width = 120;
                grdView.Columns.Add(units_traded);

                GridViewColumn price = new GridViewColumn();
                price.DisplayMemberBinding = new Binding("price");
                price.Header = "bids";
                price.Width = 110;
                grdView.Columns.Add(price);

                GridViewColumn total = new GridViewColumn();
                total.DisplayMemberBinding = new Binding("total");
                total.Header = "total";
                total.Width = 110;
                grdView.Columns.Add(total);

                MainListView.View = grdView;
                #endregion
                recent_transactions.Call_Public((string)((ComboBoxItem)bithumb_trans.SelectedItem).Content, 0, 50);

                List<bithumbrecent_transactions> items = new List<bithumbrecent_transactions>();
                #region bithumb trans items
                for (int i = 0; i < bithumbdata.recent_transactions_count; i++)
                    items.Add(new bithumbrecent_transactions()
                    {
                        cont_no = (string)bithumbdata.Humb_Public_rec_trans["cont_no" + i],
                        transaction_date = (string)bithumbdata.Humb_Public_rec_trans["transaction_date" + i],
                        type = (string)bithumbdata.Humb_Public_rec_trans["type" + i],
                        units_traded = (string)bithumbdata.Humb_Public_rec_trans["units_traded" + i],
                        price = (string)bithumbdata.Humb_Public_rec_trans["price" + i],
                        total = (string)bithumbdata.Humb_Public_rec_trans["total" + i],
                    });

                MainListView.ItemsSource = items;
                #endregion
            }catch(Exception ex)
            {

            }
            
        }

        private void premium_money_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void premium_Click(object sender, RoutedEventArgs e)
        {
            premiumresullt.Content = ExchangeRate.Request_Json((string)((ComboBoxItem)bithumb_currency_ex.SelectedItem).Content, (string)((ComboBoxItem)premium_money.SelectedItem).Content);
        }

        private void bittrex_markethistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn Id = new GridViewColumn();
                Id.DisplayMemberBinding = new Binding("Id");
                Id.Header = "Id";
                Id.Width = 120;
                grdView.Columns.Add(Id);

                GridViewColumn TimeStamp = new GridViewColumn();
                TimeStamp.DisplayMemberBinding = new Binding("TimeStamp");
                TimeStamp.Header = "TimeStamp";
                TimeStamp.Width = 150;
                grdView.Columns.Add(TimeStamp);

                GridViewColumn Quantity = new GridViewColumn();
                Quantity.DisplayMemberBinding = new Binding("Quantity");
                Quantity.Header = "Quantity";
                Quantity.Width = 120;
                grdView.Columns.Add(Quantity);

                GridViewColumn Price = new GridViewColumn();
                Price.DisplayMemberBinding = new Binding("Price");
                Price.Header = "Price";
                Price.Width = 110;
                grdView.Columns.Add(Price);

                GridViewColumn Total = new GridViewColumn();
                Total.DisplayMemberBinding = new Binding("Total");
                Total.Header = "Total";
                Total.Width = 110;
                grdView.Columns.Add(Total);

                GridViewColumn FillType = new GridViewColumn();
                FillType.DisplayMemberBinding = new Binding("FillType");
                FillType.Header = "FillType";
                FillType.Width = 110;
                grdView.Columns.Add(FillType);

                GridViewColumn OrderType = new GridViewColumn();
                OrderType.DisplayMemberBinding = new Binding("OrderType");
                OrderType.Header = "OrderType";
                OrderType.Width = 110;
                grdView.Columns.Add(OrderType);

                MainListView.View = grdView;
                #endregion
                getmarkethistory.Call_bittrex_getmarkethistory((string)((ComboBoxItem)bittrex_history1.SelectedItem).Content +"-"+ (string)((ComboBoxItem)bittrex_history.SelectedItem).Content);
                List<bittrexmarkethistory> items = new List<bittrexmarkethistory>();
                #region bittrex markethistory items
                for (int i = 0; i < bittrexdata.getmarkethistory_count; i++)
                    items.Add(new bittrexmarkethistory()
                    {
                        Id = (string)bittrexdata.trex_history["Id" + i],
                        TimeStamp = (string)bittrexdata.trex_history["TimeStamp" + i],
                        Quantity = (string)bittrexdata.trex_history["Quantity" + i],
                        Price = (string)bittrexdata.trex_history["Price" + i],
                        Total = (string)bittrexdata.trex_history["Total" + i],
                        FillType = (string)bittrexdata.trex_history["FillType" + i],
                        OrderType = (string)bittrexdata.trex_history["OrderType" + i],
                    });

                MainListView.ItemsSource = items;
                #endregion
            }catch(Exception ex)
            {

            }
        }

        private void bittrex_market_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn MarketCurrency = new GridViewColumn();
                MarketCurrency.DisplayMemberBinding = new Binding("MarketCurrency");
                MarketCurrency.Header = "MarketCurrency";
                MarketCurrency.Width = 100;
                grdView.Columns.Add(MarketCurrency);

                GridViewColumn BaseCurrency = new GridViewColumn();
                BaseCurrency.DisplayMemberBinding = new Binding("BaseCurrency");
                BaseCurrency.Header = "BaseCurrency";
                BaseCurrency.Width = 100;
                grdView.Columns.Add(BaseCurrency);

                GridViewColumn MarketCurrencyLong = new GridViewColumn();
                MarketCurrencyLong.DisplayMemberBinding = new Binding("MarketCurrencyLong");
                MarketCurrencyLong.Header = "MarketCurrencyLong";
                MarketCurrencyLong.Width = 100;
                grdView.Columns.Add(MarketCurrencyLong);

                GridViewColumn BaseCurrencyLong = new GridViewColumn();
                BaseCurrencyLong.DisplayMemberBinding = new Binding("BaseCurrencyLong");
                BaseCurrencyLong.Header = "BaseCurrencyLong";
                BaseCurrencyLong.Width = 100;
                grdView.Columns.Add(BaseCurrencyLong);

                GridViewColumn MinTradeSize = new GridViewColumn();
                MinTradeSize.DisplayMemberBinding = new Binding("MinTradeSize");
                MinTradeSize.Header = "max_price";
                MinTradeSize.Width = 100;
                grdView.Columns.Add(MinTradeSize);

                GridViewColumn MarketName = new GridViewColumn();
                MarketName.DisplayMemberBinding = new Binding("MarketName");
                MarketName.Header = "MarketName";
                MarketName.Width = 100;
                grdView.Columns.Add(MarketName);

                GridViewColumn IsActive = new GridViewColumn();
                IsActive.DisplayMemberBinding = new Binding("IsActive");
                IsActive.Header = "IsActive";
                IsActive.Width = 100;
                grdView.Columns.Add(IsActive);

                GridViewColumn Created = new GridViewColumn();
                Created.DisplayMemberBinding = new Binding("Created");
                Created.Header = "Created";
                Created.Width = 150;
                grdView.Columns.Add(Created);
                MainListView.View = grdView;
                #endregion
                getmarkets.Call_bittrex_getmarkets();

                List<bittrexmarket> items = new List<bittrexmarket>();
                #region bittrex getmarket
                for (int i = 0; i < bittrexdata.getmarkets_count; i++)
                    items.Add(new bittrexmarket()
                    {
                        MarketCurrency = (string)bittrexdata.trex_market["MarketCurrency" + i],
                        BaseCurrency = (string)bittrexdata.trex_market["BaseCurrency" + i],
                        MarketCurrencyLong = (string)bittrexdata.trex_market["MarketCurrencyLong" + i],
                        BaseCurrencyLong = (string)bittrexdata.trex_market["BaseCurrencyLong" + i],
                        MinTradeSize = (string)bittrexdata.trex_market["MinTradeSize" + i],
                        MarketName = (string)bittrexdata.trex_market["MarketName" + i],
                        IsActive = (string)bittrexdata.trex_market["IsActive" + i],
                        Created = (string)bittrexdata.trex_market["Created" + i],
                    });

                MainListView.ItemsSource = items;
                #endregion
            }catch(Exception ex)
            {

            }
        }

        private void bittrex_marketsummaries_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn MarketName = new GridViewColumn();
                MarketName.DisplayMemberBinding = new Binding("MarketName");
                MarketName.Header = "MarketName";
                MarketName.Width = 100;
                grdView.Columns.Add(MarketName);

                GridViewColumn High = new GridViewColumn();
                High.DisplayMemberBinding = new Binding("High");
                High.Header = "High";
                High.Width = 100;
                grdView.Columns.Add(High);

                GridViewColumn Low = new GridViewColumn();
                Low.DisplayMemberBinding = new Binding("Low");
                Low.Header = "Low";
                Low.Width = 100;
                grdView.Columns.Add(Low);

                GridViewColumn Volume = new GridViewColumn();
                Volume.DisplayMemberBinding = new Binding("Volume");
                Volume.Header = "Volume";
                Volume.Width = 100;
                grdView.Columns.Add(Volume);

                GridViewColumn Last = new GridViewColumn();
                Last.DisplayMemberBinding = new Binding("Last");
                Last.Header = "Last";
                Last.Width = 100;
                grdView.Columns.Add(Last);

                GridViewColumn BaseVolume = new GridViewColumn();
                BaseVolume.DisplayMemberBinding = new Binding("BaseVolume");
                BaseVolume.Header = "BaseVolume";
                BaseVolume.Width = 100;
                grdView.Columns.Add(BaseVolume);

                GridViewColumn TimeStamp = new GridViewColumn();
                TimeStamp.DisplayMemberBinding = new Binding("TimeStamp");
                TimeStamp.Header = "IsActive";
                TimeStamp.Width = 100;
                grdView.Columns.Add(TimeStamp);

                GridViewColumn Bid = new GridViewColumn();
                Bid.DisplayMemberBinding = new Binding("Bid");
                Bid.Header = "Bid";
                Bid.Width = 100;
                grdView.Columns.Add(Bid);

                GridViewColumn Ask = new GridViewColumn();
                Ask.DisplayMemberBinding = new Binding("Ask");
                Ask.Header = "Ask";
                Ask.Width = 100;
                grdView.Columns.Add(Ask);

                GridViewColumn OpenBuyOrders = new GridViewColumn();
                OpenBuyOrders.DisplayMemberBinding = new Binding("OpenBuyOrders");
                OpenBuyOrders.Header = "OpenBuyOrders";
                OpenBuyOrders.Width = 100;
                grdView.Columns.Add(OpenBuyOrders);

                GridViewColumn OpenSellOrders = new GridViewColumn();
                OpenSellOrders.DisplayMemberBinding = new Binding("OpenSellOrders");
                OpenSellOrders.Header = "OpenSellOrders";
                OpenSellOrders.Width = 100;
                grdView.Columns.Add(OpenSellOrders);

                GridViewColumn PrevDay = new GridViewColumn();
                PrevDay.DisplayMemberBinding = new Binding("PrevDay");
                PrevDay.Header = "PrevDay";
                PrevDay.Width = 100;
                grdView.Columns.Add(PrevDay);

                GridViewColumn Created = new GridViewColumn();
                Created.DisplayMemberBinding = new Binding("Created");
                Created.Header = "Created";
                Created.Width = 100;
                grdView.Columns.Add(Created);

                MainListView.View = grdView;
                #endregion
                getmarketsummaries.Call_bittrex_getmarketsummaries();
                List<bittrexmarketsummaries> items = new List<bittrexmarketsummaries>();
                #region bittrex getmarket
                for (int i = 0; i < bittrexdata.getmarketsummaries_count; i++)
                    items.Add(new bittrexmarketsummaries()
                    {
                        MarketName = bittrexdata.trex_summaries["MarketName" + i],
                        High = bittrexdata.trex_summaries["High" + i],
                        Low = bittrexdata.trex_summaries["Low" + i],
                        Volume = bittrexdata.trex_summaries["Volume" + i],
                        Last = bittrexdata.trex_summaries["Last" + i],
                        BaseVolume = bittrexdata.trex_summaries["BaseVolume" + i],
                        TimeStamp = bittrexdata.trex_summaries["TimeStamp" + i],
                        Bid = bittrexdata.trex_summaries["Bid" + i],
                        Ask = bittrexdata.trex_summaries["Ask" + i],
                        OpenBuyOrders = bittrexdata.trex_summaries["OpenBuyOrders" + i],
                        OpenSellOrders = bittrexdata.trex_summaries["OpenSellOrders" + i],
                        PrevDay = bittrexdata.trex_summaries["PrevDay" + i],
                        Created = bittrexdata.trex_summaries["Created" + i],
                    });
                MainListView.ItemsSource = items;
                #endregion
            }
            catch (Exception ex)
            {
            }
        }

        private void bittrex_ticker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn Bid = new GridViewColumn();
                Bid.DisplayMemberBinding = new Binding("Bid");
                Bid.Header = "Bid";
                Bid.Width = 200;
                grdView.Columns.Add(Bid);

                GridViewColumn Ask = new GridViewColumn();
                Ask.DisplayMemberBinding = new Binding("Ask");
                Ask.Header = "Ask";
                Ask.Width = 200;
                grdView.Columns.Add(Ask);

                GridViewColumn Last = new GridViewColumn();
                Last.DisplayMemberBinding = new Binding("Last");
                Last.Header = "Last";
                Last.Width = 200;
                grdView.Columns.Add(Last);


                MainListView.View = grdView;
                #endregion
                getticker.Call_bittrex_getticker((string)((ComboBoxItem)bittrex_ticker_market1.SelectedItem).Content + "-" + (string)((ComboBoxItem)bittrex_ticker_market.SelectedItem).Content);
                List<bittrexticker> items = new List<bittrexticker>();
                #region bittrex ticker
                items.Add(new bittrexticker()
                {
                    Bid = (string)bittrexdata.trex_ticker["Bid"],
                    Ask = (string)bittrexdata.trex_ticker["Ask"],
                    Last = (string)bittrexdata.trex_ticker["Last"],
                });

                MainListView.ItemsSource = items;
                #endregion
            }
            catch(Exception ex)
            {

            }

        }

        private void bittrex_orderbook_Click(object sender, RoutedEventArgs e)
        {
                #region MainListView Grid Setting
                GridView grdView = new GridView();
                grdView.AllowsColumnReorder = true;

                GridViewColumn buy = new GridViewColumn();
                buy.DisplayMemberBinding = new Binding("buy");
                buy.Header = "buy";
                buy.Width = 400;
                grdView.Columns.Add(buy);

                GridViewColumn sell = new GridViewColumn();
                sell.DisplayMemberBinding = new Binding("sell");
                sell.Header = "sell";
                sell.Width = 400;
                grdView.Columns.Add(sell);

                MainListView.View = grdView;
                #endregion
                getorderbook.Call_bittrex_getorderbook((string)((ComboBoxItem)bittrex_orderbook_curr1.SelectedItem).Content + "-" + (string)((ComboBoxItem)bittrex_orderbook_curr.SelectedItem).Content);
                List<bittrexorderbook> items = new List<bittrexorderbook>();
                #region bittrex getorderbook
                for (int i = 0; i < bittrexdata.getorderbook_count; i++)
                    items.Add(new bittrexorderbook()
                    {
                        buy = "Quantity: " + (string)bittrexdata.trex_orderbook["buy_Quantity" + i] +"\r\nRate:     "+ (string)bittrexdata.trex_orderbook["buy_Rate" + i],
                        sell = (string)bittrexdata.trex_orderbook["sell_Quantity" + i] + "\r\n" + (string)bittrexdata.trex_orderbook["sell_Rate" + i],
                    });
                MainListView.ItemsSource = items;
                #endregion
        }

        private void bithumb_krw_deposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                krw_deposit.Call_krw_deposit();
                bithumb_krw_deposit_label.Content = (string)bithumbdata.Humb_KRW_deposit["account"] + "   " + (string)bithumbdata.Humb_KRW_deposit["bank"] + "  " + (string)bithumbdata.Humb_KRW_deposit["BankUser"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bittrex_currency1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                account.Call_account((string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
                bithumb_balance_coin.Content = (string)bithumbdata.Humb_acc["balance"];
            }
            catch (Exception ex)
            {

            }
        }

        private void bithumb_currency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                account.Call_account((string)((ComboBoxItem)bithumb_currency.SelectedItem).Content);
                bithumb_balance_coin.Content = (string)bithumbdata.Humb_acc["balance"];
            }catch(Exception ex)
            {

            }
        }

        private void bittrex_buy_request_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bittrexdata.bittrexAPI_Key = BittrexAPI.Text;
                timer_Tick(sender, e);
                switch (bittrex_buy.IsChecked)
                {
                    case true:
                        MessageBox.Show(bittrex_buylimit.Call_bittrex_buylimit((string)((ComboBoxItem)bittrex_currency1.SelectedItem).Content + "-" + (string)((ComboBoxItem)bittrex_currency2.SelectedItem).Content, bittrex_quantity.Text, bittrex_rate.Text));
                        break;
                    case false:
                        MessageBox.Show(bittrex_selllimit.Call_bittrex_selllimit((string)((ComboBoxItem)bittrex_currency1.SelectedItem).Content + "-" + (string)((ComboBoxItem)bittrex_currency2.SelectedItem).Content, bittrex_quantity.Text, bittrex_rate.Text));
                        break;
                }
                btimer.Start();
            }catch(Exception ex)
            {

            }
        }

        private void bittrex_quantity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void bittrex_rate_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void bithumb_price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}