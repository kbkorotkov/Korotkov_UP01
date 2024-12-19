using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;

namespace Korotkov_UP01.Pages
{
    /// <summary>
    /// Логика взаимодействия для QRCodePage.xaml
    /// </summary>
    public partial class QRCodePage : Page
    {
        public QRCodePage()
        {
            InitializeComponent();
            GenerateQR("https://docs.google.com/forms/d/e/1FAIpQLScZn_nHOmaNA9eI9y7jBx8zIX-NGdBUZ7neO-aHET_AWcCAOA/viewform?usp=dialog");
        }

        private void GenerateQR(string content)
        {
            try
            {
                System.Drawing.Image img = null;
                BitmapImage bimg = new BitmapImage();
                using (var ms = new MemoryStream())
                {
                    BarcodeWriter writer;
                    writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                    writer.Options.Height = 120;
                    writer.Options.Width = 200;
                    writer.Options.PureBarcode = true;
                    img = writer.Write(content);
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Position = 0;
                    bimg.BeginInit();
                    bimg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bimg.CacheOption = BitmapCacheOption.OnLoad;
                    bimg.UriSource = null;
                    bimg.StreamSource = ms;
                    bimg.EndInit();
                    this.imgbarcode.Source = bimg;
                    this.tbkbarcodecontent.Text = content;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
