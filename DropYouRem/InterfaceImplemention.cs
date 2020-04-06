using System;using System.Collections.Generic;using System.Text;using System.Windows.Controls;using AquariusInterfaces;using DropYouRem.Views;using HandyControl.Controls;namespace DropYouRem{    class InterfaceImplemention : IPlugin    {        public string Name => "∂™ƒ„¿◊ƒ∑";        public string Author => "BranPan";
        public string Description => "√Ë ˆ";

      

        public Control Show()        {            ViewModels.RemSpriteViewModel vm = new ViewModels.RemSpriteViewModel();            RemSpriteView view = new RemSpriteView()            {                DataContext = vm,            };            return Sprite.Show(view);           // return view;        }    }}