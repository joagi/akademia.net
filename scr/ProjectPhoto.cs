using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfApplication
{
    public enum changes { cmyk32, gray, blackwhite, Rgba64 };

    interface IProjectPhoto
    {
        FormatConvertedBitmap format { get; set; }
        List<Layer> layers { get; set; }
        void Add(Bitmap bm);
        void changePhoto();
    }

    class ProjectPhoto : IProjectPhoto
    {
        private Bitmap image;
        public FormatConvertedBitmap format { get; set; }
        public List<Layer> layers { get; set; }

        public ProjectPhoto()
        {
            layers = new List<Layer>();
        }

        public ProjectPhoto(Bitmap image)
        {
            this.image = image;
            layers = new List<Layer>();
            Add(image);
            this.format = layers[0].format;           
        }

        public void Add(Bitmap bm)
        {
            Layer newLayer = new Layer(bm);
            layers.Insert(0, newLayer);
        }

        public void Add()
        {
            Layer newLayer = new Layer(image);
            layers.Insert(0, newLayer);
        }

        public void changePhoto()
        {
            foreach (Layer layer in layers)
            {
                if (layer.active.Equals(true))
                {
                    format = layer.format;
                    image = layer.LayerBitmap;
                    break;
                }
            }
        }
    }
}
