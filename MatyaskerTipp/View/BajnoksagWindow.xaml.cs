using MatyaskerTipp.Model;
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
    /// Interaction logic for BajnoksagWindow.xaml
    /// </summary>
    public partial class BajnoksagWindow : Window
    {
        Contest contest;

        public BajnoksagWindow()
        {
            InitializeComponent();
            contest = new Contest();
            cbxBajnoksagok.ItemsSource = contest.GetAllContestName();
        }

        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                BajnoksagSzerkesztesWindow window = new BajnoksagSzerkesztesWindow(cbxBajnoksagok.SelectedIndex+1);
                window.Show();
            }

        }

        private void btnUjBajnoksag_Click(object sender, RoutedEventArgs e)
        {
            UjBajknoksagWindow window = new UjBajknoksagWindow();
            window.Show();
        }
    }
}
