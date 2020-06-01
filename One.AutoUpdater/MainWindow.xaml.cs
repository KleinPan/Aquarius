using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using One.AutoUpdater.UpdateEventArgs;
using One.AutoUpdater.Utilities;


namespace One.AutoUpdater
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {

        private MyWebClient _webClient;

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        private string _tempFile;
        private DateTime _startedAt;

        private string filePath = string.Empty; //下载文件路径

        private bool isDownLoading = false; //是否正在下载标志

        //public bool result = false;
        public MainWindow(UpdateInfoEventArgs args)
        {
            InitializeComponent();
            filePath = args.DownloadURL;

            Loaded += UpdateView_Loaded;
     
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_Update.Click += Btn_Update_Click;
            Closing += MainWindow_Closing;

            txbTip.Text = $@"有新版本{args.CurrentVersion}可用，当前使用版本为{ args.InstalledVersion}";
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (_webClient == null)
            {
                
            }
            else if (_webClient.IsBusy)
            {
                _webClient.CancelAsync();
               
            }
            else
            {
                
            }
           // DialogResult = false;
        }

        private async void Btn_Update_Click(object sender, RoutedEventArgs e)
        {






            this.btn_Update.IsEnabled = false;
            this.isDownLoading = true;
            //await downloadFile();


            await DownLoadWithFTP();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void UpdateView_Loaded(object sender, RoutedEventArgs e)
        {
#if HTTP

            directory = AppDomain.CurrentDomain.BaseDirectory + Guid.NewGuid().ToString() + "\\";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(filePath);
            WebResponse response = request.GetResponse();
            contentLength = Convert.ToInt64(response.Headers["Content-Length"]);
            string size = string.Empty;
            if (contentLength > 1024 * 1024)
            {
                size = contentLength / (1024 * 1024) + "MB";
            }
            else if (contentLength > 1024)
            {
                size = contentLength / 1024 + "KB";
            }
            else
            {
                size = contentLength + "B";
            }
            this.lbl_size.Text = size;
#endif

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePath);
            request.Method = WebRequestMethods.Ftp.GetFileSize;



            request.Credentials = AutoUpdater.FtpCredentials;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            bytes_total = response.ContentLength; //这是一个int成员变量，用于以后存储
                                                  // Console.WriteLine（Fetch Complete，ContentLength { 0}，response.ContentLength）;
            response.Close();

            lbl_size.Text = BytesToString(bytes_total);
        }

        private async Task DownLoadWithFTP()
        {
            var uri = new Uri(filePath);

            _webClient = AutoUpdater.GetWebClient(uri, AutoUpdater.BasicAuthDownload);

            if (string.IsNullOrEmpty(AutoUpdater.DownloadPath))
            {
                _tempFile = Path.GetTempFileName();
            }
            else
            {
                _tempFile = Path.Combine(AutoUpdater.DownloadPath, $"{Guid.NewGuid().ToString()}.tmp");
                if (!Directory.Exists(AutoUpdater.DownloadPath))
                {
                    Directory.CreateDirectory(AutoUpdater.DownloadPath);
                }
            }


           


            _webClient.DownloadProgressChanged += OnDownloadProgressChanged;

            _webClient.DownloadFileCompleted += WebClientOnDownloadFileCompleted;


          
            _webClient.DownloadFileAsync(uri, _tempFile);



        }
        long bytes_total=-1;
        private void WebClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            this.isDownLoading = false;
         this.   DialogResult = true;

            this.Close();
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (_startedAt == default(DateTime))
            {
                _startedAt = DateTime.Now;
            }
            else
            {
                var timeSpan = DateTime.Now - _startedAt;
                long totalSeconds = (long)timeSpan.TotalSeconds;
                if (totalSeconds > 0)
                {
                    var bytesPerSecond = e.BytesReceived / totalSeconds;
                    lbl_speed.Text = BytesToString(bytesPerSecond)+"/s";
                }
            }



            //lbl_size.Text = BytesToString(e.TotalBytesToReceive);
            lbl_currentSize.Text = $@"{BytesToString(e.BytesReceived)}";
            //prob.Value = e.ProgressPercentage;

            double temp = (double)(e.BytesReceived) / (bytes_total);
           
            prob.Value = Math.Round(temp * 100,2);
        }



        private static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{(Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture)} {suf[place]}";
        }


       


    }

  
}