namespace EC.Services.PhotoStockAPI.Entities.Options
{
    public class ResizeSetting
    {
        public List<ResizeData> Data { get; set; }
    }

    public class ResizeData
    {
        public int ResizeType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public enum ResizeTypeEnum
    {
        XS=1,
        S=2,
        M=3,
        L=4,
        XL=5
    }
}
