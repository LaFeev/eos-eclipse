using System;
using System.Globalization;
using System.Linq;

namespace EOSDigital.API
{
    
    public class CalcResult
    {
        #region variables
        private SePhase[] PhaseList {set; get;}
        public string SeType { set; get; }
        public string EclipseDepth { set; get; }
        public string WattsChartLink { set; get; }
        public SeTimeSpan Duration { set; get; }
        public SeTimeSpan DurationCorr { set; get; }
        public SeDecimal Coverage { set; get; }
        public SeDecimal Depth { set; get; }
        public SeDecimal DeltaT { set; get; }
        public SeDecimal Magnitude { set; get; }
        public SeDecimal SunMoonRatio { set; get; }
        public SeDecimal LibL { set; get; }
        public SeDecimal LibB { set; get; }
        public SeDecimal PaC { set; get; }
        public double Lat {  set; get; }
        public double Lng { set; get; }
        public double Elv { set; get; }
        #endregion

        public CalcResult(CalcRaw raw)
        {
            this.Duration = new SeTimeSpan(parse_duration(raw.duration));
            this.DurationCorr = new SeTimeSpan(parse_duration(raw.duration_corr));
            this.DurationCorr.SetFormat(@"\[mm\:ss\.f\]");
            this.PhaseList = new SePhase[5];
            this.PhaseList[0] = new SePhase(
                "C1",
                parse_datetime(raw.c1_date, raw.c1_time, raw.tz),
                parse_decimal(raw.c1_alt),
                parse_decimal(raw.c1_adjalt),
                parse_decimal(raw.c1_azi),
                parse_decimal(raw.c1_p),
                parse_decimal(raw.c1_v));
            this.PhaseList[1] = new SePhase(
                "C2",
                parse_datetime(raw.c2_date, raw.c2_time, raw.tz),
                parse_decimal(raw.c2_alt),
                parse_decimal(raw.c2_adjalt),
                parse_decimal(raw.c2_azi),
                parse_decimal(raw.c2_p),
                parse_decimal(raw.c2_v),
                parse_decimal(raw.c2_lc));
            this.PhaseList[2] = new SePhase(
                "Mx",
                parse_datetime(raw.mid_date, raw.mid_time, raw.tz),
                parse_decimal(raw.mid_alt),
                parse_decimal(raw.mid_adjalt),
                parse_decimal(raw.mid_azi),
                parse_decimal(raw.mid_p),
                parse_decimal(raw.mid_v));
            this.PhaseList[3] = new SePhase(
                "C3",
                parse_datetime(raw.c3_date, raw.c3_time, raw.tz),
                parse_decimal(raw.c3_alt),
                parse_decimal(raw.c3_adjalt),
                parse_decimal(raw.c3_azi),
                parse_decimal(raw.c3_p),
                parse_decimal(raw.c3_v),
                parse_decimal(raw.c3_lc));
            this.PhaseList[4] = new SePhase(
                "C4",
                parse_datetime(raw.c4_date, raw.c4_time, raw.tz),
                parse_decimal(raw.c4_alt),
                parse_decimal(raw.c4_adjalt),
                parse_decimal(raw.c4_azi),
                parse_decimal(raw.c4_p),
                parse_decimal(raw.c4_v));
            this.Coverage = new SeDecimal(parse_decimal(raw.coverage));
            this.Coverage.SetFormat(@"#.00\%");
            this.Depth = new SeDecimal(parse_decimal(raw.depth));
            this.Depth.SetFormat(@"#.00\%");
            this.DeltaT = new SeDecimal(parse_decimal(raw.deltaT));
            this.DeltaT.SetFormat(@"#.0\s");
            this.Magnitude = new SeDecimal(parse_decimal(raw.mag));
            this.SunMoonRatio = new SeDecimal(parse_decimal(raw.sunmoonratio));
            this.LibL = new SeDecimal(parse_decimal(raw.libl));
            this.LibL.SetFormat("0.00\u00B0");
            this.LibB = new SeDecimal(parse_decimal(raw.libb));
            this.LibB.SetFormat("0.00\u00B0");
            this.PaC = new SeDecimal(parse_decimal(raw.pac));
            this.PaC.SetFormat("0.00\u00B0");
            this.SeType = raw.ecltype;
            this.EclipseDepth = raw.eclipse_depth;
            this.WattsChartLink = raw.watts_chart_link;
        }

        private TimeSpan? parse_duration(string duration)
        {
            if ((duration == null) | (duration == string.Empty) | (duration == "n/a") | (duration == "???"))
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

        public SePhase Phase(string id)
        {
            foreach (SePhase phase in this.PhaseList) 
            {
                if (phase.ID == id) return phase;
            }
            return null;
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
        public String libl;
        public String libb;
        public String pac;
        public String watts_chart_link;
        public String tz;
    }

    public class SePhase
    {
        public String ID { get;set;}
        public SeDateTime DateTime { get; set; }
        public SeDecimal Altitude { get; set; }
        public SeDecimal AdjAltitude { get; set; }
        public SeDecimal Azimuth { get; set; }
        public SeDecimal P { get; set; }
        public SeDecimal V { get; set; }
        public SeDecimal LC { get; set; }
        private readonly string[] idValues = { "C1", "C2", "Mx", "C3", "C4" };

        protected SePhase()
        {
            ID = string.Empty;
            DateTime = new SeDateTime(null);
            Altitude = new SeDecimal(null);
            AdjAltitude = new SeDecimal(null);
            Azimuth = new SeDecimal(null);
            P = new SeDecimal(null);
            V = new SeDecimal(null);
            LC = new SeDecimal(null);
        }

        public SePhase(string value) : this() 
        {
            if (idValues.Contains(value)) ID = value;
        }

        public SePhase(string id, DateTime? datetime, decimal? alt, decimal? adjalt, decimal? azi, decimal? p, decimal? v, decimal? lc=null) : this(id)
        {
            if (idValues.Contains(id))
            {
                this.DateTime.SetValue(datetime);
                this.Altitude.SetFormat("0.0\u00B0");
                this.Altitude.SetValue(alt);
                this.AdjAltitude.SetFormat("0.0\u00B0");
                this.AdjAltitude.SetValue(adjalt);
                this.Azimuth.SetFormat("0.0\u00B0");
                this.Azimuth.SetValue(azi);
                this.P.SetFormat("000\u00B0");
                this.P.SetValue(p);
                this.V.SetFormat("00.0");
                this.V.SetValue(v);
                this.LC.SetFormat("0.0's'");
                this.LC.SetValue(lc);
                this.ID = id;
            }
        }
    }

    [Serializable]
    public class SeDateTime
    {
        public string DisplayValue { get; set; }
        public DateTime ComputeValue { get; set; }
        public bool IsNull { get; set; }
        private string format = @"yyyy\/MM\/dd'  'HH\:mm\:ss\.f";

        protected SeDateTime() 
        {
            this.IsNull = true;
            this.DisplayValue = string.Empty;
            this.ComputeValue = DateTime.MinValue;
        }

        public SeDateTime(DateTime? value) : this()
        {
            this.SetValue(value);
        }

        public void SetFormat(string format)
        {
            this.format = format;
            if (!this.IsNull)
            {
                this.DisplayValue = this.ComputeValue.ToString(format);
            }
        }

        public void SetValue(DateTime? value)
        {
            if (value == null)
            {
                this.IsNull = true;
                this.DisplayValue = "-";
                this.ComputeValue = DateTime.MinValue;
            }
            else
            {
                this.IsNull = false;
                this.DisplayValue = ((DateTime)value).ToString(format);
                this.ComputeValue = (DateTime)value;
            }
        }
    }

    public class SeTimeSpan
    {
        public string DisplayValue { get; set; }
        public TimeSpan ComputeValue { get; set; }
        public bool IsNull { get; set; }
        private string format = @"mm\:ss\.f";

        protected SeTimeSpan()
        {
            this.IsNull = true;
            this.DisplayValue = string.Empty;
            this.ComputeValue = TimeSpan.Zero;
        }

        public SeTimeSpan(TimeSpan? value) : this()
        {
            this.SetValue(value);
        }

        public void SetFormat(string format)
        {
            this.format = format;
            if (!this.IsNull)
            {
                this.DisplayValue = this.ComputeValue.ToString(format);
            }
        }

        public void SetValue(TimeSpan? value)
        {
            if (value == null)
            {
                this.IsNull = true;
                this.DisplayValue = "-";
                this.ComputeValue = TimeSpan.Zero;
            }
            else
            {
                this.IsNull = false;
                this.DisplayValue = ((TimeSpan)value).ToString(format);
                this.ComputeValue = (TimeSpan)value;
            }
        }
    }

    public class SeDecimal
    {
        public string DisplayValue { get; set; }
        public decimal ComputeValue { get; set; }
        public bool IsNull { get; set; }
        private string format = @"0.00000";

        protected SeDecimal()
        {
            this.IsNull = true;
            this.DisplayValue = string.Empty;
            this.ComputeValue = 0;
        }

        public SeDecimal(decimal? value) : this()
        {
            this.SetValue(value);
        }

        public void SetFormat(string format)
        {
            this.format = format;
            if (!this.IsNull)
            {
                this.DisplayValue = this.ComputeValue.ToString(format);
            }
        }

        public void SetValue(decimal? value)
        {
            if (value == null)
            {
                this.IsNull = true;
                this.DisplayValue = "-";
                this.ComputeValue = 0;
            }
            else
            {
                this.IsNull = false;
                this.DisplayValue = ((decimal)value).ToString(format);
                this.ComputeValue = (decimal)value;
            }
        }
    }
}
