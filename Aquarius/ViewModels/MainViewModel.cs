
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
        #region ����

        [Reactive]
        public bool ContextMenuIsShow { get; set; } = true;

        [Reactive]
        public bool ContextMenuIsBlink { get; set; }

        [Reactive]
        public string IcoToolTip { get; set; }

        [Reactive]
        public float Progress { get; set; }

        #endregion ����

        #region ����

        public ReactiveCommand<Unit, Unit> OpenSettingCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> CheckUpdateCommand { get; private set; }

        #endregion ����

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
            string url1 = "https://www.rlvision.com/files/Snap2HTML.zip";
            HttpClientHelper.NotifyProgress += ShowProgress;
            HttpClientHelper.Download(url1, @"E:\���ػ���\ת��Ĭ��Ŀ¼", "123.zip");
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