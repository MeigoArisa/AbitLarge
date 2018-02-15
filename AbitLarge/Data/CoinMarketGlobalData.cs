using Newtonsoft.Json;

namespace CoinMarketCap.Models
{
    public class GlobalDataModel
    {
        [JsonProperty("total_market_cap_usd")]
        public double? TotalMarketCapUSD { get; set; }

        [JsonProperty("total_24h_volume_usd")]
        public double? Total24HrVolumeUSD { get; set; }

        [JsonProperty("bitcoin_percentage_of_market_cap")]
        public double? BitcoinPercentageOfMarketCap { get; set; }

        [JsonProperty("active_currencies")]
        public int ActiveCurrencies { get; set; }

        [JsonProperty("active_assets")]
        public int ActiveAssets { get; set; }

        [JsonProperty("active_markets")]
        public int ActiveMarkets { get; set; }

        [JsonProperty("total_market_cap_aud")]
        public double? TotalMarketCapAUD { get; set; }

        [JsonProperty("total_24h_volume_aud")]
        public double? Total24HrVolumeAUD { get; set; }

        [JsonProperty("total_market_cap_brl")]
        public double? TotalMarketCapBRL { get; set; }

        [JsonProperty("total_24h_volume_brl")]
        public double? Total24HrVolumeBRL { get; set; }

        [JsonProperty("total_market_cap_cad")]
        public double? TotalMarketCapCAD { get; set; }

        [JsonProperty("total_24h_volume_cad")]
        public double? Total24HrVolumeCAD { get; set; }

        [JsonProperty("total_market_cap_chf")]
        public double? TotalMarketCapCHF { get; set; }

        [JsonProperty("total_24h_volume_chf")]
        public double? Total24HrVolumeCHF { get; set; }

        [JsonProperty("total_market_cap_cny")]
        public double? TotalMarketCapCNY { get; set; }

        [JsonProperty("total_24h_volume_cny")]
        public double? Total24HrVolumeCNY { get; set; }

        [JsonProperty("total_market_cap_eur")]
        public double? TotalMarketCapEUR { get; set; }

        [JsonProperty("total_24h_volume_eur")]
        public double? Total24HrVolumeEUR { get; set; }

        [JsonProperty("total_market_cap_gbp")]
        public double? TotalMarketCapGBP { get; set; }

        [JsonProperty("total_24h_volume_gbp")]
        public double? Total24HrVolumeGBP { get; set; }

        [JsonProperty("total_market_cap_hkd")]
        public double? TotalMarketCapHKD { get; set; }

        [JsonProperty("total_24h_volume_hkd")]
        public double? Total24HrVolumeHKD { get; set; }

        [JsonProperty("total_market_cap_idr")]
        public double? TotalMarketCapIDR { get; set; }

        [JsonProperty("total_24h_volume_idr")]
        public double? Total24HrVolumeIDR { get; set; }

        [JsonProperty("total_market_cap_inr")]
        public double? TotalMarketCapINR { get; set; }

        [JsonProperty("total_24h_volume_inr")]
        public double? Total24HrVolumeINR { get; set; }

        [JsonProperty("total_market_cap_jpy")]
        public double? TotalMarketCapJPY { get; set; }

        [JsonProperty("total_24h_volume_jpy")]
        public double? Total24HrVolumeJPY { get; set; }

        [JsonProperty("total_market_cap_krw")]
        public double? TotalMarketCapKRW { get; set; }

        [JsonProperty("total_24h_volume_krw")]
        public double? Total24HrVolumeKRW { get; set; }

        [JsonProperty("total_market_cap_mxn")]
        public double? TotalMarketCapMXN { get; set; }

        [JsonProperty("total_24h_volume_mxn")]
        public double? Total24HrVolumeMXN { get; set; }

        [JsonProperty("total_market_cap_rub")]
        public double? TotalMarketCapRUB { get; set; }

        [JsonProperty("total_24h_volume_rub")]
        public double? Total24HrVolumeRUB { get; set; }
    }
}
