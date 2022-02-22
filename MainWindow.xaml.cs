using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace garden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<SnapShot> lstp = new List<SnapShot>();
        private int ImageNumber = 0;
        private DispatcherTimer PictureTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //var ft = GetPics();
          //lstp = sortPics("C:\\Users\\suzco\\Documents\\c#\\garden\\pics\\100MEDIA");
            lstp = sortPics("C:\\Users\\Asus\\Desktop\\Images150222\\GH010689 (2-15-2022 11-54-27 AM)-new");
            imgDynamic.Source = new BitmapImage(new Uri(lstp.ElementAt(1).FileName));
            //displayPics(lst);
            // Install a timer to show each image.
            PictureTimer.Interval = TimeSpan.FromSeconds(0.1);
            PictureTimer.Tick += Tick;
            PictureTimer.Start();
        }

        private void displayPics(List<SnapShot> lst)
        {
            foreach (var g in lst)
            {
                Uri fileUri = new Uri(g.FileName);
                BitmapImage image = new BitmapImage(fileUri);
                imgDynamic.Source = image;   
            }
        }

        private string GetPics()
        {
            string k = "";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result.ToString() == "OK" && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    k = fbd.SelectedPath;
                }
            }
            return k;
        }

        /*public async void recordd()
        {
            var file = await GetTempFileAsync();
            // Kick off the encoding
            try
            {
                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                using (_encoder = new Encoder(_device, item))
                {
                    await _encoder.EncodeAsync(
                        stream,
                        width, height, bitrate,
                        frameRate);
                }
                MainTextBlock.Foreground = originalBrush;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex);

                var message = GetMessageForHResult(ex.HResult);
                if (message == null)
                {
                    message = $"Uh-oh! Something went wrong!\n0x{ex.HResult:X8} - {ex.Message}";
                }
                var dialog = new MessageDialog(
                    message,
                    "Recording failed");

                await dialog.ShowAsync();

                
                return;
            }
        }

        private async Task<StorageFile> GetTempFileAsync()
        {
            var folder = ApplicationData.Current.TemporaryFolder;
            var name = DateTime.Now.ToString("yyyyMMdd-HHmm-ss");
            var file = await folder.CreateFileAsync($"{name}.mp4");
            return file;
        }
*/
        private List<SnapShot> sortPics(string flder) // "C:\\Users\\suzco\\Documents\\c#\\garden\\pics"
        {
            var listss = new List<SnapShot>();
            foreach (string fn in Directory.GetFiles(flder))
            {
                var gh = GetDate(new FileInfo(fn));
                if (gh.Hour <= 04 || gh.Hour >= 16) {
                    listss.Add(new SnapShot(fn, gh));
                }
            }
            return listss.OrderBy(o => o.TimeValue).ToList();
        }

        public DateTime GetDate(FileInfo f)
        {
            using (FileStream fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BitmapSource img = BitmapFrame.Create(fs);
                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                string date = md.DateTaken;
                //Console.WriteLine(date);
                return Convert.ToDateTime(date);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            displayPics(lstp);
        }

        // Display the next image.
        private void Tick(object sender, System.EventArgs e)
        {
            ImageNumber = (ImageNumber + 1) % lstp.Count;
            ShowNextImage(imgDynamic);
        }

        private void ShowNextImage(Image img)
        {
            imgDynamic.Source = new BitmapImage(new Uri(lstp.ElementAt(ImageNumber).FileName));
        }
    }
}
