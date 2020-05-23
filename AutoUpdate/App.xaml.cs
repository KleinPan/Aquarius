using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutoUpdate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        string filepath = "https://download.visualstudio.microsoft.com/download/pr/40dff314-f6c2-4aeb-bfc7-7f89fc8d2b61/79b23dcc8727ab76b7df8872968475fe/dotnet-sdk-5.0.100-preview.4.20258.7-win-x64.exe";

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                base.OnStartup(e);
                new MainWindow(e.Args).ShowDialog();
            }

            base.OnStartup(e);
            new MainWindow(filepath).ShowDialog();

            this.Shutdown();
        }
    }
}
