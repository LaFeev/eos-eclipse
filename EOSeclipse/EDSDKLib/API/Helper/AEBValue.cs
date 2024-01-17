using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOSDigital.API
{
    public class AEBValue
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }

        protected AEBValue()
        {
            StringValue = string.Empty;
            IntValue = unchecked((int)0xFFFFFFFF);
        }

        public AEBValue(string value)
            : this()
        {
            StringValue = value;
            IntValue = GetValue(value).IntValue;
        }

        public AEBValue(int value)
            : this()
        {
            IntValue = value;
            StringValue = GetValue(value).StringValue;
        }

        public AEBValue(string SValue, int Ivalue)
            : this()
        {
            IntValue = Ivalue;
            StringValue = SValue;
        }

        public static AEBValue GetValue(string value)
        {
            var values = AEBValues();
            return GetValue(value, values);
        }

        public static AEBValue GetValue(int value)
        {
            var values = AEBValues();
            return GetValue(value, values);
        }

        public static AEBValue GetValue(string value, List<AEBValue> Values)
        {
            var arr = Values.Where(t => t.StringValue == value).ToArray();
            if (arr.Length == 0)
            {
                var invalid = Values.FirstOrDefault(t => t.IntValue == unchecked((int)0xFFFFFFFF));
                if (invalid != null) return invalid;
                else throw new KeyNotFoundException("There is no AEBValue that matches this criteria");
            }
            else { return arr[0]; }
        }

        public static AEBValue GetValue(int value, List<AEBValue> Values)
        {
            var arr = Values.Where(t => t.IntValue == value).ToArray();
            if (arr.Length == 0)
            {
                var invalid = Values.FirstOrDefault(t => t.IntValue == unchecked((int)0xFFFFFFFF));
                if (invalid != null) return invalid;
                else throw new KeyNotFoundException("There is no AEBValue that matches this criteria");
            }
            else { return arr[0]; }
        }

        public static List<AEBValue> AEBValues()
        {
            List<AEBValue> values = new List<AEBValue>()
            {
                new AEBValue("1/3", 1),
                new AEBValue("2/3", 2),
                new AEBValue("1 stop", 3),
                new AEBValue("1-1/3", 4),
                new AEBValue("1-2/3", 5),
                new AEBValue("2 stop", 6),
                new AEBValue("2-1/3", 7),
                new AEBValue("2-2/3", 8),
                new AEBValue("3 stop", 9)
            };

            return values;
        }
    }
}
