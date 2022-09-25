namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class PhotoExtensions
    {
        #region GetPhotoExtent
        public static string GetPhotoExtent(IFormFile photo)
        {
            var extent = "";
            for (int i = photo.FileName.Count() - 1; i >= 0; i--)
            {
                if (photo.FileName[i] == '.')
                {
                    extent = photo.FileName.Substring(i);
                }
            }
            return extent;
        }
        #endregion
    }
}
