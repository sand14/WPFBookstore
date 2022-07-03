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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            try
            {
                using (var conn = new NpgsqlConnection("Host = abul.db.elephantsql.com; Username = kbfzlkun; Password = ECwzVivOITJU5d0yDNztrGvMMx_HovNS; Database = kbfzlkun"))
                {
                    await conn.OpenAsync();
                    string sql = "SELECT * from accounts where username='" + usernamebox.Text + "' and password='" + passwordbox.Text + "'";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    using NpgsqlDataReader rdr = cmd.ExecuteReader();
                   if(rdr.Read())
                    {
                        found = true;
                        MessageBox.Show("Login Successful");
                        Home homepage = new Home();
                        homepage.Show();
                        conn.CloseAsync();
                        Close();
                    }
                    if (found == false)
                    {
                        MessageBox.Show("Invalid username or password");
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
