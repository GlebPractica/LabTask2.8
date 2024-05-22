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
    public class ShopVM : INotifyPropertyChanged
    {
        public event EventHandler<string>? showSomeMessage;
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private ApplicationContext? _context;

        public ObservableCollection<Shop> Shops { get; set; } = null!;
        public ObservableCollection<Cart> CartItems { get; set; } = null!;

        private Shop? _selectedShop;

        public Shop? SelectedShop
        {
            get => _selectedShop;
            set
            {
                _selectedShop = value;
                OnPropertyChanged("SelectedShop");
            }
        }

        private string _newProductName;
        public string NewProductName
        {
            get => _newProductName;
            set
            {
                _newProductName = value;
                OnPropertyChanged();
            }
        }

        private int _newProductCount;
        public int NewProductCount
        {
            get => _newProductCount;
            set
            {
                _newProductCount = value;
                OnPropertyChanged();
            }
        }

        private int _newProductPrice;
        public int NewProductPrice
        {
            get => _newProductPrice;
            set
            {
                _newProductPrice = value;
                OnPropertyChanged();
            }
        }

        public ShopVM()
        {
            _context = new ApplicationContext();
            Shops = new ObservableCollection<Shop>(_context.Shops.ToList());
            CartItems = new ObservableCollection<Cart>(_context.Carts.ToList());
        }

        private RelayCommand? _addToCart;
        private RelayCommand? _addProduct;
        private RelayCommand? _deleteProduct;

        public RelayCommand DeleteProduct
        {
            get
            {
                return _deleteProduct ??
                    (_deleteProduct = new RelayCommand(o =>
                    {
                        DeleteProductMet();
                    }));
            }
        }

        private void DeleteProductMet()
        {
            if (SelectedShop == null)
            {
                showSomeMessage?.Invoke(this, "Нечего удалять");
                return;
            }

            Shops.Remove(SelectedShop);
            _context?.Shops.Remove(SelectedShop);
            _context?.SaveChanges();
            SelectedShop = null;
        }

        public RelayCommand? AddProduct
        {
            get
            {
                return _addProduct ??
                    (_addProduct = new RelayCommand(o =>
                    {
                        AddNewProduct();
                    }));
            }
        }

        private void AddNewProduct()
        {
            var newProduct = new Shop
            {
                NameProd = NewProductName,
                CountProd = NewProductCount,
                PriceProd = NewProductPrice
            };

            Shops.Add(newProduct);
            _context?.Shops.Add(newProduct);
            _context?.SaveChanges();
            showSomeMessage?.Invoke(this, "Продукт добавлен в магазин");
        }

        public RelayCommand AddToCart
        {
            get
            {
                return _addToCart ??
                    (_addToCart = new RelayCommand(o =>
                    {
                        AddItemToCart();
                    }));
            }
        }

        private void AddItemToCart()
        {
            if (SelectedShop == null)
            {
                showSomeMessage?.Invoke(this, "Продукт не выбран");
                return;
            }

            if (SelectedShop.CountProd < 1)
            {
                showSomeMessage?.Invoke(this, "Продукты закончились");
                return;
            }

            var cartItem = CartItems.FirstOrDefault(c => c.IdS == SelectedShop.Id);

            if (cartItem == null)
            {
                cartItem = new Cart()
                {
                    IdS = SelectedShop.Id,
                    Count = 1,
                    Shop = SelectedShop
                };
                CartItems.Add(cartItem);
                _context?.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
                _context?.Carts.Update(cartItem);
            }

            SelectedShop.CountProd--;

            _context?.SaveChanges();
        }
    }
}
