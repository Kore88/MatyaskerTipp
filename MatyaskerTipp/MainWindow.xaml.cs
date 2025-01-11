using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.View;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using MatyaskerTipp.ViewModel;

namespace MatyaskerTipp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        { 
            InitializeComponent();

        }


        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                btnBejelentkezes_Click(sender, e);
            }
        }

        private void btnBejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectAllUser,conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    User admin = new User
                    {
                        UserName = dr["UserName"].ToString(),
                        Password = dr["Password"].ToString()
                    };

                    string hashedPassword = HashPassword(tbxJelszo.Password);
                    hashedPassword = hashedPassword.ToUpper();

                    if (admin.UserName == tbxFelhasznaloNev.Text && admin.Password == hashedPassword)
                    {
                        var selectMenuWindow = new SelectMenuWindow();
                        selectMenuWindow.Show();
                        this.Close();
                        conn.Close();
                        return;
                    }
                }
                if (tbxFelhasznaloNev.Text == "" || tbxJelszo.Password == "")
                {
                    MessageBox.Show("A felhasználónév és a jelszó kitöltése kötelező!");
                }
                else
                {
                    MessageBox.Show("Téves felhasználónév vagy jelszó!");
                }
                conn.Close();
            }
            catch (Exception ex)
            
                { MessageBox.Show(ex.Message); }

        }

        private string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

    }
}