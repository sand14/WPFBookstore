using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
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
    /// Interaction logic for BooksList.xaml
    /// </summary>
    public partial class BooksList : Window
    {
        public BooksList()
        {
            InitializeComponent();
            try
            {
                using (var conn = new NpgsqlConnection("Host = localhost; Username = postgres; Password = password1; Database = WPFBookstore"))
                {
                    conn.OpenAsync();
                    string sql = "SELECT * from books";
                    using NpgsqlDataAdapter rdr = new NpgsqlDataAdapter(sql, conn);
                    DataTable t = new DataTable();
                    rdr.Fill(t);
                    dataGrid1.ItemsSource = t.DefaultView;
                    conn.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
