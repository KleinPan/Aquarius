using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Text;
using Aquarius.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aquarius.ViewModels
{
    class GeneralSettingViewModel: BaseViewModel
    {
        [Reactive]
        public bool AutoStart { get; set; } = true;

        public ReactiveCommand<Unit, Unit> CheckUpdateCmd { get; private set; }


        public GeneralSettingViewModel()
        {
            CheckUpdateCmd = ReactiveCommand.Create(CheckUpdate);
        }

        private void CheckUpdate()
        {
            //string filepath = "";//文件得下载地址
            string filepath = "https://56992f2d790002eb5da84202376812c4.dlied1.cdntips.com/dlied1.qq.com/qqweb/PCQQ/PCQQ_EXE/PCQQ2020.exe?mkey=5ec8b5c7ca640553&f=0ae7&cip=202.100.35.166&proto=https&access_type=$header_ApolloNet";

            Process p = new Process();
            p.StartInfo.FileName = "AutoUpdate.exe";
            //filepaht = 下载文件的地址,  空格隔开的是 启动程序的完整路径
            p.StartInfo.Arguments = filepath + " " + System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            p.Start();
        }
    }
}
