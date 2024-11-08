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
    /// Interaction logic for SelectMenuWindow.xaml
    /// </summary>
    public partial class SelectMenuWindow : Window
    {
        public SelectMenuWindow()
        {
            InitializeComponent();
        }


        private void btnTabella_Click(object sender, RoutedEventArgs e)
        {
            var window = new TabellaWindow();
            window.Show();
        }

        private void btnJatekosok_Click(object sender, RoutedEventArgs e)
        {
            var window = new JatekosokWindow();
            window.Show();
        }

        private void btnBajnoksag_Click(object sender, RoutedEventArgs e)
        {
            var window = new BajnoksagWindow();
            window.Show();
        }

        private void btnPontrendszer_Click(object sender, RoutedEventArgs e)
        {
            var window = new PontrendszerWindow();
            window.Show();
        }

        private void btnKielepes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMeccsek_Click(object sender, RoutedEventArgs e)
        {
            var window = new MeccsekWindow();
            window.Show();
        }
    }
}
