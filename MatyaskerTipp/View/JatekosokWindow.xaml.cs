using MatyaskerTipp.Model;
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
    /// Interaction logic for JatekosokWindow.xaml
    /// </summary>
    public partial class JatekosokWindow : Window
    {
        public JatekosokWindow()
        {
            InitializeComponent();
            dgJatekosok.ItemsSource = LoadNonAdminUsers();
        }

        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            var checkBoxColumn = dgJatekosok.Columns
                .OfType<DataGridCheckBoxColumn>()
                .FirstOrDefault();

            if (checkBoxColumn != null)
            {
                var isCurrentlyReadOnly = checkBoxColumn.IsReadOnly;
                checkBoxColumn.IsReadOnly = !isCurrentlyReadOnly;
            }
        }

        private void btnJovahagyas_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();


                foreach (var item in dgJatekosok.Items)
                    {
                        if (item is User user)
                        {

                            using (MySqlCommand cmd = new MySqlCommand(SqlCommans.updateStatus, conn))
                            {
                                cmd.Parameters.AddWithValue("@isActive", user.IsActive ? 1 : 0);
                                cmd.Parameters.AddWithValue("@id", user.Id);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                MessageBox.Show("A változások mentésre kerültek!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}");
            }
        }

        private List<User> LoadNonAdminUsers()
        {
            List<User> users = new List<User>();

            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectAllUsers, conn);

                MySqlDataReader dr = cmd.ExecuteReader();
                        
                            while (dr.Read())
                            {
                                users.Add(new User
                                {
                                    Id = dr.GetInt32("id"),
                                    UserName = dr.GetString("username"),
                                    RealName = dr.GetString("realname"),
                                    IsActive = dr.GetBoolean("isActive")
                                });
                            }
                        
                    conn.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a felhasználók betöltése során: {ex.Message}");
            }

            return users;
        }
    }
}
