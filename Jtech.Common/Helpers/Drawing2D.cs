using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Helpers
{
    public static class Drawing2D
    {
        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
            }
            return lResult;
        }

        public static async Task ResizeImage(string sourceImg, string destImg, Size sizeForResize)
        {
            var destRect = new Rectangle(0, 0, sizeForResize.Width, sizeForResize.Height);
            using (var destImage = new Bitmap(sizeForResize.Width, sizeForResize.Height))
            {
                string dir = Path.GetDirectoryName(destImg);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        using (Image image = Image.FromFile(sourceImg))
                            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                destImage.Save(destImg);
            }
            await Task.CompletedTask;
        }
    }
}
