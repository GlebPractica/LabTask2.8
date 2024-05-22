using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task1.ViewModel;

namespace Task1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            DataContext = new MainMenuVM();

            if (DataContext is MainMenuVM vm)
            {
                vm.OpenShopWindow += Vm_OpenShopWindow;
                vm.OpenCartWindow += Vm_OpenCartWindow;
            }
        }

        private void OpenWindow(Button openBttn, Window openWindow)
        {
            if (openBttn == null)
                return;

            openBttn.IsEnabled = false;

            openWindow.ShowDialog();

            openBttn.IsEnabled = true;
        }

        private void Vm_OpenCartWindow(object? sender, bool e)
        {
            if (e)
            {
                OpenWindow(BttnOpenCart, new MyCart());
            }
        }

        private void Vm_OpenShopWindow(object? sender, bool e)
        {
            if (e)
            {
                OpenWindow(BttnOpenShop, new Shop());
            }
        }
    }
}