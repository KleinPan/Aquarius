
using System.Diagnostics;
using System.Reactive;

using Aquarius.Base;
using Aquarius.Helper;
using Aquarius.Views;

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
        }

       
        void ShowProgress(float x)
        {
            Progress = x;
            System.Diagnostics.Debug.WriteLine(x);


            System.Diagnostics.Debug.WriteLine(x);


        }
        private void OpenSettingEvent()
        {
            SettingWindowViewModel settingWindowViewModel = new SettingWindowViewModel();

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.DataContext = settingWindowViewModel;
            settingsWindow.ShowDialog();
        }
    }
}