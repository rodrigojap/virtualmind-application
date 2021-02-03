using System;
using System.Globalization;

namespace VirtualMind.Application.Extensions
{
    public static class MonetaryConversions
    {
        public static string ConvertUSDToBRL(this string value)
        {
            var culture = new CultureInfo("en-US");

            var resultCast = value.ConvertStringToDecimal();

            var conversionResult = resultCast / 4;

            return conversionResult.ToString(culture);
        }        
    }
}
