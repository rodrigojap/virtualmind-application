using System;
using System.Globalization;

namespace VirtualMind.Application.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ConvertStringToDecimal(this string value)
        {
            var culture = new CultureInfo("en-US");
            decimal outPutValue;

            if (decimal.TryParse(s: value, style: NumberStyles.Float, result: out outPutValue, provider: culture))
            {
                return outPutValue;
            }
            else
            {
                Console.WriteLine($"Invalid Decimal Conversion with this value : [{value}]");
                return 0;
            }
        }
    }
}
