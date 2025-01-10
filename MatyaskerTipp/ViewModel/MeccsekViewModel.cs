using K4os.Compression.LZ4.Internal;
using MatyaskerTipp.Model;
using MatyaskerTipp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.ViewModel
{
    public class MeccsekViewModel : ObservableObjects
    {
        private Match match;
        public JavitasViewModel jvm;
        public List<String> items;
        public MeccsekViewModel()
        {
            match = new Match();
            jvm = new JavitasViewModel();
            jvm.PropertyChanged += PropertyChanged;
            items = match.GetAllNonCheckedMatches();
        }


        public void Hozzaad(double l, double w, double t, double h)
        {
            var existingWindow = Application.Current.Windows.OfType<UjMeccsWindow>().FirstOrDefault();

            if (existingWindow == null)
            {     
                var hozzaAdWindow = new UjMeccsWindow();
                hozzaAdWindow.Left = l + w + 10;
                hozzaAdWindow.Top = t;
                hozzaAdWindow.Show();
            }
            else
            {
                MessageBox.Show("A meccs hozzáadás ablak már meg van nyitva.");
            }
        }

        public void Javitas(string name, double l, double w, double t, double h)
        {
            string theID = null;
            string selected = name;
            for (int i = 0; i < selected.Length; i++)
            {
                 if (Char.IsDigit(selected[i]))
                     theID += selected[i];
                 else
                     break;
            }
            int meccsId = int.Parse(theID);

            var existingWindow = Application.Current.Windows.OfType<MeccsJavitasWindow>().FirstOrDefault();

            if (existingWindow == null)
            {
                var window = new MeccsJavitasWindow(meccsId, this);
                window.Left = l + w + 10;
                window.Top = t + h + 10;
                window.Show();
            }
            else
            {
                MessageBox.Show("A meccs javító ablak már meg van nyitva.");
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
