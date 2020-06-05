
using System.Diagnostics;
using System.Reactive;

using Aquarius.Base;
using Aquarius.Helper;
using Aquarius.Views;
using Newtonsoft.Json;
using One.AutoUpdater;
using One.AutoUpdater.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aquarius.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region 属性

        [Reactive]
        public bool ContextMenuIsShow { get; set; } = true;

        [Reactive]
        public bool ContextMenuIsBlink { get; set; }

        [Reactive]
        public string IcoToolTip { get; set; }

        [Reactive]
        public float Progress { get; set; }

        #endregion 属性

        #region 命令

        public ReactiveCommand<Unit, Unit> OpenSettingCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> CheckUpdateCommand { get; private set; }

        #endregion 命令

        public MainViewModel()
        {
            GetAssemblyInfo();

            InitCommand();


            AutoUpdater.DownloadPath = Configs.PathConfig.downloadPath;
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            //AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
        }

        private void GetAssemblyInfo()
        {
            AssemblyInfoHelper AssemblyInfo = new AssemblyInfoHelper();

            IcoToolTip = AssemblyInfo.TitleInfo.Title + " " + AssemblyInfo.VersionInfo.Version;
        }

        private void InitCommand()
        {
            OpenSettingCommand = ReactiveCommand.Create(OpenSettingEvent);
            CheckUpdateCommand = ReactiveCommand.Create(CheckUpdateEvent);
        }

        private void CheckUpdateEvent()
        {
            //string url1 = "https://56992f2d790002eb5da84202376812c4.dlied1.cdntips.com/dlied1.qq.com/qqweb/PCQQ/PCQQ_EXE/PCQQ2020.exe?mkey=5ec8b5c7ca640553&f=0ae7&cip=202.100.35.166&proto=https&access_type=$header_ApolloNet";
            //HttpClientHelper.NotifyProgress += ShowProgress;
            //HttpClientHelper.Download(url1, @"E:\下载汇总\转存默认目录", "123.zip");

            AutoUpdater.Start("ftp://117.33.179.181//Version.json", new System.Net.NetworkCredential("FtpTest", "123456"));
        }

       
        void ShowProgress(float x)
        {
            Progress = x;
            System.Diagnostics.Debug.WriteLine(x);



        }
        private void OpenSettingEvent()
        {
            SettingWindowViewModel settingWindowViewModel = new SettingWindowViewModel();

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.DataContext = settingWindowViewModel;
            settingsWindow.ShowDialog();
        }

        #region AutoUpdate第三方

        // UpdateInfoEventArgs json;
        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.version,
                ChangelogURL = json.changelog,
                DownloadURL = json.url,
                Mandatory = new Mandatory
                {
                    Value = json.mandatory.value,

                    MinimumVersion = json.mandatory.minVersion
                },
                CheckSum = new CheckSum
                {
                    Value = json.checksum.value,
                    HashingAlgorithm = json.checksum.hashingAlgorithm
                }
            };
        }

        private void AutoUpdater_ApplicationExitEvent()
        {
            //Text = @"Closing application...";
            //Thread.Sleep(2000);
            App.Current.Shutdown();
        }

        #endregion AutoUpdate第三方
    }
}