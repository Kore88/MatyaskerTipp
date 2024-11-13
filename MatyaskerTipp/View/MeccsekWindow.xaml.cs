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
using ZstdSharp.Unsafe;


namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for MeccsekWindow.xaml
    /// </summary>
    public partial class MeccsekWindow : Window
    {
        private Match match;
        public MeccsekWindow()
        {
            InitializeComponent();
            match = new Match();
            lbxMeccsek.ItemsSource = match.GetAllNonCheckedMatches();
        }

        private void btnHozzaad_Click(object sender, RoutedEventArgs e)
        {
            var window = new UjMeccsWindow();
            window.Show();
        }

        private void btnJavitas_Click(object sender, RoutedEventArgs e)
        {

            if (lbxMeccsek.SelectedIndex != -1)
            {
                int meccsId = int.Parse(lbxMeccsek.SelectedItem.ToString().Substring(0, 1));
                MeccsJavitasWindow window = new MeccsJavitasWindow(meccsId);
                window.Show();
            }
        }
    }
}
