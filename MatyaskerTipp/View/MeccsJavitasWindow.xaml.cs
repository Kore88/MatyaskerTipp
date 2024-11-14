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
        private Match match;
        public MeccsJavitasWindow(int matchId, MeccsekViewModel mevm)
        {
            InitializeComponent();
            mvm = mevm;
            jvm = mvm.jvm;
            jvm.GetMatchByID(matchId);
            lbMeccs.Content = jvm.hname + " VS " + jvm.gname;
            lbGuestName.Content = jvm.gname;
            lbHomeName.Content = jvm.hname;
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnJavitas_Click(object sender, RoutedEventArgs e)
        {
            if (tbxGuestGoals.Text != "" && tbxHomeGoals.Text != "")
            {
                jvm.Javitas(tbxHomeGoals.Text, tbxGuestGoals.Text);
                this.Close();
            }
            else
                MessageBox.Show("Mindkét eredmény kitöltése kötelező!");
        }

    }
}
