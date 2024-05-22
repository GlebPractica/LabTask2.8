using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Task1.Model;

namespace Task1.ViewModel
{
    public class MyCartVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private ApplicationContext _context;

        public ObservableCollection<Cart> CartItems { get; set; }

        public MyCartVM()
        {
            _context = new ApplicationContext();
            CartItems = new ObservableCollection<Cart>(_context.Carts.Include(c => c.Shop).ToList());
        }
    }
}
