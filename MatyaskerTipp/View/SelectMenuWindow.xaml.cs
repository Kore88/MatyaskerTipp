using MatyaskerTipp.ViewModel;
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
            var existingWindow = Application.Current.Windows.OfType<TabellaWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new TabellaWindow();
                window.Left = this.Left + this.Width + 10;
                window.Top = 0;
                window.Show();
            }
            else
            {
                MessageBox.Show("A tabella ablak már meg van nyitva.");
            }
        }

        private void btnJatekosok_Click(object sender, RoutedEventArgs e)
        {
            var existingWindow = Application.Current.Windows.OfType<JatekosokWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new JatekosokWindow();
                window.Left = this.Left - window.Width + this.Width;
                window.Top = window.Top = System.Windows.SystemParameters.PrimaryScreenHeight - window.Height - 50;
                window.Show();
            }
            else
            {
                MessageBox.Show("A játékosok ablak már meg van nyitva.");
            }
        }

        private void btnBajnoksag_Click(object sender, RoutedEventArgs e)
        {


            var existingWindow = Application.Current.Windows.OfType<BajnoksagWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new BajnoksagWindow();
                window.Left = this.Left - window.Width -10;
                window.Top = this.Top;
                window.Show();
            }
            else
            {
                MessageBox.Show("A bajnokság ablak már meg van nyitva.");
            }

        }

        private void btnPontrendszer_Click(object sender, RoutedEventArgs e)
        {
            var existingWindow = Application.Current.Windows.OfType<PontrendszerWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new PontrendszerWindow();
                window.Left = this.Left + this.Width + 10;
                window.Top = this.Top + 350 + 10;
                window.Show();
            }
            else
            {
                MessageBox.Show("A pontrendszer ablak már meg van nyitva.");
            }
        }

        private void btnKielepes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMeccsek_Click(object sender, RoutedEventArgs e)
        {


            var existingWindow = Application.Current.Windows.OfType<MeccsekWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new MeccsekWindow();
                window.Left = this.Left + this.Width + 10;
                window.Top = this.Top;
                window.Show();
            }
            else
            {
                MessageBox.Show("A meccsek ablak már meg van nyitva.");
            }


        }
    }
}
