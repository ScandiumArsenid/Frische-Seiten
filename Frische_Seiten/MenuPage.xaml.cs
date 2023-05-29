using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Reflection.Emit;
using Label = System.Windows.Controls.Label;
using System.Windows.Controls.Primitives;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Frische_Seiten
{
    /// <summary>
    /// Interaktionslogik für MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        //Properties
        OleDbConnection conn;
        DataTable dt;
        OleDbDataAdapter adapter;
        //Listen für die Sortierung
        List<string> tnliste = new List<string>() { "Glatt", "Locken", "Dicht", "Glatze"};
        List<List<int>> p_tliste = new List<List<int>>();
        List<List<int>> n_tliste = new List<List<int>>();
        List<List<int>> b_tliste = new List<List<int>>();
        //booleans für sortierung
        private bool sabst, saufst, sabz, sbew = false;
        public string t;
        private int tindex;
        public int s = 0;
        private int minpreis = 0;
        private int maxpreis = 100;
        private ObservableCollection<string> districts = new ObservableCollection<string>();

        public MenuPage()
        {
            InitializeComponent();
            GetFriseure();
            PreisSort();
            UpdLabel(t, s, MainWindow.nightmode);
            bezirkListBox.DataContext = districts;
        }

        public Label UpdLabel(string t, int s, bool nm)
        {
            if (t == null || s == 0)
            {
                Label label = new Label();
                label.Name = "keinergebniss";
                if (t == null) { label.Content = "Geben Sie Ihren Haartyp an"; }
                else { label.Content = "Keine Ergebnisse"; }
                label.FontFamily = new FontFamily("Bahnschrift");
                label.FontSize = 24;
                if (nm) { label.Foreground = Brushes.White; }
                else { label.Foreground = Brushes.Black; }
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                return label;
            }
            else { return null; }
        }
        void PreisSort()
        {
            for (int f = 0; f < 4; f++)
            {
                List<int> preisliste = new List<int>();
                List<int> p_unsorted = new List<int>();
                List<int> p_posliste = new List<int>();
                List<int> n_posliste = new List<int>(); 
                List<int> b_posliste = new List<int>();
                List<float> bewertungsliste = new List<float>();
                List<float> b_unsorted = new List<float>();
                List<string> namenliste = new List<string>();
                List<string> n_unsorted = new List<string>();
                dt = new DataTable();
                adapter = new OleDbDataAdapter("SELECT *FROM " + tnliste[f], conn);
                conn.Open();
                adapter.Fill(dt);
                for (int x = 0; x < 68; x++)
                {
                    preisliste.Add(int.Parse(dt.Rows[x].ItemArray[5].ToString()));
                    p_unsorted.Add(int.Parse(dt.Rows[x].ItemArray[5].ToString()));
                    namenliste.Add(dt.Rows[x].ItemArray[1].ToString());
                    n_unsorted.Add(dt.Rows[x].ItemArray[1].ToString());
                    bewertungsliste.Add(float.Parse(dt.Rows[x].ItemArray[7].ToString()));
                    b_unsorted.Add(float.Parse(dt.Rows[x].ItemArray[7].ToString()));
                }
                preisliste.Sort();
                namenliste.Sort();
                bewertungsliste.Sort();
                for (int y = 0; y < preisliste.Count(); y++)
                {
                    for (int x = 0; x < p_unsorted.Count(); x++)
                    {
                        if (p_unsorted[x].Equals(preisliste[y]))
                        {
                            p_posliste.Add(x);
                            p_unsorted[x] = 0;
                            preisliste[y] = 999;
                            break;
                        }
                    }
                }
                p_tliste.Add(p_posliste);
                for (int y = 0; y < namenliste.Count(); y++)
                {
                    for (int x = 0; x < n_unsorted.Count(); x++)
                    {
                        if (n_unsorted[x].Equals(namenliste[y]))
                        {
                            n_posliste.Add(x);
                            n_unsorted[x] = "";
                            namenliste[y] = "XXX";
                            break;
                        }
                    }
                }
                n_tliste.Add(n_posliste);
                for (int y = 0; y < bewertungsliste.Count(); y++)
                {
                    for (int x = 0; x < b_unsorted.Count(); x++)
                    {
                        if (b_unsorted[x].Equals(bewertungsliste[y]))
                        {
                            b_posliste.Add(x);
                            b_unsorted[x] = 0;
                            bewertungsliste[y] = 999;
                            break;
                        }
                    }
                }
                b_tliste.Add(b_posliste);
                conn.Close();
            }
            dt = null;
            adapter = null;
        }
        //-------------------------------------------------------

        //Friseuraufruf Methode

        //-------------------------------------------------------
        public void GetFriseure()
        {            
            //conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"frische seiten.accdb\"");
            conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\frische seiten.mdb");
            dt = new DataTable();
            if (t != null)
            {
                adapter = new OleDbDataAdapter("SELECT *FROM " + t, conn);
            }
            DataView dv = new DataView(dt);
            //Die s Hier zählt die Ergebnisse
            //die Stackpanel hier dient das alle ergbnisse gleichzeitig erscheinen
            StackPanel sta = new StackPanel();
            //Liste der Preise und ids zur sortierung
            s = 0;
            conn.Open();
            if (adapter != null)
            {
                adapter.Fill(dt);
                int lindex = dv.Count;
                if (t.Equals("Glatt")) { tindex = 0; }
                else if (t.Equals("Locken")) { tindex = 1; }
                else if (t.Equals("Dicht")) { tindex = 2; }
                else if (t.Equals("Glatze")) { tindex = 3; }
                if (saufst)
                {
                    foreach (int i in p_tliste[tindex])
                    {
                        try
                        {
                            //die If-Statments hier dienen zu filterung
                            if (int.Parse(dt.Rows[i].ItemArray[5].ToString()) > maxpreis)
                            {
                                continue;
                            }
                            if (int.Parse(dt.Rows[i].ItemArray[4].ToString()) < minpreis)
                            {
                                continue;
                            }
                            if (slider != null && double.Parse(dt.Rows[i].ItemArray[7].ToString()) < double.Parse(slider.Value.ToString()))
                            {
                                continue;
                            }
                            if (districts.Count > 0)
                            {
                                bool found = false;
                                char[] bezirkstringarray = dt.Rows[i].ItemArray[6].ToString().ToCharArray();
                                string bezirk = Convert.ToString(bezirkstringarray[1]) + Convert.ToString(bezirkstringarray[2]);
                                foreach (string a in districts)
                                {
                                    if(a.Length == 1)
                                    {
                                        if (bezirk.Equals("0" + a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bezirk.Equals(a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    continue;
                                }
                            }
                            StackPanel bst = new StackPanel();
                            bst.Orientation = Orientation.Horizontal;
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[1].ToString(), 20, -125));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[2].ToString(), -40, -20));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[3].ToString(), -40, 90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[1].ToString(), "friseurnamen", 20, -90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[2].ToString(), "adresse", -500, 20));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[3].ToString(), "telefon", -500, 130));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[4].ToString() + "-" + dt.Rows[i].ItemArray[5].ToString() + " €", "preis", 50, 0));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[7].ToString() + " Sterne", "bewertung", -550, 80));
                            sta.Children.Add(BorderCreater(bst, i));
                            s++;
                        }
                        catch (Exception ex)
                        {
                            //im Falle eines Fehlers erscheint dieses Messagebox
                            MessageBox.Show("Fehler", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else if (sabst)
                {
                    p_tliste[tindex].Reverse();
                    foreach (int i in p_tliste[tindex])
                    {
                        try
                        {
                            //die If-Statments hier dienen zu filterung
                            if (int.Parse(dt.Rows[i].ItemArray[5].ToString()) > maxpreis)
                            {
                                continue;
                            }

                            if (int.Parse(dt.Rows[i].ItemArray[4].ToString()) < minpreis)
                            {
                                continue;
                            }
                            if (slider != null && double.Parse(dt.Rows[i].ItemArray[7].ToString()) < double.Parse(slider.Value.ToString()))
                            {
                                continue;
                            }
                            if (districts.Count > 0)
                            {
                                bool found = false;
                                char[] bezirkstringarray = dt.Rows[i].ItemArray[6].ToString().ToCharArray();
                                string bezirk = Convert.ToString(bezirkstringarray[1]) + Convert.ToString(bezirkstringarray[2]);
                                foreach (string a in districts)
                                {
                                    if (a.Length == 1)
                                    {
                                        if (bezirk.Equals("0" + a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bezirk.Equals(a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    continue;
                                }
                            }

                            StackPanel bst = new StackPanel();
                            bst.Orientation = Orientation.Horizontal;
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[1].ToString(), 20, -125));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[2].ToString(), -40, -20));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[3].ToString(), -40, 90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[1].ToString(), "friseurnamen", 20, -90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[2].ToString(), "adresse", -500, 20));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[3].ToString(), "telefon", -500, 130));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[4].ToString() + "-" + dt.Rows[i].ItemArray[5].ToString() + " €", "preis", 50, 0));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[7].ToString() + " Sterne", "bewertung", -550, 80));
                            sta.Children.Add(BorderCreater(bst, i));
                            s++;
                            
                        }
                        catch (Exception ex)
                        {
                            //im Falle eines Fehlers erscheint dieses Messagebox
                            MessageBox.Show("Fehler", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    p_tliste[tindex].Reverse();
                }
                else if (sabz)
                {
                    foreach (int i in n_tliste[tindex])
                    {
                        try
                        {
                            //die If-Statments hier dienen zu filterung
                            if (int.Parse(dt.Rows[i].ItemArray[5].ToString()) > maxpreis)
                            {
                                continue;
                            }

                            if (int.Parse(dt.Rows[i].ItemArray[4].ToString()) < minpreis)
                            {
                                continue;
                            }
                            if (slider != null && double.Parse(dt.Rows[i].ItemArray[7].ToString()) < double.Parse(slider.Value.ToString()))
                            {
                                continue;
                            }
                            if (districts.Count > 0)
                            {
                                bool found = false;
                                char[] bezirkstringarray = dt.Rows[i].ItemArray[6].ToString().ToCharArray();
                                string bezirk = Convert.ToString(bezirkstringarray[1]) + Convert.ToString(bezirkstringarray[2]);
                                foreach (string a in districts)
                                {
                                    if (a.Length == 1)
                                    {
                                        if (bezirk.Equals("0" + a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bezirk.Equals(a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    continue;
                                }
                            }

                            StackPanel bst = new StackPanel();
                            bst.Orientation = Orientation.Horizontal;
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[1].ToString(), 20, -125));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[2].ToString(), -40, -20));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[3].ToString(), -40, 90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[1].ToString(), "friseurnamen", 20, -90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[2].ToString(), "adresse", -500, 20));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[3].ToString(), "telefon", -500, 130));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[4].ToString() + "-" + dt.Rows[i].ItemArray[5].ToString() + " €", "preis", 50, 0));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[7].ToString() + " Sterne", "bewertung", -550, 80));
                            sta.Children.Add(BorderCreater(bst, i));
                            s++;
                        }
                        catch (Exception ex)
                        {
                            //im Falle eines Fehlers erscheint dieses Messagebox
                            MessageBox.Show("Fehler", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else if (sbew)
                {
                    b_tliste[tindex].Reverse();
                    foreach (int i in b_tliste[tindex])
                    {
                        try
                        {
                            //die If-Statments hier dienen zu filterung
                            if (int.Parse(dt.Rows[i].ItemArray[5].ToString()) > maxpreis)
                            {
                                continue;
                            }

                            if (int.Parse(dt.Rows[i].ItemArray[4].ToString()) < minpreis)
                            {
                                continue;
                            }
                            if (slider != null && double.Parse(dt.Rows[i].ItemArray[7].ToString()) < double.Parse(slider.Value.ToString()))
                            {
                                continue;
                            }
                            if (districts.Count > 0)
                            {
                                bool found = false;
                                char[] bezirkstringarray = dt.Rows[i].ItemArray[6].ToString().ToCharArray();
                                string bezirk = Convert.ToString(bezirkstringarray[1]) + Convert.ToString(bezirkstringarray[2]);
                                foreach (string a in districts)
                                {
                                    if (a.Length == 1)
                                    {
                                        if (bezirk.Equals("0" + a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bezirk.Equals(a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    continue;
                                }
                            }
                            StackPanel bst = new StackPanel();
                            bst.Orientation = Orientation.Horizontal;
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[1].ToString(), 20, -125));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[2].ToString(), -40, -20));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[3].ToString(), -40, 90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[1].ToString(), "friseurnamen", 20, -90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[2].ToString(), "adresse", -500, 20));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[3].ToString(), "telefon", -500, 130));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[4].ToString() + "-" + dt.Rows[i].ItemArray[5].ToString() + " €", "preis", 50, 0));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[7].ToString() + " Sterne", "bewertung", -550, 80));
                            sta.Children.Add(BorderCreater(bst, i));
                            s++;
                        }
                        catch (Exception ex)
                        {
                            //im Falle eines Fehlers erscheint dieses Messagebox
                            MessageBox.Show("Fehler", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    b_tliste[tindex].Reverse();
                }
                else
                {
                    for(int i = 0; i < lindex; i++)
                    {
                        try
                        {
                            //die If-Statments hier dienen zu filterung
                            if (int.Parse(dt.Rows[i].ItemArray[5].ToString()) > maxpreis)
                            {
                                continue;
                            }

                            if (int.Parse(dt.Rows[i].ItemArray[4].ToString()) < minpreis)
                            {
                                continue;
                            }
                            if (slider != null && double.Parse(dt.Rows[i].ItemArray[7].ToString()) < double.Parse(slider.Value.ToString()))
                            {
                                continue;
                            }
                            if (districts.Count > 0)
                            {
                                bool found = false;
                                char[] bezirkstringarray = dt.Rows[i].ItemArray[6].ToString().ToCharArray();
                                string bezirk = Convert.ToString(bezirkstringarray[1]) + Convert.ToString(bezirkstringarray[2]);
                                foreach (string a in districts)
                                {
                                    if (a.Length == 1)
                                    {
                                        if (bezirk.Equals("0" + a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (bezirk.Equals(a))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found)
                                {
                                    continue;
                                }
                            }
                            StackPanel bst = new StackPanel();
                            bst.Orientation = Orientation.Horizontal;
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[1].ToString(), 20, -125));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[2].ToString(), -40, -20));
                            bst.Children.Add(CopyButtonCreator(dt.Rows[i].ItemArray[3].ToString(), -40, 90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[1].ToString(), "friseurnamen", 20, -90));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[2].ToString(), "adresse", -500, 20));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[3].ToString(), "telefon", -500, 130));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[4].ToString() + "-" + dt.Rows[i].ItemArray[5].ToString() + " €", "preis", 50, 0));
                            bst.Children.Add(Labelcreater(dt.Rows[i].ItemArray[7].ToString() + " Sterne", "bewertung", -550, 80));
                            sta.Children.Add(BorderCreater(bst, i));
                            s++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                //wenn es keine Ergebnisse Gibt
                if (s == 0)
                {
                    //                    Label label = new Label();
                    //                    label.Name = "keinergebniss";
                    //                    label.Content = "Keine Ergebnisse";
                    //                    label.FontFamily = new FontFamily("Bahnschrift");
                    //                    label.FontSize = 24;
                    //                    //ändere Hier die Foreground so das es bei lightmode
                    //                    //schwarz wird und bei nightmode weiß
                    //                    label.Foreground = Brushes.White;
                    //                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    sta.Children.Add(UpdLabel(t, s, MainWindow.nightmode));
                    anzahll.Content = 0;

                }
                //Anzahl der Ergebnisse
                if (anzahll != null)
                {
                    anzahll.Content = s;
                }
                ausgabesc.Content = sta;
                conn.Close();

            }
            //falls t noch keinen Wert hat
            else
            {
                sta.Children.Add(UpdLabel(t, s, MainWindow.nightmode));
                //sta.Children.Add(label);
                ausgabesc.Content = sta;
                conn.Close();
                anzahll.Content = 0;
            }
        }


        //es erstellt border wo die ganzen Friseurinfos sind
        //Hier auch passe es lightmode ("#FFE699") und nightmode ("#4472C4") an
        private Border BorderCreater(StackPanel bst, int a)
        {
            Border border = new Border();
            border.Name = "friseur" + a;
            border.CornerRadius = new CornerRadius(20);
            if (MainWindow.nightmode)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4472C4"));
            }
            else { border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966")); }
            border.Height = 230;
            border.Width = 764;
            //border.Width = 1500;
            border.Margin = new Thickness(0, 20, 0, 10);
            border.HorizontalAlignment = HorizontalAlignment.Center;
            border.BorderThickness = new Thickness(1);
            border.Child = bst;
            return border;
        }
        //da wird der Text erstellt was in den Borders danach kommt
        //Hier auch pass es den lightmode (Schwarzer text) und nightmode (Weißer Text) an

        private Label Labelcreater(string inhalt, String name, int a, int b)
        {
            Label label = new Label();
            label.Content = inhalt;
            label.Name = name;
            label.FontFamily = new FontFamily("Amasis MT Pro Medium");
            label.FontSize = 24;
            if (MainWindow.nightmode) { label.Foreground = Brushes.White; }
            else { label.Foreground = Brushes.Black; }
            label.Width = 500;
            label.Height = 72;
            label.Margin = new Thickness(a, b, 0, 10);
            return label;
        }

        private Button CopyButtonCreator(string text, int a, int b)
        {
            Button button = new Button();
            var brush = new ImageBrush();
            brush.ImageSource = bi1;
            button.Background = brush;
            button.Margin = new Thickness(a, b, 10, 10);
            button.Width = 30;
            button.Height = button.Width;
            button.Click += (s, e) => { Clipboard.SetText(text);erfolgko();};
            return button;
        }
        private async void erfolgko()
        {
            Stopwatch stopWatch = new Stopwatch();
            kopiert.Visibility = Visibility.Visible;
            stopWatch.Start();
            await Task.Delay(1000);
            kopiert.Visibility = Visibility.Hidden;
            stopWatch.Stop();
        }

        private void Glatt_Checked(object sender, RoutedEventArgs e)
        {
            t = "Glatt";
            tindex = 0;
            adapter = new OleDbDataAdapter("SELECT *FROM " + t, conn);
            GetFriseure();
        }
        private void Locken_Checked(object sender, RoutedEventArgs e)
        {
            t = "Locken";
            tindex = 1;
            adapter = new OleDbDataAdapter("SELECT *FROM " + t, conn);
            GetFriseure();
        }
        private void Dicht_Checked(object sender, RoutedEventArgs e)
        {
            t = "Dicht";
            tindex = 2;
            adapter = new OleDbDataAdapter("SELECT *FROM " + t, conn);
            GetFriseure();
        }
        private void Glatze_Checked(object sender, RoutedEventArgs e)
        {
            t = "Glatze";
            tindex = 3;
            adapter = new OleDbDataAdapter("SELECT *FROM " + t, conn);
            GetFriseure();
        }

        //-------------------------------------------------------

        //Die Hauptmethoden zur Filterung

        //-------------------------------------------------------

        private void Max_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                maxpreis = int.Parse(max.Text);
                GetFriseure();
            }
            catch
            {
                maxpreis = 1000;
            }
        }
        private void Min_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                minpreis = int.Parse(min.Text);
                GetFriseure();
            }
            catch
            {
                minpreis = 0;
            }
        }
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GetFriseure();
        }
        private int heightb;
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (districts.Count < 4)
            {
                try
                {
                    if (districts.Contains(DistrictNameTextBox.Text))
                    {
                        MessageBox.Show("Bezirk bereits vorhanden", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        int textinte = Convert.ToInt32(DistrictNameTextBox.Text);
                        if (int.Parse(DistrictNameTextBox.Text) is int && textinte <= 23 &&(DistrictNameTextBox.Text.Length == 1 || DistrictNameTextBox.Text.Length == 2))
                        {
                            districts.Add(DistrictNameTextBox.Text);
                            DistrictNameTextBox.Text = string.Empty;
                        }
                        if (textinte > 23)
                        {
                            MessageBox.Show("Bezirk existiert nicht", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Sie haben einen nicht gültigen Wert eingegeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    if (districts.Contains(DistrictNameTextBox.Text))
                    {
                        MessageBox.Show("Bezirk bereits vorhanden", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        int textinte = Convert.ToInt32(DistrictNameTextBox.Text);
                        if (int.Parse(DistrictNameTextBox.Text) is int && textinte  <= 23 && (DistrictNameTextBox.Text.Length == 1 || DistrictNameTextBox.Text.Length == 2))
                        {
                            districts.Add(DistrictNameTextBox.Text);
                            DistrictNameTextBox.Text = string.Empty;
                            if (bezirkB.Height < 200)
                            {
                                heightb += 15;
                                Height += 15;
                                bezirkSV.Height += 15;
                            }
                        }
                        else
                        {
                            if (textinte > 23)
                            {
                                MessageBox.Show("Bezirk existiert nicht", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("sie haben einen nicht gültigen wert eingegeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }
            if (districts.Count !=0)
            {
                bezirkSV.Visibility = Visibility.Visible;
                Alllose.Visibility= Visibility.Visible;
            }
            GetFriseure();
        }

        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var panel = (StackPanel)button.Parent;
            var textBlock = (TextBlock)panel.Children[0];
            districts.Remove(textBlock.Text);
            if (districts.Count == 0)
            {
                bezirkSV.Visibility = Visibility.Hidden;
                Alllose.Visibility = Visibility.Hidden;
            }
            GetFriseure();
        }
        private void Alllose_Click(object sender, RoutedEventArgs e)
        {
            districts.Clear();
            bezirkSV.Visibility = Visibility.Hidden;
            Alllose.Visibility = Visibility.Hidden;
            GetFriseure();
        }


        //-------------------------------------------------------
        //Das ist das Code für Wasser Zeichen
        //-------------------------------------------------------

        private new void GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == textBox.Name)
            {
                textBox.Text = null;
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        private new void LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = textBox.Name;
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C9C9C"));
            }
        }
        //ComboboxItem Text soll schwarz/weiß werden
        private void sortieren_aufst_Selected(object sender, RoutedEventArgs e)
        {
            text_aufst.Foreground = new SolidColorBrush(Colors.Black);
            Img_aufst_w.Visibility = Visibility.Collapsed;
            Img_aufst_b.Visibility = Visibility.Visible;
            saufst = true;
            GetFriseure();
        }

        private void sortieren_abst_Selected(object sender, RoutedEventArgs e)
        {
            text_abst.Foreground = new SolidColorBrush(Colors.Black);
            Img_abst_w.Visibility = Visibility.Collapsed;
            Img_abst_b.Visibility = Visibility.Visible;
            sabst = true;
            GetFriseure();
        }

        private void sortieren_abz_Selected(object sender, RoutedEventArgs e)
        {
            text_abz.Foreground = new SolidColorBrush(Colors.Black);
            // Img_abz_w.Source = ((System.Windows.Controls.Image)this.Resources[@"\Bilder\sortieren-abz_b.png"]).Source;
            Img_abz_w.Visibility = Visibility.Collapsed;
            Img_abz_b.Visibility = Visibility.Visible;
            sabz = true;
            GetFriseure();
        }
        private void sortieren_bew_Selected(object sender, RoutedEventArgs e)
        {
            text_bew.Foreground = new SolidColorBrush(Colors.Black);
            Img_bew_w.Visibility = Visibility.Collapsed;
            Img_bew_b.Visibility = Visibility.Visible;
            sbew = true;
            GetFriseure();
        }

        private void sortieren_aufst_Unselected(object sender, RoutedEventArgs e)
        {
            if(MainWindow.nightmode)
            {
                Img_aufst_w.Visibility = Visibility.Visible;
                Img_aufst_b.Visibility = Visibility.Collapsed;
                text_aufst.Foreground = new SolidColorBrush(Colors.White);
            }
            saufst = false;
            GetFriseure();
        }

        private void slider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            GetFriseure();
        }

        private void sortieren_abst_Unselected(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nightmode)
            {
                Img_abst_w.Visibility = Visibility.Visible;
                Img_abst_b.Visibility = Visibility.Collapsed;
                text_abst.Foreground = new SolidColorBrush(Colors.White);
            }
            sabst = false;
            GetFriseure();
        }

        private void sortieren_abz_Unselected(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nightmode)
            {
                Img_abz_w.Visibility = Visibility.Visible;
                Img_abz_b.Visibility = Visibility.Collapsed;
                text_abz.Foreground = new SolidColorBrush(Colors.White);
            }
            sabz = false;
            GetFriseure();
        }
        private void sortieren_bew_Unselected(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nightmode)
            {
                Img_bew_w.Visibility = Visibility.Visible;
                Img_bew_b.Visibility = Visibility.Collapsed;
                text_bew.Foreground = new SolidColorBrush(Colors.White);
            }
            sbew = false;
            GetFriseure();
        }
    }
}
