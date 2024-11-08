using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
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

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for UjBajknoksagWindow.xaml
    /// </summary>
    public partial class UjBajknoksagWindow : Window
    {
        public UjBajknoksagWindow()
        {
            InitializeComponent();
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnHozzad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(tbxBajnoksagNev.Text != "" && dpKezdo.SelectedDate.HasValue
                    && dpVege.SelectedDate.HasValue && 
                    (dpKezdo.SelectedDate < dpVege.SelectedDate))
                {

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(SqlCommans.addContest, conn);
                    cmd.Parameters.AddWithValue("@name", tbxBajnoksagNev.Text);
                    cmd.Parameters.AddWithValue("@startDate", dpKezdo.SelectedDate);
                    cmd.Parameters.AddWithValue("@endDate", dpVege.SelectedDate);
                    cmd.Parameters.AddWithValue("@isOpened", 0);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("A bajnokság sikeresen létrehozva!");
                }
                else
                {
                    if (dpKezdo.SelectedDate > dpVege.SelectedDate) 
                    {
                        MessageBox.Show("A kezdődátum nem lehet kisebb mint a végdátum!");
                    }
                    else
                    {
                        MessageBox.Show("Minden adat kitöltése kötelező");
                    }

                }

            }
            catch (Exception ex) {MessageBox.Show(ex.Message); }
        }
    }
}
