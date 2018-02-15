using Newtonsoft.Json;
using System.Windows;

namespace CoinMarketCap.Models
{
    public class TickerModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("price_usd")]
        public double? PriceUSD { get; set; }

        [JsonProperty("price_btc")]
        public double? PriceBTC { get; set; }

        [JsonProperty("24h_volume_usd")]
        public double? VolumeUSD24Hrs { get; set; }

        [JsonProperty("market_cap_usd")]
        public double? MarketCapUSD { get; set; }

        [JsonProperty("available_supply")]
        public double? AvailableSupply { get; set; }

        [JsonProperty("total_supply")]
        public double? TotalSupply { get; set; }

        [JsonProperty("percent_change_1h")]
        public double? PercentChange1Hr { get; set; }

        [JsonProperty("percent_change_24h")]
        public double? PercentChange24Hr { get; set; }

        [JsonProperty("percent_change_7d")]
        public double? PercentChange7Days { get; set; }

        [JsonProperty("last_updated")]
        public double? LastUpdated { get; set; }

        [JsonProperty("price_inr")]
        public double? PriceINR { get; set; }

        [JsonProperty("24h_volume_inr")]
        public double? VolumeINR24Hr { get; set; }

        [JsonProperty("market_cap_inr")]
        public double? MarketCapINR { get; set; }

        [JsonProperty("price_aud")]
        public double? PriceAUD { get; set; }

        [JsonProperty("24h_volume_aud")]
        public double? VolumeAUD24Hr { get; set; }

        [JsonProperty("market_cap_aud")]
        public double? MarketCapAUD { get; set; }

        [JsonProperty("price_brl")]
        public double? PriceBRL { get; set; }

        [JsonProperty("24h_volume_brl")]
        public double? VolumeBRL24Hr { get; set; }

        [JsonProperty("market_cap_brl")]
        public double? MarketCapBRL { get; set; }

        [JsonProperty("price_cad")]
        public double? PriceCAD { get; set; }

        [JsonProperty("24h_volume_cad")]
        public double? VolumeCAD24Hr { get; set; }

        [JsonProperty("market_cap_cad")]
        public double? MarketCapCAD { get; set; }

        [JsonProperty("price_chf")]
        public double? PriceCHF { get; set; }

        [JsonProperty("24h_volume_chf")]
        public double? VolumeCHF24Hr { get; set; }

        [JsonProperty("market_cap_chf")]
        public double? MarketCapCHF { get; set; }

        [JsonProperty("price_cny")]
        public double? PriceCNY { get; set; }

        [JsonProperty("24h_volume_cny")]
        public double? VolumeCNY24Hr { get; set; }

        [JsonProperty("market_cap_cny")]
        public double? MarketCapCNY { get; set; }

        [JsonProperty("price_eur")]
        public double? PriceEUR { get; set; }

        [JsonProperty("24h_volume_eur")]
        public double? VolumeEUR24Hr { get; set; }

        [JsonProperty("market_cap_eur")]
        public double? MarketCapEUR { get; set; }

        [JsonProperty("price_gbp")]
        public double? PriceGBP { get; set; }

        [JsonProperty("24h_volume_gbp")]
        public double? VolumeGBP24Hr { get; set; }

        [JsonProperty("market_cap_gbp")]
        public double? MarketCapGBP { get; set; }

        [JsonProperty("price_hkd")]
        public double? PriceHKD { get; set; }

        [JsonProperty("24h_volume_hkd")]
        public double? VolumeHKD24Hr { get; set; }

        [JsonProperty("market_cap_hkd")]
        public double? MarketCapHKD { get; set; }

        [JsonProperty("price_idr")]
        public double? PriceIDR { get; set; }

        [JsonProperty("24h_volume_idr")]
        public double? VolumeIDR24Hr { get; set; }

        [JsonProperty("market_cap_idr")]
        public double? MarketCapIDR { get; set; }

        [JsonProperty("price_jpy")]
        public double? PriceJPY { get; set; }

        [JsonProperty("24h_volume_jpy")]
        public double? VolumeJPY24Hr { get; set; }

        [JsonProperty("market_cap_jpy")]
        public double? MarketCapJPY { get; set; }

        [JsonProperty("price_krw")]
        public double? PriceKRW { get; set; }

        [JsonProperty("24h_volume_krw")]
        public double? VolumeKRW24Hr { get; set; }

        [JsonProperty("market_cap_krw")]
        public double? MarketCapKRW { get; set; }

        [JsonProperty("price_mxn")]
        public double? PriceMXN { get; set; }

        [JsonProperty("24h_volume_mxn")]
        public double? VolumeMXN24Hr { get; set; }

        [JsonProperty("market_cap_mxn")]
        public double? MarketCapMXN { get; set; }

        [JsonProperty("price_rub")]
        public double? PriceRUB { get; set; }

        [JsonProperty("24h_volume_rub")]
        public double? VolumeRUB24Hr { get; set; }

        [JsonProperty("market_cap_rub")]
        public double? MarketCapRUB { get; set; }
    }
}
