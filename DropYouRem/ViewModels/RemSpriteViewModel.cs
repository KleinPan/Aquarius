using System;
using System.Reactive;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

using DropYouRem.Base;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DropYouRem.ViewModels
{
    internal class RemSpriteViewModel : BaseViewModel
    {
        [Reactive]
        public string GifURI { get; set; }


        [Reactive]
        public BitmapImage ImgUri { get; set; }

        public ReactiveCommand<object, Unit> DragEnterCommand { get; private set; }

        public RemSpriteViewModel()
        {
            string binPath = Assembly.GetExecutingAssembly().Location;
            string dir = binPath.Substring(0, binPath.LastIndexOf('\\'));
            
           

            Uri uri = new Uri(dir+@"\Resources\test.png", UriKind.RelativeOrAbsolute);
            
            //Console.WriteLine(uri.AbsoluteUri);

            BitmapImage wpfBitmap = new BitmapImage();
            wpfBitmap.BeginInit();
            wpfBitmap.UriSource = uri;// @"/ImageResTestLib;component/MyData/SomeStuff/Resources/Img.png"
            wpfBitmap.EndInit();

            //Console.WriteLine(wpfBitmap.BaseUri);
            //Console.WriteLine(wpfBitmap.UriSource);
            ImgUri = wpfBitmap;

            DragEnterCommand = ReactiveCommand.Create<object>(DragEnterEvent);
        }

        private void DragEnterEvent(object obj)
        {
            var args = obj as DragEventArgs;
            var data = args.Data;

            string[] filePath = (string[])data.GetData(DataFormats.FileDrop);

        }
    }
}