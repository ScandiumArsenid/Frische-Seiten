using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Media.Animation;

namespace Frische_Seiten
{
    /// <summary>
    /// Interaktionslogik für Passwort.xaml
    /// </summary>
    public partial class Passwort : Page
    {
        //Properties
        OleDbConnection conn, conn2;
        DataTable dt;
        OleDbDataAdapter adapter;
        ArrayList passlist = new ArrayList();
        //Speichert alle hash-werte als string in passlist und gibt länge als int zurück
        int GetPass()
        {
            //conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\frische seiten.accdb");
            //conn2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\frische seiten.accdb");
            conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\frische seiten.mdb");
            dt = new DataTable();
            adapter = new OleDbDataAdapter("SELECT *FROM " + "Pass", conn);
            conn.Open();
            adapter.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                passlist.Add(dt.Rows[i].ItemArray[1].ToString());
            }
            conn.Close();
            return passlist.Count;
        }
        //legt neues passwort mittels INSERT query an
        void NewPass(string npass)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "INSERT INTO Pass (ID, Pw) VALUES ('" + Convert.ToInt32(dt.Rows.Count+1) + "','" + Convert.ToString(ComputeSha1(npass)) + "')";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
            //conn2.Open();
            //OleDbCommand cmd2 = new OleDbCommand();
            //cmd2.CommandText = "INSERT INTO Pass (ID, Pw) VALUES ('" + Convert.ToInt32(dt.Rows.Count + 1) + "','" + Convert.ToString(ComputeSha1(npass)) + "')";
            //cmd2.Connection = conn2;
            //cmd2.ExecuteNonQuery();
            //conn2.Close();
            MessageBox.Show("Data Inserted.");
        }
        bool checkPass(string givenpass)
        {
            string hgpass = ComputeSha1(givenpass);
            for(int i = 0; i < passc; i++)
            {
                if(hgpass.Equals(Convert.ToString(passlist[i])))
                {
                    return true;
                }
            }
            return false;
        }
        //Anzahl der passwörter in db
        private int passc;
        public Passwort()
        {
            InitializeComponent();
            Backborder.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = this });
            Backborder.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = this });
            passc = GetPass();
        }

        //Wandelt passwort in byte[] hashwert mittels SHA-1 um
        string ComputeSha1(string input)
        {
            // Convert the input string to a byte array
            byte[] buffer = Encoding.UTF8.GetBytes(input);

            // Create a new instance of the SHA-1 crypto service provider
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                // Compute the hash of the input buffer
                byte[] hash = sha1.ComputeHash(buffer);

                // Convert the hash to a string and return it
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        //Einlogbutton ==> überprüft Benutzername und Passowrt 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string benutzer = "admin";
            if (checkPass(Convert.ToString(passwort.Password)) == true && Benutzername.Text == benutzer)
            {
                this.Visibility = Visibility.Hidden;
                Check = true;
            }
            Hilfe.Visibility = Visibility.Visible;
        }
        public bool Check
        {
            get;
            set;
        }

        //Hilfe Button ==> Wenn du Passwort vergessen hast
        private void Hilfe_Click(object sender, RoutedEventArgs e)
        {
            Benutzername.Visibility = Visibility.Hidden;
            passwort.Visibility = Visibility.Hidden;
            loginbutton.Visibility = Visibility.Hidden;
            bel.Visibility = Visibility.Hidden;
            pal.Visibility = Visibility.Hidden;
            frage.Visibility = Visibility.Visible;
            antwort.Visibility = Visibility.Visible;
            if (antwort.Text == "kitkat")
            {
                frage.Content = "neues Passwort?";
                antwort.Text = "";
                Hilfe.Visibility = Visibility.Hidden;
                neuerpasswort.Visibility = Visibility.Visible;
                neuerpasswort.Content = "festlegen";
            }
        }

        //lässt dich einen neuen Passwort legen
        private void neuerpasswort_Click(object sender, RoutedEventArgs e)
        {
            //passwort in hashwert umwandeln und in db einfügen
            if(passc < 4)
            {
                // Fehler mit INSERT, da zwei Datenbanken verwendet werden
                NewPass(Convert.ToString(antwort.Text));
            }
            neuerpasswort.Visibility = Visibility.Hidden;
            frage.Visibility= Visibility.Hidden;
            antwort.Visibility = Visibility.Hidden;
            bel.Visibility = Visibility.Visible;
            pal.Visibility = Visibility.Visible;
            Benutzername.Visibility = Visibility.Visible;
            passwort.Visibility = Visibility.Visible;
            loginbutton.Visibility = Visibility.Visible;
            antwort.Visibility = Visibility.Hidden;
            Benutzername.Visibility = Visibility.Visible;
        }
    }
}

