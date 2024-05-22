using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1.Model;

namespace Task1.ViewModel
{
    public class MainMenuVM
    {
        private Model.ApplicationContext? _context;

        public MainMenuVM()
        {
            _context = new Model.ApplicationContext();

            _context.Database.EnsureCreated();
        }

        public event EventHandler<bool>? OpenShopWindow;
        public event EventHandler<bool>? OpenCartWindow;

        private RelayCommand? goToShop;
        private RelayCommand? goToCart;

        public RelayCommand GoToShop
        {
            get
            {
                return goToShop ??
                    (goToShop = new RelayCommand(o =>
                    {
                        OpenShopWindow?.Invoke(this, true);
                    }));
            }
        }

        public RelayCommand GoToCart
        {
            get
            {
                return goToCart ??
                    (goToCart = new RelayCommand(o =>
                    {
                        OpenCartWindow?.Invoke(this, true);
                    }));
            }
        }
    }
}
