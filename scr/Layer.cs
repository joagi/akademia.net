using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication
{
    class Layer : Photo
    {
        public bool active { get; set; }
        public FormatConvertedBitmap format { get; set; }

        public Layer(Bitmap bm)
        {
            this.active = true;
            this.LayerBitmap = bm;
            Transform();
        }

        public Layer(int w, int h)
        {
            Bitmap tmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(System.Drawing.Color.Transparent);
        }

        public void Transform(changes color = changes.Rgba64)
        {
            BitmapImage LayerBitmapImage = base.toBitmapImage();

            format = new FormatConvertedBitmap();
            format.BeginInit();
            format.Source = LayerBitmapImage;
            switch (color)
            {
                case changes.cmyk32:
                    format.DestinationFormat = PixelFormats.Cmyk32;
                    break;
                case changes.blackwhite:
                    format.DestinationFormat = PixelFormats.BlackWhite;
                    break;
                case changes.gray:
                    format.DestinationFormat = PixelFormats.Gray32Float;
                    break;
                default:
                    format.DestinationFormat = PixelFormats.Rgba64;
                    break;
            }
            format.EndInit();
        }
    }
}
