namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class DateExtensions
    {
        public static string GetTimestamp(DateTime value)
        {
            return new DateTimeOffset(value).ToUnixTimeSeconds().ToString();
        }


    }
}
