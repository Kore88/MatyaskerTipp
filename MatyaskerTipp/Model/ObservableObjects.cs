using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MatyaskerTipp.Model
{
    public class ObservableObjects : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
