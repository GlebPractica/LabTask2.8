using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Model
{
    public class Shop : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private int _id;
        private string _nameProd;
        private int _countProd;
        private int _priceProd;

        public int Id { 
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string NameProd
        {
            get => _nameProd;
            set
            {
                _nameProd = value;
                OnPropertyChanged();
            }
        }

        public int CountProd
        {
            get => _countProd;
            set
            {
                _countProd = value;
                OnPropertyChanged();
            }
        }

        public int PriceProd
        {
            get => _priceProd;
            set
            {
                _priceProd = value;
                OnPropertyChanged();
            }
        }
    }
}
