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
        Windows.Storage.Streams.IRandomAccessStream fileStream;
        byte[] sourcePixels;
        int w, h;
        lab01biometria.image_RGB ImageLeft;
        lab01biometria.image_RGB ImageRight;
        private async System.Threading.Tasks.Task open_Click(object sender, RoutedEventArgs e)
        {
            Windows.Graphics.Imaging.BitmapDecoder decoder;
            Guid decoderId;

            ///Windows.Storage.Streams.IRandomAccessStream fileStream; // Wczytanie pliku do strumienia



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
                //Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
                //bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
                //this.show.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
                // Poniżej znajduje się zapamiętanie dekodera
                
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
                //w = bitmapImage.PixelWidth;
                //h = bitmapImage.PixelHeight;


               

            }
           

        }
        private async void open_ClickLeft(object sender, RoutedEventArgs e)
        {
            await open_Click(sender, e);
            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
            bitmapImage.SetSource(fileStream);
            w = bitmapImage.PixelWidth;
            h = bitmapImage.PixelHeight;
            sourcePixels = pixelData.DetachPixelData();
            ImageLeft = new lab01biometria.image_RGB(sourcePixels, w, h);
            try
            {
                Anaglyphy();
            }
            catch (NullReferenceException ex)
            {
                this.Warning.Text = "Choose right image";
            }

        }
        private async void open_ClickRight(object sender, RoutedEventArgs e)
        {
            await open_Click(sender, e);
            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
            bitmapImage.SetSource(fileStream);
            w = bitmapImage.PixelWidth;
            h = bitmapImage.PixelHeight;
            sourcePixels = pixelData.DetachPixelData();
            ImageRight = new lab01biometria.image_RGB(sourcePixels, w, h);
            try
            {
                Anaglyphy();
            }
            catch (NullReferenceException ex)
            {
                this.Warning.Text = "Choose left image";
            }

        }
        private void Anaglyphy()
        {
            if (ImageRight.w == ImageLeft.w && ImageRight.h == ImageLeft.h)
            {
                var rightcopy = ImageRight.copy();
                var leftcopy = ImageLeft.copy();

                Anaglyfy.Anaglifyoperation TruevisitLeft = new Anaglyfy.TrueAnaglyphsLeft();
                Anaglyfy.Anaglifyoperation TruevisitRight = new Anaglyfy.TrueAnaglyphsRight();
                lab01biometria.image_RGB Trueright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Trueleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);


                Anaglyfy.Anaglifyoperation ColorvisitLeft = new Anaglyfy.ColorAnaglyphsLeft();
                Anaglyfy.Anaglifyoperation ColorvisitRight = new Anaglyfy.ColorAnaglyphsRight();
                lab01biometria.image_RGB Colorright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Colorleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);

                Anaglyfy.Anaglifyoperation GreyvisitLeft = new Anaglyfy.GrayAnaglyphsLeft();
                Anaglyfy.Anaglifyoperation GreyvisitRight = new Anaglyfy.GrayAnaglyphsRight();
                lab01biometria.image_RGB Greyright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Greyleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);


                Anaglyfy.Anaglifyoperation HalfvisitLeft = new Anaglyfy.HalfColorAnaglyphsLeft();
                Anaglyfy.Anaglifyoperation HalfvisitRight = new Anaglyfy.HalfColorAnaglyphsRight();
                lab01biometria.image_RGB Halfright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Halfleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);


                Anaglyfy.Anaglifyoperation OptimazevisitLeft = new Anaglyfy.OptimizedAnaglyphsLeft();
                Anaglyfy.Anaglifyoperation OptimazevisitRight = new Anaglyfy.OptimizedAnaglyphsRight();
                lab01biometria.image_RGB Optimazeright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Optimazeleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);

                Anaglyfy.Anaglifyoperation DuboisvisitLeft = new Anaglyfy.DuboisLeft();
                Anaglyfy.Anaglifyoperation DuboisvisitRight = new Anaglyfy.DuboisRight();
                lab01biometria.image_RGB Duboisright = new lab01biometria.image_RGB(rightcopy.utab, rightcopy.w, rightcopy.h);
                lab01biometria.image_RGB Duboiszeleft = new lab01biometria.image_RGB(leftcopy.utab, leftcopy.w, leftcopy.h);


                TruevisitLeft.rob(Trueleft);
                TruevisitRight.rob(Trueright);
                var image3D = TruevisitLeft.addImagesAnaglfy(Trueleft, Trueright);
                bitmpe(image3D, TrueAnaglyphs);

                ColorvisitLeft.rob(Colorleft);
                ColorvisitRight.rob(Colorright);
                image3D = TruevisitLeft.addImagesAnaglfy(Colorleft, Colorright);
                bitmpe(image3D, ColorAnaglyphs);

                GreyvisitLeft.rob(Greyleft);
                GreyvisitRight.rob(Greyright);
                image3D = TruevisitLeft.addImagesAnaglfy(Greyleft, Greyright);
                bitmpe(image3D, GeryAnaglyphs);

                HalfvisitLeft.rob(Halfleft);
                HalfvisitRight.rob(Halfright);
                image3D = TruevisitLeft.addImagesAnaglfy(Halfleft, Halfright);
                bitmpe(image3D, HalfColorAnaglyphs);

                OptimazevisitLeft.rob(Optimazeleft);
                OptimazevisitRight.rob(Optimazeright);
                image3D = TruevisitLeft.addImagesAnaglfy(Optimazeleft, Optimazeright);
                bitmpe(image3D, OptimizedAnaglyphs);

                OptimazevisitLeft.rob(Duboiszeleft);
                OptimazevisitRight.rob(Duboisright);
                image3D = TruevisitLeft.addImagesAnaglfy(Duboiszeleft, Duboisright);


                bitmpe(image3D, DuboisAnaglyphs);



                this.Warning.Text = "";
            }
            else
            {
                this.Warning.Text = "Images are wrong!!!! load again";
            }
            
         

        }
        private async void bitmpe(lab01biometria.image_as_tab obiekt,Image image )
        {
            Windows.UI.Xaml.Media.Imaging.WriteableBitmap writeableBitmap = new Windows.UI.Xaml.Media.Imaging.WriteableBitmap((int)obiekt.w, (int)obiekt.h);

            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(obiekt.show(), 0, obiekt.show().Length);
            }
            image.Source = writeableBitmap;


        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
