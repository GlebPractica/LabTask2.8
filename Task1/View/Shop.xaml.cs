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
using Task1.ViewModel;

namespace Task1.View
{
    /// <summary>
    /// Логика взаимодействия для Shop.xaml
    /// </summary>
    public partial class Shop : Window
    {
        public Shop()
        {
            InitializeComponent();
            DataContext = new ShopVM();

            if (DataContext is ShopVM vm)
            {
                vm.showSomeMessage += Vm_showSomeMessage;
            }
        }

        private void Vm_showSomeMessage(object? sender, string e)
        {
            MessageBox.Show(e);
        }
    }
}
