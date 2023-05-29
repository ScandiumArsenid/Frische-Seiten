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

namespace Frische_Seiten
{
    /// <summary>
    /// Interaktionslogik für HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
            Backborder.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = this });
            Backborder.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = this });
        }

    }
}
