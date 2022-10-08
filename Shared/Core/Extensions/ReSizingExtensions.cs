using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System;

namespace Core.Extensions
{
    public class ReSizingExtensions
    {
        public class GetSize
        {
            public GetSize(Stream stream)
            {
                using (Image iOriginal = Image.Load(stream))
                {
                    stream.Position = 0;
                    Width = iOriginal.Width;
                    Height = iOriginal.Height;
                }
            }
            public int Width { get; }
            public int Height { get; }
        }

        #region SaveImage(Stream imageStream, string path, int width, int height, bool preserveImageRatio)
        public static void SaveImage(Stream imageStream, string path, int width, int height, bool preserveImageRatio)
        {
            using (Image iOriginal = Image.Load(imageStream))
            {
                imageStream.Position = 0;
                if (preserveImageRatio)
                {
                    float percentWidth = width / (float)iOriginal.Width;
                    float percentHeight = height / (float)iOriginal.Height;
                    float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                    width = (int)Math.Round(iOriginal.Width * percent, 0);
                    height = (int)Math.Round(iOriginal.Height * percent, 0);
                }
                resize(iOriginal, width, height, path);
            }
        }
        #endregion
        #region SaveImage(Stream imageStream, int newWidth, int newHeight, bool preserveImageRatio, Stream saveToStream)
        public static void SaveImage(Stream imageStream, int newWidth, int newHeight, bool preserveImageRatio, Stream saveToStream)
        {
            using (Image iOriginal = Image.Load(imageStream))
            {
                imageStream.Position = 0;
                if (preserveImageRatio)
                {
                    float percentWidth = newWidth / (float)iOriginal.Width;
                    float percentHeight = newHeight / (float)iOriginal.Height;
                    float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                    newWidth = (int)Math.Round(iOriginal.Width * percent, 0);
                    newHeight = (int)Math.Round(iOriginal.Height * percent, 0);
                }
                resize(imageStream, iOriginal, newWidth, newHeight, saveToStream);
            }
        }
        #endregion
        #region SaveImage(Stream imageStream, int newNumberOfPixels, Stream saveToStream)
        public static void SaveImage(Stream imageStream, int newNumberOfPixels, Stream saveToStream)
        {
            using (Image iOriginal = Image.Load(imageStream))
            {
                imageStream.Position = 0;
                double ratio = Math.Sqrt(newNumberOfPixels / (double)(iOriginal.Width * iOriginal.Height));
                resize(imageStream, iOriginal, (int)Math.Round(iOriginal.Width * ratio, 0), (int)Math.Round(iOriginal.Height * ratio, 0), saveToStream);
            }
        }
        #endregion
        #region resize(Stream origSource, Image image, int newWidth, int newHeight, Stream saveTo)
        private static void resize(Stream origSource, Image image, int newWidth, int newHeight, Stream saveTo)
        {
            image.Mutate(x => x.Resize(newWidth, newHeight));
            transformImage(image); // NOTE: transform image AFTER resizing it!!!
            image.Save(saveTo, Image.DetectFormat(origSource));
        }
        #endregion
        #region resize(Image image, int newWidth, int newHeight, string path)
        private static void resize(Image image, int newWidth, int newHeight, string path)
        {
            image.Mutate(x => x.Resize(newWidth, newHeight));
            transformImage(image); // NOTE: transform image AFTER resizing it!!!
            image.Save(path);
        }
        #endregion
        #region transformImage(Image image)
        private static void transformImage(Image image)
        {
            IExifValue exifOrientation = image.Metadata?.ExifProfile?.GetValue(ExifTag.Orientation);

            if (exifOrientation == null)
                return;

            RotateMode rotateMode;
            FlipMode flipMode;
            setRotateFlipMode(exifOrientation, out rotateMode, out flipMode);

            image.Mutate(x => x.RotateFlip(rotateMode, flipMode));
            image.Metadata.ExifProfile.SetValue(ExifTag.Orientation, (ushort)1);
        }
        #endregion
        #region setRotateFlipMode(IExifValue exifOrientation, out RotateMode rotateMode, out FlipMode flipMode)
        private static void setRotateFlipMode(IExifValue exifOrientation, out RotateMode rotateMode, out FlipMode flipMode)
        {
            var orientation = (ushort)exifOrientation.GetValue();

            switch (orientation)
            {
                case 2:
                    rotateMode = RotateMode.None;
                    flipMode = FlipMode.Horizontal;
                    break;
                case 3:
                    rotateMode = RotateMode.Rotate180;
                    flipMode = FlipMode.None;
                    break;
                case 4:
                    rotateMode = RotateMode.Rotate180;
                    flipMode = FlipMode.Horizontal;
                    break;
                case 5:
                    rotateMode = RotateMode.Rotate90;
                    flipMode = FlipMode.Horizontal;
                    break;
                case 6:
                    rotateMode = RotateMode.Rotate90;
                    flipMode = FlipMode.None;
                    break;
                case 7:
                    rotateMode = RotateMode.Rotate90;
                    flipMode = FlipMode.Vertical;
                    break;
                case 8:
                    rotateMode = RotateMode.Rotate270;
                    flipMode = FlipMode.None;
                    break;
                default:
                    rotateMode = RotateMode.None;
                    flipMode = FlipMode.None;
                    break;
            }
        }
        #endregion

    }
}
