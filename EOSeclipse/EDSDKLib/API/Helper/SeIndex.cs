using EOSDigital.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDSDKLib.API.Helper
{
    public class SeIndex
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }

        protected SeIndex()
        {
            StringValue = string.Empty;
            IntValue = unchecked((int)0xFFFFFFFF);
        }

        public SeIndex(string value)
            : this()
        {
            StringValue = value;
            IntValue = GetValue(value).IntValue;
        }

        public SeIndex(int value)
            : this()
        {
            IntValue = value;
            StringValue = GetValue(value).StringValue;
        }

        public SeIndex(string SValue, int Ivalue)
            : this()
        {
            IntValue = Ivalue;
            StringValue = SValue;
        }

        public static SeIndex GetValue(string value)
        {
            var values = SeIndices();
            return GetValue(value, values);
        }

        public static SeIndex GetValue(int value)
        {
            var values = SeIndices();
            return GetValue(value, values);
        }

        public static SeIndex GetValue(string value, List<SeIndex> Values)
        {
            var arr = Values.Where(t => t.StringValue == value).ToArray();
            if (arr.Length == 0)
            {
                var invalid = Values.FirstOrDefault(t => t.IntValue == unchecked((int)0xFFFFFFFF));
                if (invalid != null) return invalid;
                else throw new KeyNotFoundException("There is no SeIndex that matches this criteria");
            }
            else { return arr[0]; }
        }

        public static SeIndex GetValue(int value, List<SeIndex> Values)
        {
            var arr = Values.Where(t => t.IntValue == value).ToArray();
            if (arr.Length == 0)
            {
                var invalid = Values.FirstOrDefault(t => t.IntValue == unchecked((int)0xFFFFFFFF));
                if (invalid != null) return invalid;
                else throw new KeyNotFoundException("There is no SeIndex that matches this criteria");
            }
            else { return arr[0]; }
        }

        public static List<SeIndex> SeIndices()
        {
            List<SeIndex> values = new List<SeIndex>()
            {
                new SeIndex("1970 Mar 07 (T)", -65),
                new SeIndex("1970 Aug 31 (A)", -64),
                new SeIndex("1971 Feb 25 (P)", -63),
                new SeIndex("1971 Jul 22 (P)", -62),
                new SeIndex("1971 Aug 20 (P)", -61),
                new SeIndex("1972 Jan 16 (A)", -60),
                new SeIndex("1972 Jul 10 (T)", -59),
                new SeIndex("1973 Jan 04 (A)", -58),
                new SeIndex("1973 Jun 30 (T)", -57),
                new SeIndex("1973 Dec 24 (A)", -56),
                new SeIndex("1974 Jun 20 (T)", -55),
                new SeIndex("1974 Dec 13 (P)", -54),
                new SeIndex("1975 May 11 (P)", -53),
                new SeIndex("1975 Nov 03 (P)", -52),
                new SeIndex("1976 Apr 29 (A)", -51),
                new SeIndex("1976 Oct 23 (T)", -50),
                new SeIndex("1977 Apr 18 (A)", -49),
                new SeIndex("1977 Oct 12 (T)", -48),
                new SeIndex("1978 Apr 07 (P)", -47),
                new SeIndex("1978 Oct 02 (P)", -46),
                new SeIndex("1979 Feb 26 (T)", -45),
                new SeIndex("1979 Aug 22 (A)", -44),
                new SeIndex("1980 Feb 16 (T)", -43),
                new SeIndex("1980 Aug 10 (A)", -42),
                new SeIndex("1981 Feb 04 (A)", -41),
                new SeIndex("1981 Jul 31 (T)", -40),
                new SeIndex("1982 Jan 25 (P)", -39),
                new SeIndex("1982 Jun 21 (P)", -38),
                new SeIndex("1982 Jul 20 (P)", -37),
                new SeIndex("1982 Dec 15 (P)", -36),
                new SeIndex("1983 Jun 11 (T)", -35),
                new SeIndex("1983 Dec 04 (A)", -34),
                new SeIndex("1984 May 30 (A)", -33),
                new SeIndex("1984 Nov 22 (T)", -32),
                new SeIndex("1985 May 19 (P)", -31),
                new SeIndex("1985 Nov 12 (T)", -30),
                new SeIndex("1986 Apr 09 (P)", -29),
                new SeIndex("1986 Oct 03 (H)", -28),
                new SeIndex("1987 Mar 29 (H)", -27),
                new SeIndex("1987 Sep 23 (A)", -26),
                new SeIndex("1988 Mar 18 (T)", -25),
                new SeIndex("1988 Sep 11 (A)", -24),
                new SeIndex("1989 Mar 07 (P)", -23),
                new SeIndex("1989 Aug 31 (P)", -22),
                new SeIndex("1990 Jan 26 (A)", -21),
                new SeIndex("1990 Jul 22 (T)", -20),
                new SeIndex("1991 Jan 15 (A)", -19),
                new SeIndex("1991 Jul 11 (T)", -18),
                new SeIndex("1992 Jan 04 (A)", -17),
                new SeIndex("1992 Jun 30 (T)", -16),
                new SeIndex("1992 Dec 24 (P)", -15),
                new SeIndex("1993 May 21 (P)", -14),
                new SeIndex("1993 Nov 13 (P)", -13),
                new SeIndex("1994 May 10 (A)", -12),
                new SeIndex("1994 Nov 03 (T)", -11),
                new SeIndex("1995 Apr 29 (A)", -10),
                new SeIndex("1995 Oct 24 (T)", -9),
                new SeIndex("1996 Apr 17 (P)", -8),
                new SeIndex("1996 Oct 12 (P)", -7),
                new SeIndex("1997 Mar 09 (T)", -6),
                new SeIndex("1997 Sep 02 (P)", -5),
                new SeIndex("1998 Feb 26 (T)", -4),
                new SeIndex("1998 Aug 22 (A)", -3),
                new SeIndex("1999 Feb 16 (A)", -2),
                new SeIndex("1999 Aug 11 (T)", -1),
                new SeIndex("2000 Feb 05 (P)", 0),
                new SeIndex("2000 Jul 01 (P)", 1),
                new SeIndex("2000 Jul 31 (P)", 2),
                new SeIndex("2000 Dec 25 (P)", 3),
                new SeIndex("2001 Jun 21 (T)", 4),
                new SeIndex("2001 Dec 14 (A)", 5),
                new SeIndex("2002 Jun 10 (A)", 6),
                new SeIndex("2002 Dec 04 (T)", 7),
                new SeIndex("2003 May 31 (A)", 8),
                new SeIndex("2003 Nov 23 (T)", 9),
                new SeIndex("2004 Apr 19 (P)", 10),
                new SeIndex("2004 Oct 14 (P)", 11),
                new SeIndex("2005 Apr 08 (H)", 12),
                new SeIndex("2005 Oct 03 (A)", 13),
                new SeIndex("2006 Mar 29 (T)", 14),
                new SeIndex("2006 Sep 22 (A)", 15),
                new SeIndex("2007 Mar 19 (P)", 16),
                new SeIndex("2007 Sep 11 (P)", 17),
                new SeIndex("2008 Feb 07 (A)", 18),
                new SeIndex("2008 Aug 01 (T)", 19),
                new SeIndex("2009 Jan 26 (A)", 20),
                new SeIndex("2009 Jul 22 (T)", 21),
                new SeIndex("2010 Jan 15 (A)", 22),
                new SeIndex("2010 Jul 11 (T)", 23),
                new SeIndex("2011 Jan 04 (P)", 24),
                new SeIndex("2011 Jun 01 (P)", 25),
                new SeIndex("2011 Jul 01 (P)", 26),
                new SeIndex("2011 Nov 25 (P)", 27),
                new SeIndex("2012 May 20 (A)", 28),
                new SeIndex("2012 Nov 13 (T)", 29),
                new SeIndex("2013 May 10 (A)", 30),
                new SeIndex("2013 Nov 03 (H)", 31),
                new SeIndex("2014 Apr 29 (A)", 32),
                new SeIndex("2014 Oct 23 (P)", 33),
                new SeIndex("2015 Mar 20 (T)", 34),
                new SeIndex("2015 Sep 13 (P)", 35),
                new SeIndex("2016 Mar 09 (T)", 36),
                new SeIndex("2016 Sep 01 (A)", 37),
                new SeIndex("2017 Feb 26 (A)", 38),
                new SeIndex("2017 Aug 21 (T)", 39),
                new SeIndex("2018 Feb 15 (P)", 40),
                new SeIndex("2018 Jul 13 (P)", 41),
                new SeIndex("2018 Aug 11 (P)", 42),
                new SeIndex("2019 Jan 06 (P)", 43),
                new SeIndex("2019 Jul 02 (T)", 44),
                new SeIndex("2019 Dec 26 (A)", 45),
                new SeIndex("2020 Jun 21 (A)", 46),
                new SeIndex("2020 Dec 14 (T)", 47),
                new SeIndex("2021 Jun 10 (A)", 48),
                new SeIndex("2021 Dec 04 (T)", 49),
                new SeIndex("2022 Apr 30 (P)", 50),
                new SeIndex("2022 Oct 25 (P)", 51),
                new SeIndex("2023 Apr 20 (H)", 52),
                new SeIndex("2023 Oct 14 (A)", 53),
                new SeIndex("2024 Apr 08 (T)", 54),
                new SeIndex("2024 Oct 02 (A)", 55),
                new SeIndex("2025 Mar 29 (P)", 56),
                new SeIndex("2025 Sep 21 (P)", 57),
                new SeIndex("2026 Feb 17 (A)", 58),
                new SeIndex("2026 Aug 12 (T)", 59),
                new SeIndex("2027 Feb 06 (A)", 60),
                new SeIndex("2027 Aug 02 (T)", 61),
                new SeIndex("2028 Jan 26 (A)", 62),
                new SeIndex("2028 Jul 22 (T)", 63),
                new SeIndex("2029 Jan 14 (P)", 64),
                new SeIndex("2029 Jun 12 (P)", 65),
                new SeIndex("2029 Jul 11 (P)", 66),
                new SeIndex("2029 Dec 05 (P)", 67),
                new SeIndex("2030 Jun 01 (A)", 68),
                new SeIndex("2030 Nov 25 (T)", 69),
                new SeIndex("2031 May 21 (A)", 70),
                new SeIndex("2031 Nov 14 (H)", 71),
                new SeIndex("2032 May 09 (A)", 72),
                new SeIndex("2032 Nov 03 (P)", 73),
                new SeIndex("2033 Mar 30 (T)", 74),
                new SeIndex("2033 Sep 23 (P)", 75),
                new SeIndex("2034 Mar 20 (T)", 76),
                new SeIndex("2034 Sep 12 (A)", 77),
                new SeIndex("2035 Mar 09 (A)", 78),
                new SeIndex("2035 Sep 02 (T)", 79),
                new SeIndex("2036 Feb 27 (P)", 80),
                new SeIndex("2036 Jul 23 (P)", 81),
                new SeIndex("2036 Aug 21 (P)", 82),
                new SeIndex("2037 Jan 16 (P)", 83),
                new SeIndex("2037 Jul 13 (T)", 84),
                new SeIndex("2038 Jan 05 (A)", 85),
                new SeIndex("2038 Jul 02 (A)", 86),
                new SeIndex("2038 Dec 26 (T)", 87),
                new SeIndex("2039 Jun 21 (A)", 88),
                new SeIndex("2039 Dec 15 (T)", 89)
            };

            return values;
        }
    }
}
