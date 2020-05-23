using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

using ICSharpCode.SharpZipLib.Zip;

namespace AutoUpdate
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
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
            this.btn_Update.IsEnabled = false;
            this.isDownLoading = true;
            await downloadFile();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateView_Loaded(object sender, RoutedEventArgs e)
        {
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
        }

        private void UpdateView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.isDownLoading)
            {
                e.Cancel = true;
            }
        }

        /// <summary> 下载文件 </summary>
        /// <returns> </returns>
        private async Task downloadFile()
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1, filePath.Length - filePath.LastIndexOf('/') - 1);
                FileStream fs = new FileStream(directory + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(filePath);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                byte[] mybyte = new byte[128 * 200];//128byte = 1kb 200kb/s
                int count = stream.Read(mybyte, 0, mybyte.Length);
                currentLength += count;
                fs.Seek(0, SeekOrigin.Begin);

                while (count > 0)
                {
                    fs.Write(mybyte, 0, count);
                    count = stream.Read(mybyte, 0, mybyte.Length);
                    currentLength += count;
                    await Task.Delay(1);
                    this.Dispatcher.Invoke(() =>
                    {
                        long update_length = (100 * currentLength) / contentLength;
                        this.prob.Value = Convert.ToInt32(update_length);
                        string size = string.Empty;
                        if (currentLength > 1024 * 1024)
                        {
                            size = currentLength / (1024 * 1024) + "MB";
                        }
                        else if (currentLength > 1024)
                        {
                            size = currentLength / 1024 + "KB";
                        }
                        else
                        {
                            size = currentLength + "B";
                        }
                        this.lbl_currentSize.Text = size;
                    });
                }
                stream.Close();
                fs.Close();

                if (System.IO.Path.GetExtension(fileName).Equals(".zip"))
                {
                    this.unzipFiles(directory + fileName);
                    File.Delete(directory + fileName);
                    Directory.Delete(directory, true);
                }
                await Task.Delay(500);
                this.isDownLoading = false;
                this.Dispatcher.Invoke(() =>
                {
                    if (startPath != "")
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = this.startPath;
                        p.Start();
                    }
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                this.isDownLoading = false;
                MessageBox.Show(ex.Message, "提示信息");
                this.Close();
            }
        }

        /// <summary> 解压文件 </summary>
        /// <param name="zipFilePath"> </param>
        private void unzipFiles(string zipFilePath)
        {
            if (!File.Exists(zipFilePath))
            {
                Console.WriteLine("Cannot find file '{0}'", zipFilePath);
                return;
            }

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    Console.WriteLine(theEntry.Name);

                    string directoryName = System.IO.Path.GetDirectoryName(theEntry.Name);
                    string fileName = System.IO.Path.GetFileName(theEntry.Name);

                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    if (fileName != String.Empty)
                    {
                        try
                        {
                            using (FileStream streamWriter = File.Create(theEntry.Name))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}