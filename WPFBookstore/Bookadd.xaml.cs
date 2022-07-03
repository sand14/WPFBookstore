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
using System.Windows.Shapes;
using Npgsql;

namespace WPFBookstore
{
    /// <summary>
    /// Interaction logic for Bookadd.xaml
    /// </summary>
    public partial class Bookadd : Window
    {
        public Bookadd()
        {
            InitializeComponent();
            for(int i=1;i<=50;i++)
            {
                Quantitybox.Items.Add(i);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void addbook(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection("Host = abul.db.elephantsql.com; Username = kbfzlkun; Password = ECwzVivOITJU5d0yDNztrGvMMx_HovNS; Database = kbfzlkun"))
                {
                    await conn.OpenAsync();
                    string sql = "INSERT INTO BOOKS (name,author,quantity) VALUES (@name,@author,@quantity)";
                    await using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", Bookname.Text);
                        cmd.Parameters.AddWithValue("@author", Bookauthor.Text);
                        cmd.Parameters.AddWithValue("@quantity", Quantitybox.SelectedValue);

                        await cmd.ExecuteNonQueryAsync();
                        MessageBox.Show("Book added successfully");


                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
