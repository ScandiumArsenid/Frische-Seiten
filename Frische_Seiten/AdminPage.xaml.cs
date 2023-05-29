using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
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
using System.Xml.Linq;
using System.Reflection;

namespace Frische_Seiten
{
    /// <summary>
    /// Interaktionslogik für AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private OleDbConnection _connection;
        private DataTable _dataTable;
        private string _tableName;
        private bool _isFirstLoad = true;
        private DataRowView _selectedRow;

        public AdminPage()
        {
            InitializeComponent();
            Backborder.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = this });
            Backborder.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = this });
            passwortfram.SetBinding(Border.HeightProperty, new Binding("ActualHeight") { Source = this });
            passwortfram.SetBinding(Border.WidthProperty, new Binding("ActualWidth") { Source = this });
            Passwort passwort = new Passwort();
            passwortfram.Content = passwort;
            GetDatabase();
        }

        private void GetDatabase()
        {
            if(_tableName!= null)
            {
                try
                {
                    //_connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"frische seiten.accdb\"");
                    _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\frische seiten.mdb");
                    _dataTable = new DataTable();
                    var adapter = new OleDbDataAdapter($"SELECT * FROM {_tableName}", _connection);

                    var dataView = new DataView(_dataTable);
                    _connection.Open();
                    adapter.Fill(_dataTable);
                    friseurdaten.ItemsSource = dataView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    _connection?.Close();
                }
            }            
        }
        private void Glatt_Checked(object sender, RoutedEventArgs e)
        {
            _tableName = "Glatt";
            GetDatabase();
        }
        private void Locken_Checked(object sender, RoutedEventArgs e)
        {
            _tableName = "Locken";
            GetDatabase();
        }
        private void Dicht_Checked(object sender, RoutedEventArgs e)
        {
            _tableName = "Dicht";
            GetDatabase();
        }
        private void Glatze_Checked(object sender, RoutedEventArgs e)
        {
            _tableName = "Glatze";
            GetDatabase();
        }
        //-------------------------------------------------------
        //Wasserzeichen Teil
        //-------------------------------------------------------
        private void GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == textBox.Name)
            {
                textBox.Text = null;
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Text = textBox.Name;
                textBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C9C9C"));
            }
        }


        private void friseurdaten_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ID.Foreground = new SolidColorBrush(Colors.Black);
            Name.Foreground = new SolidColorBrush(Colors.Black);
            Adresse.Foreground = new SolidColorBrush(Colors.Black);
            Tel.Foreground = new SolidColorBrush(Colors.Black);
            Min.Foreground = new SolidColorBrush(Colors.Black);
            Max.Foreground = new SolidColorBrush(Colors.Black);
            PLZ.Foreground = new SolidColorBrush(Colors.Black);
            Bewertung.Foreground = new SolidColorBrush(Colors.Black);
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            _selectedRow = dr;
            if (dr != null)
            {
                ID.Text = dr["ID"].ToString();
                Name.Text = dr["Namen"].ToString();
                Adresse.Text = dr["Adresse"].ToString();
                Tel.Text = dr["Tel"].ToString();
                Min.Text = dr["min"].ToString();
                Max.Text = dr["max"].ToString();
                PLZ.Text = dr["PLZ"].ToString();

                Bewertung.Text = dr["bewertung"].ToString();
            }
        }
        private void einfuegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"frische seiten.accdb\"");
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = _connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT MAX(ID) FROM " + _tableName;
                int lastID = (int)command.ExecuteScalar();
                for(int x=0; x == lastID; x++)
                {

                }
                int newID = lastID + 1;

                command.CommandText = "INSERT INTO " + _tableName + " ([ID], [Namen], [Adresse], [Tel], [Min], [Max], [PLZ], [Bewertung]) VALUES (@ID, @Namen, @Adresse, @Tel, @Min, @Max, @PLZ, @Bewertung)";
                try
                {
                    if (ID.Text == null || ID.Text == "ID")
                    {
                        command.Parameters.AddWithValue("@ID", newID);
                    }
                }
                catch
                {
                    command.Parameters.AddWithValue("@ID", newID);
                }                
                command.Parameters.AddWithValue("@Namen", Name.Text);
                command.Parameters.AddWithValue("@Adresse", Adresse.Text);
                command.Parameters.AddWithValue("@Tel", Tel.Text);
                command.Parameters.AddWithValue("@Min", Min.Text);
                command.Parameters.AddWithValue("@Max", Max.Text);
                command.Parameters.AddWithValue("@PLZ", PLZ.Text);
                command.Parameters.AddWithValue("@bewertung", Bewertung.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Record inserted successfully");
                GetDatabase();
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection?.Close();
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedRow == null)
                {
                    MessageBox.Show("Please select a row to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (_connection == null)
                {
                    _connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"frische seiten.accdb\"");
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var command = new OleDbCommand
                {
                    Connection = _connection,
                    CommandText = $"DELETE FROM {_tableName} WHERE ID = @ID"
                };
                command.Parameters.AddWithValue("@ID", _selectedRow["ID"]);
                command.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully");
                ClearControls();
                GetDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection?.Close();
            }
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedRow == null)
                {
                    MessageBox.Show("Please select a row to update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (_connection == null)
                {
                    _connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=frische seiten.accdb");
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var command = new OleDbCommand
                {
                    Connection = _connection,
                    CommandText = $"UPDATE {_tableName} SET [Namen] = @Name, [Adresse] = @Adresse, [Tel] = @Tel, [Min] = @Min, [Max] = @Max, [PLZ] = @PLZ, [Bewertung] = @Bewertung WHERE ID = @ID"
                };
                command.Parameters.AddWithValue("@Namen", Name.Text);
                command.Parameters.AddWithValue("@Adresse", Adresse.Text);
                command.Parameters.AddWithValue("@Tel", Tel.Text);
                command.Parameters.AddWithValue("@Min", Min.Text);
                command.Parameters.AddWithValue("@Max", Max.Text);
                command.Parameters.AddWithValue("@PLZ", PLZ.Text);
                command.Parameters.AddWithValue("@bewertung", Bewertung.Text);
                command.Parameters.AddWithValue("@ID", _selectedRow["ID"]);
                command.ExecuteNonQuery();
                MessageBox.Show("Record updated successfully");
                ClearControls();
                GetDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection?.Close();
            }

        }
        private void ClearControls()
        {
            ID.Text = "";
            Name.Text = "";
            Adresse.Text = "";
            Tel.Text = "";
            Min.Text = "";
            Max.Text = "";
            PLZ.Text = "";
            Bewertung.Text = "";
            _selectedRow = null;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                MessageBox.Show("Please enter a search term", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_connection == null)
                {
                    _connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=frische seiten.accdb");
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var command = new OleDbCommand
                {
                    Connection = _connection,
                    CommandText = $"SELECT * FROM {_tableName} WHERE Namen LIKE '%'+@Search+'%' OR Adresse LIKE '%'+@Search+'%' OR Tel LIKE '%'+@Search+'%' OR PLZ LIKE '%'+@Search+'%'"
                };
                command.Parameters.AddWithValue("@Search", Search.Text);
                var adapter = new OleDbDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                friseurdaten.ItemsSource = dataTable.DefaultView;
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection?.Close();
            }
        }

        
    }
}
