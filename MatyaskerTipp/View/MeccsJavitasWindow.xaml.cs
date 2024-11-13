using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Match = MatyaskerTipp.Model.Match;

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for MeccsJavitasWindow.xaml
    /// </summary>
    public partial class MeccsJavitasWindow : Window
    {

        private Match match;
        public MeccsJavitasWindow(int matchId)
        {
            InitializeComponent();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectedMatch, conn);
                cmd.Parameters.AddWithValue("@idx", matchId);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    match = new Match
                    {
                        Id = dr.GetInt32("id"),
                        HomeName = dr.GetString("homeName"),
                        GuestName = dr.GetString("guestName"),
                        Date = dr.GetDateTime("date"),
                        HomeGoals = dr.GetInt32("homeGoals"),
                        GuestGoals = dr.GetInt32("guestGoals"),
                        IsAvailable = dr.GetBoolean("isAvailable")
                    };
                }

                conn.Close();
            }
            catch  (Exception ex) {MessageBox.Show(ex.Message);}
            lbMeccs.Content = match.HomeName +" VS "+match.GuestName;
            lbGuestName.Content = match.GuestName;
            lbHomeName.Content = match.HomeName;
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnJavitas_Click(object sender, RoutedEventArgs e)
        {
            if(tbxGuestGoals.Text != "" && tbxHomeGoals.Text != "")
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(SqlCommans.updateMatch, conn);
                    cmd.Parameters.AddWithValue("@homeGoals",int.Parse(tbxHomeGoals.Text));
                    cmd.Parameters.AddWithValue("@guestGoals", int.Parse(tbxGuestGoals.Text));
                    cmd.Parameters.AddWithValue("@isAvailable", 0);
                    cmd.Parameters.AddWithValue("@id", match.Id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sikeres javítás!");

                    conn.Close();
                    this.Close();
                }
                catch(Exception ex) {  MessageBox.Show(ex.Message);}
            }
            else
            {
                MessageBox.Show("Mindkét eredmény kitöltése kötelező!");
            }
        }
    }
}
