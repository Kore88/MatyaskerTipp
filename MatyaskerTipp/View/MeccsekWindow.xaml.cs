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
using ZstdSharp.Unsafe;


namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for MeccsekWindow.xaml
    /// </summary>
    public partial class MeccsekWindow : Window
    {
        private MeccsekViewModel match;
        public MeccsekWindow()
        {
            InitializeComponent();
            match = new MeccsekViewModel();
            lbxMeccsek.ItemsSource = match.items;
            match.PropertyChanged += PropertyChanged;
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
                match.Javitas(lbxMeccsek.SelectedItem.ToString());
            }
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lbxMeccsek.ItemsSource = match.items;
            this.UpdateLayout();
        }
    }
}
