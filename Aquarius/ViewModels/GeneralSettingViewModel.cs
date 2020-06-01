using System.Reactive;

using Aquarius.Base;

using Newtonsoft.Json;

using One.AutoUpdater;
using One.AutoUpdater.UpdateEventArgs;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aquarius.ViewModels
{
    internal class GeneralSettingViewModel : BaseViewModel
    {
        [Reactive]
        public bool AutoStart { get; set; } = true;

        public ReactiveCommand<Unit, Unit> CheckUpdateCmd { get; private set; }

        public GeneralSettingViewModel()
        {
            CheckUpdateCmd = ReactiveCommand.Create(CheckUpdate);

            AutoUpdater.DownloadPath = Configs.Paths.downloadPath;
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            //AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;//没有执行
        }

        private void CheckUpdate()
        {
            AutoUpdater.Start("ftp://117.33.179.181//Version.json", new System.Net.NetworkCredential("FtpTest", "123456"));
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