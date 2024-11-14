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
        public void Hozzaad()
        {
            var window = new UjMeccsWindow();
            window.Show();
        }

        public void Javitas(string name)
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
            MeccsJavitasWindow window = new MeccsJavitasWindow(meccsId, this);
            window.Show();
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            items.Clear();
            items = match.GetAllNonCheckedMatches();
            Notify();
        }
    }
}
