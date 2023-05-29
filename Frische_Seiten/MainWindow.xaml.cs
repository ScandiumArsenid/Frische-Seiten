using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frische_Seiten
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool nightmode = true;


        public MainWindow()
        {
            InitializeComponent();
            myBorder.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = this });
            myBorder.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = this });
            menupolygon.Visibility = Visibility.Visible;
            update();            
            this.Background = null;
        }




        //------------------------------------------------------------------------

        //Design Teil

        //------------------------------------------------------------------------
        TeamPage teampage = new TeamPage();
        MenuPage menupage = new MenuPage();
        AdminPage adminPage = new AdminPage();
        HelpPage helpPage = new HelpPage();

        //------------------------------------------------------------------------
        //MenuPage Teil
        //------------------------------------------------------------------------
        private void Menub(object sender, MouseButtonEventArgs e)
        {
            menupolygon.Visibility = Visibility.Visible;
            teampolygon.Visibility = Visibility.Hidden;
            adminpolygon.Visibility = Visibility.Hidden;
            helppolygon.Visibility = Visibility.Hidden;
            menupage.GetFriseure();
            if (nightmode)
            {
                menupage.backBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.filterBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222A35"));
                menupage.sortierl.Foreground = new SolidColorBrush(Colors.White);
                menupage.anzahlEl.Foreground = new SolidColorBrush(Colors.White);
                menupage.anzahll.Foreground = new SolidColorBrush(Colors.White);
                menupage.HaartypB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.preisB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.bewertungB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.sortierFktBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222A35"));
                menupage.Glatt.Foreground = new SolidColorBrush(Colors.White);
                menupage.Glatze.Foreground = new SolidColorBrush(Colors.White);
                menupage.Locken.Foreground = new SolidColorBrush(Colors.White);
                menupage.Dicht.Foreground = new SolidColorBrush(Colors.White);
                menupage.bewertungl.Foreground = new SolidColorBrush(Colors.White);
                menupage.BewertungZl.Foreground = new SolidColorBrush(Colors.White);
                menupage.preis.Foreground = new SolidColorBrush(Colors.White);
                menupage.Haartyp.Foreground = new SolidColorBrush(Colors.White);
                menupage.text_abz.Foreground= new SolidColorBrush(Colors.White);
                menupage.text_aufst.Foreground = new SolidColorBrush(Colors.White);
                menupage.text_abst.Foreground = new SolidColorBrush(Colors.White);
                menupage.text_bew.Foreground = new SolidColorBrush(Colors.White);
                menupage.sortieren_abz.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.sortieren_aufst.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.sortieren_abst.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.sortieren_bew.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.bezirkB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                menupage.bezirkl.Foreground = new SolidColorBrush(Colors.White);
                menupage.period.Foreground = new SolidColorBrush(Colors.White);
                menupage.Img_abz_w.Visibility = Visibility.Visible;
                menupage.Img_abz_b.Visibility = Visibility.Collapsed;
                menupage.Img_aufst_w.Visibility = Visibility.Visible;
                menupage.Img_aufst_b.Visibility = Visibility.Collapsed;
                menupage.Img_abst_w.Visibility = Visibility.Visible;
                menupage.Img_abst_b.Visibility = Visibility.Collapsed;
                menupage.Img_bew_w.Visibility = Visibility.Visible;
                menupage.Img_bew_b.Visibility = Visibility.Collapsed;
            }
            else
            {
                menupage.backBorder.Background = new SolidColorBrush(Colors.GhostWhite);
                menupage.filterBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2"));
                menupage.sortierFktBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2"));
                menupage.sortierl.Foreground = new SolidColorBrush(Colors.Black);
                menupage.anzahlEl.Foreground = new SolidColorBrush(Colors.Black);
                menupage.anzahll.Foreground = new SolidColorBrush(Colors.Black);
                menupage.HaartypB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.preisB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.bewertungB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.Glatt.Foreground = new SolidColorBrush(Colors.Black);
                menupage.Glatze.Foreground = new SolidColorBrush(Colors.Black);
                menupage.Locken.Foreground = new SolidColorBrush(Colors.Black);
                menupage.Dicht.Foreground = new SolidColorBrush(Colors.Black);
                menupage.bewertungl.Foreground = new SolidColorBrush(Colors.Black);
                menupage.BewertungZl.Foreground = new SolidColorBrush(Colors.Black);
                menupage.preis.Foreground = new SolidColorBrush(Colors.Black);
                menupage.Haartyp.Foreground = new SolidColorBrush(Colors.Black);
                menupage.sortieren_abz.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.sortieren_aufst.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.sortieren_abst.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.sortieren_bew.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.bezirkB.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966"));
                menupage.bezirkl.Foreground = new SolidColorBrush(Colors.Black);
                menupage.text_abz.Foreground = new SolidColorBrush(Colors.Black);
                menupage.text_aufst.Foreground = new SolidColorBrush(Colors.Black);
                menupage.text_abst.Foreground = new SolidColorBrush(Colors.Black);
                menupage.text_bew.Foreground = new SolidColorBrush(Colors.Black);
                menupage.period.Foreground = new SolidColorBrush(Colors.Black);
                menupage.Img_abz_w.Visibility = Visibility.Collapsed;
                menupage.Img_abz_b.Visibility = Visibility.Visible;
                menupage.Img_aufst_w.Visibility = Visibility.Collapsed;
                menupage.Img_aufst_b.Visibility = Visibility.Visible;
                menupage.Img_abst_w.Visibility = Visibility.Collapsed;
                menupage.Img_abst_b.Visibility = Visibility.Visible;
                menupage.Img_bew_w.Visibility = Visibility.Collapsed;
                menupage.Img_bew_b.Visibility = Visibility.Visible;
                //#203864
                //#FFD966 fürfilter border                
            }

            mainfram.Content = menupage;
        }

        //------------------------------------------------------------------------
        //TeamPage Teil
        //------------------------------------------------------------------------

        private void Teamb(object sender, MouseButtonEventArgs e)
        {
            menupolygon.Visibility = Visibility.Hidden;
            teampolygon.Visibility = Visibility.Visible;
            adminpolygon.Visibility = Visibility.Hidden;
            helppolygon.Visibility = Visibility.Hidden;
            if (nightmode)
            {
               
                teampage.Backborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                teampage.Text1.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text2.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text3.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text4.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text5.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text6.Foreground = new SolidColorBrush(Colors.White);
                teampage.Text7.Foreground = new SolidColorBrush(Colors.White);
                teampage.name1.Foreground = new SolidColorBrush(Colors.White);
                teampage.name2.Foreground = new SolidColorBrush(Colors.White);
                teampage.name3.Foreground = new SolidColorBrush(Colors.White);
                teampage.aufgabe1.Foreground = new SolidColorBrush(Colors.White);
                teampage.aufgabe2.Foreground = new SolidColorBrush(Colors.White);
                teampage.aufgabe3.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                teampage.Backborder.Background = new SolidColorBrush(Colors.GhostWhite);
                teampage.Text1.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text2.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text3.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text4.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text5.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text6.Foreground = new SolidColorBrush(Colors.Black);
                teampage.Text7.Foreground = new SolidColorBrush(Colors.Black);
                teampage.name1.Foreground = new SolidColorBrush(Colors.Black);
                teampage.name2.Foreground = new SolidColorBrush(Colors.Black);
                teampage.name3.Foreground = new SolidColorBrush(Colors.Black);
                teampage.aufgabe1.Foreground = new SolidColorBrush(Colors.Black);
                teampage.aufgabe2.Foreground = new SolidColorBrush(Colors.Black);
                teampage.aufgabe3.Foreground = new SolidColorBrush(Colors.Black);
            }
            mainfram.Content = teampage;
        }

        //------------------------------------------------------------------------
        //HelpPage Teil
        //------------------------------------------------------------------------
        
        private void Helpb(object sender, MouseButtonEventArgs e)
        {
            menupolygon.Visibility = Visibility.Hidden;
            teampolygon.Visibility = Visibility.Hidden;
            adminpolygon.Visibility = Visibility.Hidden;
            helppolygon.Visibility = Visibility.Visible;
            if (nightmode)
            {
                helpPage.Backborder.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                helpPage.Label1.Foreground= new SolidColorBrush(Colors.White);
                helpPage.Label2.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Label3.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Label4.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text1.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text2.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text3.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text4.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text5.Foreground = new SolidColorBrush(Colors.White); 
                helpPage.Text6.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text7.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text8.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text9.Foreground = new SolidColorBrush(Colors.White); 
                helpPage.Text10.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text11.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text12.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text13.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text15.Foreground = new SolidColorBrush(Colors.White);
                helpPage.Text16.Foreground = new SolidColorBrush(Colors.White);

            }
            else
            {
                helpPage.Backborder.Background = new SolidColorBrush(Colors.GhostWhite);
                helpPage.Label1.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Label2.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Label3.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Label4.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text1.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text2.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text3.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text4.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text5.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text6.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text7.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text8.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text9.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text10.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text11.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text12.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text13.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text15.Foreground = new SolidColorBrush(Colors.Black);
                helpPage.Text16.Foreground = new SolidColorBrush(Colors.Black);
            }   


            mainfram.Content = helpPage;
        }

        //------------------------------------------------------------------------
        //AdminPage Teil
        //------------------------------------------------------------------------

        private void Adminb(object sender, MouseButtonEventArgs e)
        {
            menupolygon.Visibility = Visibility.Hidden;
            teampolygon.Visibility = Visibility.Hidden;
            adminpolygon.Visibility = Visibility.Visible;
            helppolygon.Visibility = Visibility.Hidden;
            if (nightmode)
            {
                
                adminPage.Backborder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
                adminPage.FilterBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF02072B"));
                adminPage.IDl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.namel.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Adressel.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Tell.Foreground = new SolidColorBrush(Colors.White);
                adminPage.minl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.maxl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.PLZl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Bewertungl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.searchl.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Glatt.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Locken.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Dicht.Foreground = new SolidColorBrush(Colors.White);
                adminPage.Glatze.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                
                adminPage.Backborder.Background = new SolidColorBrush(Colors.GhostWhite);
                adminPage.FilterBorder.Background = new SolidColorBrush(Colors.LightGray);
                adminPage.IDl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.namel.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Adressel.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Tell.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.minl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.maxl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.PLZl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Bewertungl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.searchl.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Glatt.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Locken.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Dicht.Foreground = new SolidColorBrush(Colors.Black);
                adminPage.Glatze.Foreground = new SolidColorBrush(Colors.Black);
            }
            mainfram.Content = adminPage;

        }

        //Update Methode Ruft die Methoden auf zum aufrufen der Pages
        private void update()
        {
            if (menupolygon.Visibility == Visibility.Visible)
            {
                Menub(null, null);
                menupage.GetFriseure();
            }
            else if (teampolygon.Visibility == Visibility.Visible)
            {
                Teamb(null, null);
            }
            else if (adminpolygon.Visibility == Visibility.Visible)
            {
                Adminb(null, null);
            }
            else if (helppolygon.Visibility == Visibility.Visible)
            {
                Helpb(null, null);
            }
        }

        //------------------------------------------------------------------------

        //Switch night- & lightmode

        //------------------------------------------------------------------------

        private void lightButton(object sender, RoutedEventArgs e)
        {
            GradientStop gradientStop1 = myBorder.FindName("gradientStop1") as GradientStop;
            GradientStop gradientStop2 = myBorder.FindName("gradientStop2") as GradientStop;
            GradientStop gradientStopOffset = myBorder.FindName("gradientStopOffset") as GradientStop;
            gradientStop1.Color = Colors.SkyBlue;
            gradientStop2.Color = Colors.SkyBlue;
            gradientStopOffset.Color = Colors.Red;
            Nightmode.Visibility = Visibility.Visible;
            Lightmode.Visibility = Visibility.Hidden;
            myBorder.Background = new SolidColorBrush(Colors.GhostWhite);
            nebenBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF2CC"));
            topBorder.Background = new SolidColorBrush(Colors.Black);
            adminpicblack.Visibility = Visibility.Visible;
            adminpicwhite.Visibility = Visibility.Hidden;
            menupicewhite.Visibility = Visibility.Hidden;
            menupicblack.Visibility = Visibility.Visible;
            teampicblack.Visibility = Visibility.Visible;
            teampicwhite.Visibility = Visibility.Hidden;
            helppicblack.Visibility = Visibility.Visible;
            helppicwhite.Visibility = Visibility.Hidden;
            teamtxt.Foreground = new SolidColorBrush(Colors.Black);
            admintxt.Foreground = new SolidColorBrush(Colors.Black);
            menupolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE699"));
            helppolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE699"));
            teampolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE699"));
            adminpolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF8B47"));
            nightmode = false;
            menupage.UpdLabel(menupage.t, menupage.s, MainWindow.nightmode);
            update();

        }
        private void nightButton(object sender, RoutedEventArgs e)
        {
            GradientStop gradientStop1 = myBorder.FindName("gradientStop1") as GradientStop;
            GradientStop gradientStop2 = myBorder.FindName("gradientStop2") as GradientStop;
            GradientStop gradientStopOffset = myBorder.FindName("gradientStopOffset") as GradientStop;
            gradientStop1.Color = Colors.Blue;
            gradientStop2.Color = Colors.Blue;
            gradientStopOffset.Color = Colors.Black;
            Lightmode.Visibility = Visibility.Visible;
            Nightmode.Visibility = Visibility.Hidden;
            myBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#203864"));
            nebenBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A1919"));
            topBorder.Background = new SolidColorBrush(Colors.Black);
            adminpicblack.Visibility = Visibility.Hidden;
            adminpicwhite.Visibility = Visibility.Visible;
            menupicewhite.Visibility = Visibility.Visible;
            menupicblack.Visibility = Visibility.Hidden;
            teampicblack.Visibility = Visibility.Hidden;
            teampicwhite.Visibility = Visibility.Visible;
            helppicblack.Visibility = Visibility.Hidden;
            helppicwhite.Visibility = Visibility.Visible;
            teamtxt.Foreground = new SolidColorBrush(Colors.White);
            admintxt.Foreground = new SolidColorBrush(Colors.White);
            menupolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08758C"));
            teampolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08758C"));
            adminpolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD00000"));
            helppolygon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08758C"));
            nightmode = true;
            menupage.UpdLabel(menupage.t, menupage.s, MainWindow.nightmode);
            update();
        }
        public Brush Textfarbe()
        {
            if (nightmode)
            {
                return Brushes.White;
            }
            else
            {
                return Brushes.Black;
            }
        }




        //------------------------------------------------------------------------

        //Desplay Teil

        //------------------------------------------------------------------------
        private void move(object sender, MouseEventArgs e)
        {
            Point mousePos = Mouse.GetPosition(this);

            // Set the position of the window to the mouse position

            switch (this.WindowState)
            {
                case WindowState.Normal:
                    DragMove();
                    break;
                case WindowState.Maximized:

                    this.WindowState = WindowState.Normal;
                    this.Left = mousePos.X - this.Width / 2;
                    this.Top = mousePos.Y - 30;
                    DragMove();

                    break;
                default:
                    break;
            }
        }

        //Die drei Knöpfe
        private void Schließen(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minmax(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
                default:
                    break;
            }
        }

        private void hide(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = "https://kas22167.webspace.spengergasse.at/", UseShellExecute = true });
        }
        
    }
}
