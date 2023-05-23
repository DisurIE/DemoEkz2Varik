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

namespace DemoEkz2Varik
{
    /// <summary>
    /// Логика взаимодействия для AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Window
    {
        private Product _currentProduct = new Product();
        bool isUpdate = false;
        public AddEditProduct(Product selectedProduct)
        {

            if(selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }

            InitializeComponent();
            DataContext = _currentProduct;
        }

        private void AddEditButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductArticleNumber))
            {
                errors.AppendLine("Пусто 1");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductName))
            {
                errors.AppendLine("Пусто 2");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductDescription))
            {
                errors.AppendLine("Пусто 3");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductCategory))
            {
                errors.AppendLine("Пусто 4");
            }
            
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductManufacturer))
            {
                errors.AppendLine("Пусто 6");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductCost.ToString()))
            {
                errors.AppendLine("Пусто 7");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductDiscountAmount.ToString()))
            {
                errors.AppendLine("Пусто 8");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductQuantityInStock.ToString()))
            {
                errors.AppendLine("Пусто 9");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductStatus.ToString()))
            {
                errors.AppendLine("Пусто 10");
            }
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var currentProducts = TradeEntities.getContext().Product.ToList();
            foreach(Product p in currentProducts)
            {
                if (_currentProduct.ProductArticleNumber == p.ProductArticleNumber)
                {
                    isUpdate = true;
                    break;
                }
            }
            if (!isUpdate)
            {
                TradeEntities.getContext().Product.Add(_currentProduct);
            }
            try
            {
                TradeEntities.getContext().SaveChanges();
                MessageBox.Show("Добавлено");
                AdminPanelWindow ad = new AdminPanelWindow();
                ad.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanel = new AdminPanelWindow();
            adminPanel.Show();
            this.Close();
        }
    }
}
