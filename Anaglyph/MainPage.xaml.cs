using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Anaglyph
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        Windows.Graphics.Imaging.PixelDataProvider pixelData;
        byte[] sourcePixels;
        int w, h;
        lab01biometria.image_RGB ImageLeft;
        lab01biometria.image_RGB ImageRight;
        private async System.Threading.Tasks.Task open_Click(object sender, RoutedEventArgs e)
        {
            Windows.Graphics.Imaging.BitmapDecoder decoder;
            Guid decoderId;

            Windows.Storage.Streams.IRandomAccessStream fileStream; // Wczytanie pliku do strumienia



            //przyciskiEnabled();
            Windows.Storage.Pickers.FileOpenPicker FOP = new Windows.Storage.Pickers.FileOpenPicker(); // Klasa okna wybierania pliku
            FOP.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail; // Rodzaj podglądu plików w oknie - tu jako małe obrazy
            FOP.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary; // Od jakiego katalogu okno powinno zacząć wyświetlanie
            FOP.FileTypeFilter.Add(".bmp"); // filtry, które informują jakie rozszerzenia można wybrać
            FOP.FileTypeFilter.Add(".jpg");
            FOP.FileTypeFilter.Add(".jpeg");
            FOP.FileTypeFilter.Add(".png");
            FOP.FileTypeFilter.Add(".gif");
            Windows.Storage.StorageFile file = await FOP.PickSingleFileAsync();
            // Uruchomienie wybierania pliku pojedynczego

            if (file != null)
            {
                //przyciskiVisible();

                fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                // Dekoder będzie potrzebny później przy pracy na obrazie
                Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
                bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
                //this.show.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
                // Poniżej znajduje się zapamiętanie dekodera
                w = bitmapImage.PixelWidth;
                h = bitmapImage.PixelHeight;

                switch (file.FileType.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        decoderId = Windows.Graphics.Imaging.BitmapDecoder.JpegDecoderId;
                        break;
                    case ".bmp":
                        decoderId = Windows.Graphics.Imaging.BitmapDecoder.BmpDecoderId;
                        break;
                    case ".png":
                        decoderId = Windows.Graphics.Imaging.BitmapDecoder.PngDecoderId;
                        break;
                    case ".gif":
                        decoderId = Windows.Graphics.Imaging.BitmapDecoder.GifDecoderId;
                        break;
                    default:
                        return;
                }

                decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(decoderId, fileStream); // Dekodowanie strumienia za pomocą dekodera
                // Dekodowanie strumienia do klasy z informacjami o jego parametrach
                pixelData = await decoder.GetPixelDataAsync(
                Windows.Graphics.Imaging.BitmapPixelFormat.Bgra8,// Warto tu zwrócić uwagę jak przechowywane są kolory!!!
                Windows.Graphics.Imaging.BitmapAlphaMode.Straight,
                new Windows.Graphics.Imaging.BitmapTransform(),
                Windows.Graphics.Imaging.ExifOrientationMode.IgnoreExifOrientation,
                Windows.Graphics.Imaging.ColorManagementMode.DoNotColorManage
                );


                //v = new lab01biometria.imageoperation.Otsu();
                //v.rob(ImageToWork);
                //bitmpe(ImageToWork);

            }
           

        }
        private async void open_ClickLeft(object sender, RoutedEventArgs e)
        {
            await open_Click(sender, e);
            sourcePixels = pixelData.DetachPixelData();
            ImageLeft = new lab01biometria.image_RGB(sourcePixels, w, h);

        }
        private async void open_ClickRight(object sender, RoutedEventArgs e)
        {
            await open_Click(sender, e);
            sourcePixels = pixelData.DetachPixelData();
            ImageRight = new lab01biometria.image_RGB(sourcePixels, w, h);

        }
        private void Anaglyphy()
        {
            Anaglyfy.Anaglifyoperation visitLeft = new Anaglyfy.TrueAnaglyphsRight();
            Anaglyfy.Anaglifyoperation visitRight = new Anaglyfy.TrueAnaglyphsLeft();
            visitLeft.rob(ImageLeft);
            visitRight.rob(ImageRight);
            var image3D = visitLeft.addImagesAnaglfy(ImageLeft, ImageRight);
            bitmpe(image3D);

        }
        private async void bitmpe(lab01biometria.image_as_tab obiekt)
        {
            Windows.UI.Xaml.Media.Imaging.WriteableBitmap writeableBitmap = new Windows.UI.Xaml.Media.Imaging.WriteableBitmap((int)obiekt.w, (int)obiekt.h);

            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(obiekt.show(), 0, obiekt.show().Length);
            }
            this.TrueAnaglyphs.Source = writeableBitmap;


        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            Anaglyphy();
        }
    }
}
