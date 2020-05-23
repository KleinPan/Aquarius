using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

using Aquarius.Base;
using Aquarius.Models;

using AquariusInterfaces;

using Autofac;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aquarius.ViewModels
{
    public class PluginInfoViewModel : BaseViewModel
    {

        public ReactiveCommand<Unit, Unit> InstallCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> UninstallCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> EditCommand { get; private set; }

        [Reactive]
        public PluginInfo pluginInfo { get; set; }

        public PluginInfoViewModel(PluginInfo pluginInfo)
        {
            this.pluginInfo = pluginInfo;

            InstallCommand = ReactiveCommand.Create(InstallEvent);
            RefreshCommand = ReactiveCommand.Create(RefreshEvent);
            UninstallCommand = ReactiveCommand.Create(UninstallEvent);
            EditCommand = ReactiveCommand.Create(EditEvent);


        }

        private void EditEvent()
        {
            throw new NotImplementedException();
        }

        private void UninstallEvent()
        {
            throw new NotImplementedException();
        }

        private void RefreshEvent()
        {
            throw new NotImplementedException();
        }

        private void InstallEvent()
        {
            IEnumerable<IPlugin> plugArray;
            IPlugin plugin;

            using (var scope = AppBootstrapper.Container.BeginLifetimeScope())
            {
                plugArray = scope.Resolve<IEnumerable<IPlugin>>();
                plugin = plugArray.FirstOrDefault(x => x.Name == pluginInfo.Name);
            }

            plugin.Show();
        }
    }
}