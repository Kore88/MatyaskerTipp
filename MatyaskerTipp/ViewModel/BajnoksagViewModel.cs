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

        public void BajnoksagSzerkesztes(int idx)
        {
            BajnoksagSzerkesztesWindow window = new BajnoksagSzerkesztesWindow(this, idx);
            window.Show();
        }

        public void UjBajnoksag()
        {
            UjBajknoksagWindow window = new UjBajknoksagWindow(this);
            window.Show();
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
