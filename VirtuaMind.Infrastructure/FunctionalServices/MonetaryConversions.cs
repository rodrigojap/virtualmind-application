namespace VirtuaMind.Infrastructure.FunctionalServices
{
    public static class MonetaryConversions
    {
        public static string ConvertUSDToBRL(this string value)
        {
            decimal resultCast;
            decimal.TryParse(value, out resultCast);

            var conversionResult = resultCast / 4;

            return conversionResult.ToString();
        }
    }
}
