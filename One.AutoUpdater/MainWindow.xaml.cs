using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using One.AutoUpdater.Utilities;


namespace One.AutoUpdater
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {

        private MyWebClient _webClient;
        private string _tempFile;
        private DateTime _startedAt;
        public MainWindow(string[] args)
        {
            InitializeComponent();
            if (args.Length > 1)
            {
                filePath = args[0].Trim();
                startPath = args[1].Trim();
            }

            Loaded += UpdateView_Loaded;
            Closing += UpdateView_Closing;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_Update.Click += Btn_Update_Click;
        }
        public MainWindow(string args)
        {
            InitializeComponent();
            filePath = args;

            Loaded += UpdateView_Loaded;
            Closing += UpdateView_Closing;
            btn_Cancel.Click += Btn_Cancel_Click;
            btn_Update.Click += Btn_Update_Click;
        }


        private string filePath = string.Empty; //下载文件路径
        private string startPath = string.Empty; //启动程序路径
        private long contentLength = 0;  //文件总长度
        private long currentLength = 0;  //当前下载长度
        private string directory = string.Empty; //创建临时文件夹
        private bool isDownLoading = false; //是否正在下载标志

        private async void Btn_Update_Click(object sender, RoutedEventArgs e)
        {


            AutoUpdater.Start("ftp://117.33.179.181//Version.json", new NetworkCredential("FtpTest", "123456"));



            this.btn_Update.IsEnabled = false;
            this.isDownLoading = true;
            //await downloadFile();


            await DownLoadWithFTP();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
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

        private void WebClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

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
                    lbl_speed.Text = BytesToString(bytesPerSecond);
                }
            }



            lbl_size.Text = BytesToString(e.TotalBytesToReceive);
            lbl_currentSize.Text = $@"{BytesToString(e.BytesReceived)}";
            prob.Value = e.ProgressPercentage;
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


        private void UpdateView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.isDownLoading)
            {
                e.Cancel = true;
            }
        }

       

     
    }
}