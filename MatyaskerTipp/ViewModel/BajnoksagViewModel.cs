using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.ViewModel
{
    public class BajnoksagViewModel : ObservableObjects
    {
        private Match match;
        public List<String> items;
        public BajnoksagViewModel()
        {
        }

        public void BajnoksagSzerkesztes(int idx, double l, double w, double t, double h)
        {
            var existingWindow = Application.Current.Windows.OfType<BajnoksagSzerkesztesWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new BajnoksagSzerkesztesWindow(this,idx);
                window.Left = l + w - window.Width;
                window.Top = t - window.Height - 10;
                window.Show();
            }
            else
            {
                MessageBox.Show("A bajnokság szerkesztése ablak már meg van nyitva.");
            }
        }

        public void UjBajnoksag(double l, double w, double t, double h)
        {
            var existingWindow = Application.Current.Windows.OfType<UjBajknoksagWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new UjBajknoksagWindow();
                window.Left = l - window.Width - 10;
                window.Top = t;
                window.Show();
            }
            else
            {
                MessageBox.Show("Az új bajnokság ablak már meg van nyitva.");
            }
        }
        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Notify();
        }

        public void InvokeNotify()
        {
            Notify();
        }

    }
}
