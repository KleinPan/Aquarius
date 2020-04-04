using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DropYouRem.Views
{
    /// <summary>
    /// Interaction logic for RemSprite.xaml
    /// </summary>
    public partial class RemSpriteView
    {
        public RemSpriteView()
        {
            InitializeComponent();
            var baseUri = BaseUriHelper.GetBaseUri(this);
        }

        public void Dispose()
        {
            //GifImageMain.Dispose();
        }
    }
}