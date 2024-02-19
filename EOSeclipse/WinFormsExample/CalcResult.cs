using System;
using System.Globalization;


namespace MainForm
{
    
    public class CalcResult
    {
        #region variables
        public CalcRaw raw ;
        public string ecltype;
        public TimeSpan? duration;
        public TimeSpan? duration_corr;
        public decimal? coverage;
        public decimal? depth;
        public string eclipse_depth;
        public decimal? deltaT;
        public decimal? mag;
        public decimal? sunmoonratio;
        public DateTime? c1_datetime;
        public decimal? c1_alt;
        public decimal? c1_adjalt;
        public decimal? c1_azi;
        public decimal? c1_p;
        public decimal? c1_v;
        public DateTime? c2_datetime;
        public decimal? c2_alt;
        public decimal? c2_adjalt;
        public decimal? c2_azi;
        public decimal? c2_p;
        public decimal? c2_v;
        public decimal? c2_lc;
        public DateTime? mid_datetime;
        public decimal? mid_alt;
        public decimal? mid_adjalt;
        public decimal? mid_azi;
        public decimal? mid_p;
        public decimal? mid_v;
        public DateTime? c3_datetime;
        public decimal? c3_alt;
        public decimal? c3_adjalt;
        public decimal? c3_azi;
        public decimal? c3_p;
        public decimal? c3_v;
        public decimal? c3_lc;
        public DateTime? c4_datetime;
        public decimal? c4_alt;
        public decimal? c4_adjalt;
        public decimal? c4_azi;
        public decimal? c4_p;
        public decimal? c4_v;
        public decimal? libl;
        public decimal? libb;
        public decimal? pac;
        public string watts_chart_link;
        #endregion

        public CalcResult(CalcRaw raw)
        {
            this.raw = raw;
            this.duration = parse_duration(raw.duration);
            this.duration_corr = parse_duration(raw.duration_corr);
            this.coverage = parse_decimal(raw.coverage);
            this.depth = parse_decimal(raw.depth);
            this.deltaT = parse_decimal(raw.deltaT);
            this.mag = parse_decimal(raw.mag);
            this.sunmoonratio = parse_decimal(raw.sunmoonratio);
            this.c1_alt = parse_decimal(raw.c1_alt);
            this.c1_adjalt = parse_decimal(raw.c1_adjalt);
            this.c1_azi = parse_decimal(raw.c1_azi);
            this.c1_p = parse_decimal(raw.c1_p);
            this.c1_v = parse_decimal(raw.c1_v);
            this.c2_alt = parse_decimal(raw.c2_alt);
            this.c2_adjalt = parse_decimal(raw.c2_adjalt);
            this.c2_azi = parse_decimal(raw.c2_azi);
            this.c2_p = parse_decimal(raw.c2_p);
            this.c2_v = parse_decimal(raw.c2_v);
            this.c2_lc = parse_decimal(raw.c2_lc);
            this.c3_alt = parse_decimal(raw.c3_alt);
            this.c3_adjalt = parse_decimal(raw.c3_adjalt);
            this.c3_azi = parse_decimal(raw.c3_azi);
            this.c3_p = parse_decimal(raw.c3_p);
            this.c3_v = parse_decimal(raw.c3_v);
            this.c3_lc = parse_decimal(raw.c3_lc);
            this.c4_alt = parse_decimal(raw.c4_alt);
            this.c4_adjalt = parse_decimal(raw.c4_adjalt);
            this.c4_azi = parse_decimal(raw.c4_azi);
            this.c4_p = parse_decimal(raw.c4_p);
            this.c4_v = parse_decimal(raw.c4_v);
            this.mid_alt = parse_decimal(raw.mid_alt);
            this.mid_adjalt = parse_decimal(raw.mid_adjalt);
            this.mid_azi = parse_decimal(raw.mid_azi);
            this.mid_p = parse_decimal(raw.mid_p);
            this.mid_v = parse_decimal(raw.mid_v);
            this.libl = parse_decimal(raw.libl);
            this.libb = parse_decimal(raw.libb);
            this.pac = parse_decimal(raw.pac);
            this.c1_datetime = parse_datetime(raw.c1_date, raw.c1_time, raw.tz);
            this.c2_datetime = parse_datetime(raw.c2_date, raw.c2_time, raw.tz);
            this.mid_datetime = parse_datetime(raw.mid_date, raw.mid_time, raw.tz);
            this.c3_datetime = parse_datetime(raw.c3_date, raw.c3_time, raw.tz);
            this.c4_datetime = parse_datetime(raw.c4_date, raw.c4_time, raw.tz);
            this.ecltype = raw.ecltype;
            this.eclipse_depth = raw.eclipse_depth;
            this.watts_chart_link = raw.watts_chart_link;
        }

        private TimeSpan? parse_duration(string duration)
        {
            if ((duration == null) | (duration == string.Empty) | (duration == "n/a"))
            {
                return null;
            }
            else
            {
                duration = duration.Replace("[", string.Empty);
                duration = duration.Replace("]", string.Empty);
                CultureInfo provider = CultureInfo.InvariantCulture;
                string format = "m'm 'ss\\.f\\s";
                return TimeSpan.ParseExact(duration, format, provider);
            }
        }

        private decimal? parse_decimal(string string_value)
        {
            decimal? number;
            NumberStyles style = NumberStyles.Float;
            if (string_value != null) 
            {
                string_value = string_value.Replace("?", string.Empty);
                string_value = string_value.Replace("n/a", string.Empty);
                string_value = string_value.Replace("%", string.Empty);
                string_value = string_value.Replace("\xB0", string.Empty);
                string_value = string_value.Replace("s", string.Empty);
                if (string_value != string.Empty)
                {
                    number = Decimal.Parse(string_value, style);
                    return number;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private DateTime? parse_datetime(string date, string time, string timezone)
        {
            if ((date == "n/a") | (date == string.Empty) | (time == string.Empty))
            {
                return null;
            }
            else
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                string dateString, format;

                dateString = date + "T" + time;
                format = "yyyy/MM/ddTHH:mm:ss.fzzz";

                // parse timezone - beware, Xavier's code uses resverse signs (- for east of Greenwich)
                decimal tz;
                int tzh, tzm;
                NumberStyles style = NumberStyles.Float;
                tz = Decimal.Parse(timezone, style) * -1;
                if (tz != 0)
                {
                    tzh = decimal.ToInt16(Math.Floor(Math.Abs(tz)) * tz / Math.Abs(tz));
                    tzm = decimal.ToInt16(Math.Round((tz - tzh) * 60));
                }
                else
                {
                    tzh = 0;
                    tzm = 0;
                }
                if (tz >= 0) { dateString += "+" + tzh.ToString("D2") + ":" + tzm.ToString("D2"); }
                else { dateString += tzh.ToString("D2") + ":" + tzm.ToString("D2"); }

                return DateTime.ParseExact(dateString, format, provider);
            }
        }


    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class CalcRaw
    {
        public String ecltype;
        public String duration;
        public String duration_corr;
        public String coverage;
        public String depth;
        public String eclipse_depth;
        public String deltaT;
        public String mag;
        public String sunmoonratio;
        public String c1_date;
        public String c1_time;
        public String c1_alt;
        public String c1_adjalt;
        public String c1_azi;
        public String c1_p;
        public String c1_v;
        public String c2_date;
        public String c2_time;
        public String c2_alt;
        public String c2_adjalt;
        public String c2_azi;
        public String c2_p;
        public String c2_v;
        public String c2_lc;
        public String mid_date;
        public String mid_time;
        public String mid_alt;
        public String mid_adjalt;
        public String mid_azi;
        public String mid_p;
        public String mid_v;
        public String c3_date;
        public String c3_time;
        public String c3_alt;
        public String c3_adjalt;
        public String c3_azi;
        public String c3_p;
        public String c3_v;
        public String c3_lc;
        public String c4_date;
        public String c4_time;
        public String c4_alt;
        public String c4_adjalt;
        public String c4_azi;
        public String c4_p;
        public String c4_v;
        public string libl;
        public string libb;
        public string pac;
        public string watts_chart_link;
        public string tz;
    }

}
