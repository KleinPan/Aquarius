using System;using System.Collections.Generic;using System.Text;using Aquarius.Helper;using ReactiveUI.Fody.Helpers;

namespace Aquarius.Models{   public class PluginInfo    {        public int ID { get; set; }        public string Name { get; set; }        [Reactive]        public string Author { get; set; }
        public string Description { get; set; }        public AssemblyInfoHelper AssemblyInfo { get; set; }    }}