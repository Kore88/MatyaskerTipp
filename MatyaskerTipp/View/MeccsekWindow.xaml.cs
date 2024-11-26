using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.ViewModel;
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
        private MeccsekViewModel mvm;
        public MeccsekWindow()
        {
            InitializeComponent();
            mvm = new MeccsekViewModel();
            match = new Match();
            lbxMeccsek.ItemsSource = mvm.items;
            mvm.PropertyChanged += PropertyChanged;
        }

        private void btnHozzaad_Click(object sender, RoutedEventArgs e)
        {
            mvm.Hozzaad();
        }

        private void btnJavitas_Click(object sender, RoutedEventArgs e)
        {
            if (lbxMeccsek.SelectedIndex != -1)
            {
                mvm.Javitas(lbxMeccsek.SelectedItem.ToString());
            }
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            mvm.items.Clear();
            mvm.items = match.GetAllNonCheckedMatches();
            lbxMeccsek.ItemsSource = mvm.items;
            this.UpdateLayout();
        }
    }
}
