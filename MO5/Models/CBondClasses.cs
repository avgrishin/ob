using System;
using System.Collections.Generic;

namespace MO5.Models
{
  public class get_tradings_stocks
  {
    public string id { get; set; }
    public string agreement_number { get; set; }
    public string avar_price { get; set; }
    public string begin_circulation_micex { get; set; }
    public string buying_quote { get; set; }
    public string change_1_day { get; set; }
    public string change_30_day { get; set; }
    public string change_365_day { get; set; }
    public string currency_id { get; set; }
    public string currency_name { get; set; }
    public string currency_name_rus { get; set; }
    public string emission_full_name_eng { get; set; }
    public string emission_full_name_rus { get; set; }
    public string emission_id { get; set; }
    public string emission_name_eng { get; set; }
    public string emission_name_rus { get; set; }
    public string emitent_full_name_eng { get; set; }
    public string emitent_full_name_rus { get; set; }
    public string emitent_id { get; set; }
    public string emitent_name_eng { get; set; }
    public string emitent_name_rus { get; set; }
    public string end_circulation_micex { get; set; }
    public string isin { get; set; }
    public string kod_micex { get; set; }
    public string kot_micex { get; set; }
    public string last_price { get; set; }
    public string max_price { get; set; }
    public string micex { get; set; }
    public string micex_marketprice3 { get; set; }
    public string min_price { get; set; }
    public string open_price { get; set; }
    public string overturn { get; set; }
    public string overturn_rur { get; set; }
    public string place_full_name_eng { get; set; }
    public string place_full_name_rus { get; set; }
    public string place_id { get; set; }
    public string place_name_eng { get; set; }
    public string place_name_rus { get; set; }
    public string selling_quote { get; set; }
    public string trading_date { get; set; }
    public string trading_time { get; set; }
    public DateTime update_time { get; set; }
  }

  public class get_tradings
  {
    public string id { get; set; }
    public string admittedquote { get; set; }
    public string agreement_number { get; set; }
    public string avar_price { get; set; }
    public string board_id { get; set; }
    public string buying_quote { get; set; }
    public string clearance_profit { get; set; }
    public string clearance_profit_effect { get; set; }
    public string clearance_profit_nominal { get; set; }
    public string convexity { get; set; }
    public string convexity_offer { get; set; }
    public string date { get; set; }
    public string dur { get; set; }
    public string dur_mod { get; set; }
    public string dur_mod_to { get; set; }
    public string dur_to { get; set; }
    public string emission_emitent_country_id { get; set; }
    public string emission_emitent_country_region_id { get; set; }
    public string emission_emitent_id { get; set; }
    public string emission_id { get; set; }
    public string indicative_price { get; set; }
    public string indicative_price_type { get; set; }
    public string indicative_yield { get; set; }
    public string indicative_yield_type { get; set; }
    public string last_price { get; set; }
    public string legalcloseprice { get; set; }
    public string marketprice { get; set; }
    public string marketprice2 { get; set; }
    public string max_price { get; set; }
    public string mid_price { get; set; }
    public string min_price { get; set; }
    public string nkd { get; set; }
    public string offer_date { get; set; }
    public string offer_profit { get; set; }
    public string offer_profit_effect { get; set; }
    public string offer_profit_nominal { get; set; }
    public string open_price { get; set; }
    public string overturn { get; set; }
    public string pvbp { get; set; }
    public string pvbp_offer { get; set; }
    public string selling_quote { get; set; }
    public string trading_ground_id { get; set; }
    public string volume { get; set; }
    public string volume_money { get; set; }
    public string years_to_maturity { get; set; }
    public string ytm_bid { get; set; }
    public string ytm_close { get; set; }
    public string ytm_last { get; set; }
    public string ytm_max { get; set; }
    public string ytm_min { get; set; }
    public string ytm_offer { get; set; }
    public string ytm_open { get; set; }
    public string yto_bid { get; set; }
    public string yto_close { get; set; }
    public string yto_last { get; set; }
    public string yto_offer { get; set; }
    public string bid_ask_spread { get; set; }
    public string g_spread { get; set; }
    public string t_spread { get; set; }
    public string t_spread_benchmark { get; set; }
    public string isin_code { get; set; }
    public string isin_code_144a { get; set; }
    public string isin_code_3 { get; set; }
    public string document_eng { get; set; }
    public string document_rus { get; set; }
    public string document_pol { get; set; }
    public string document_ita { get; set; }
    public string emission_kind_id { get; set; }
  }

  public class Meta
  {
    public double cms_full_gen_time { get; set; }
    public int user_id { get; set; }
    public string lang { get; set; }
    public int strict_mode { get; set; }
    public int performLogging { get; set; }
  }

  public class RootObject<T>
  {
    public int count { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public double exec_time { get; set; }
    public List<T> items { get; set; }
    public Meta meta { get; set; }
  }
}