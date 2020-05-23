using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Controls;
using Aquarius.Base;
using Aquarius.Models;
using AquariusInterfaces;
using Autofac;
using HandyControl.Controls;
using HandyControl.Data;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aquarius.ViewModels
{

    
    internal class SettingWindowViewModel : BaseViewModel
    {
        public ReactiveCommand<object, Unit> SelectCommand { get; private set; }

        
        public ReactiveCommand<object, Unit> PluginsSwitchItemCmd { get; private set; }

        public ReactiveCommand<FunctionEventArgs<object>, Unit> SwitchItemCmd { get; private set; }

        public ReactiveCommand<Unit, Unit> ScanPluginsCommand { get; private set; }
       
        /// <summary>
        /// 插件列表
        /// </summary>
        [Reactive]
        public ObservableCollection<PluginInfo> OCPlugIns { get; set; } 
        [Reactive]
        public object  CContent { get; set; }

        [Reactive]
        public PluginInfoViewModel CurrentPluginInfoVM { get; set; }
        public SettingWindowViewModel()
        {
            SelectCommand = ReactiveCommand.Create<object>(SelectEvent);
            SwitchItemCmd = ReactiveCommand.Create<FunctionEventArgs<object>>(SwitchItemEvent);
            PluginsSwitchItemCmd = ReactiveCommand.Create<object>(PluginsSwitchItemEvent);
            ScanPluginsCommand = ReactiveCommand.Create(ScanPluginsEvent);
           
        }

      

        private void PluginsSwitchItemEvent(object obj)
        {
            var temp = obj as SelectionChangedEventArgs;

            if (temp.AddedItems.Count==0)
            {
                return;
            }
            var item = temp.AddedItems[0] as PluginInfo;

            item.AssemblyInfo = new Helper.AssemblyInfoHelper();
            CurrentPluginInfoVM = new PluginInfoViewModel(item);
        }

        private void ScanPluginsEvent()
        {

            OCPlugIns = new ObservableCollection<PluginInfo>();
            IEnumerable<IPlugin> userBLL;

            using (var scope = AppBootstrapper.Container.BeginLifetimeScope())
            {
                userBLL = scope.Resolve<IEnumerable<IPlugin>>();
            }
            foreach (var item in userBLL)
            {
                PluginInfo pluginInfo = new PluginInfo();
                pluginInfo.ID = OCPlugIns.Count;
                pluginInfo.Name = item.Name;
                pluginInfo.Author = item.Author;
                pluginInfo.Description = item.Description;
               // pluginInfo.AssemblyInfo = item.Name;

                OCPlugIns.Add(pluginInfo);
            }
        }

        private void SwitchItemEvent(FunctionEventArgs<object> obj)
        {
            var title = (obj.Info as SideMenuItem)?.Header.ToString();
            switch (title)
            {
                case "常规设置":
                    CContent = new GeneralSettingViewModel();

                    break;

                default:
                    break;
            }
        }

        private void SelectEvent(object obj)
        {
        }
    }
}