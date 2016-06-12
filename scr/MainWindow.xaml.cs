using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace WpfApplication
{
    public partial class MainWindow : Window
    {

        ProjectPhoto transformImage;

        public MainWindow()
        {
            InitializeComponent();

            this.colorList.ItemsSource = Enum.GetValues(typeof(changes));
            this.colorList.SelectedIndex = 0;
            this.LayersList.SelectedIndex = 0;
        }

        private void ButtonLoadClick(object sender, RoutedEventArgs e)
        {
            Bitmap image = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Open Image";
                openFileDialog.Filter = "Image File|*.bmp; *.gif; *.jpg; *.jpeg; *.png;";
                if (openFileDialog.ShowDialog() == true)
                {
                    image = new Bitmap(openFileDialog.FileName);
                }

                transformImage = new ProjectPhoto(image);
                foreach (var item in transformImage.layers)
                {
                    LayersList.Items.Add(item);
                }
                RefreshLayers();
            }
            catch (Exception ex) { }
        }


        private void ButtonAddLayerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                transformImage.Add();
                LayersList.Items.Clear();
                foreach (var item in transformImage.layers)
                {
                    LayersList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load image");
            }
        }

        private void RefreshLayers()
        {
            imageView.Source = new BitmapImage();
            LayersList.Items.Clear();
            foreach (var item in transformImage.layers)
            {
                LayersList.Items.Add(item);
            }
            imageView.Source = transformImage.format;
        }

        private void changePhoto(object sender, RoutedEventArgs e)
        {
            transformImage.changePhoto();
            RefreshLayers();
        }


        private void TransformLayer(object sender, RoutedEventArgs e)
        {
            try
            {
                transformImage.layers[LayersList.SelectedIndex].Transform((changes)Enum.Parse(typeof(changes), this.colorList.Text));
                transformImage.changePhoto();
                RefreshLayers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Choose layer");
            }
        }
    }
}
