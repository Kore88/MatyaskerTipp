using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.ViewModel;
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
        private MeccsekViewModel mvm;
        private JavitasViewModel jvm;

        public MeccsJavitasWindow(int matchId, MeccsekViewModel mevm)
        {
            InitializeComponent();
            mvm = mevm;
            jvm = mvm.jvm;
            jvm.GetMatchByID(matchId);  // Megkapjuk a match adatokat a jvm-en keresztül

            // Az jvm.match objektumot használjuk itt
            lbMeccs.Content = jvm.match.HomeName + " VS " + jvm.match.GuestName;
            lbGuestName.Content = jvm.match.GuestName;
            lbHomeName.Content = jvm.match.HomeName;
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnJavitas_Click(object sender, RoutedEventArgs e)
        {
            if (tbxGuestGoals.Text != "" && tbxHomeGoals.Text != "")
            {
                // Használjuk a jvm.match objektumot
                jvm.Javitas(tbxHomeGoals.Text, tbxGuestGoals.Text, jvm.match.Id);
                this.Close();
            }
            else
                MessageBox.Show("Mindkét eredmény kitöltése kötelező!");
        }
    }
}
