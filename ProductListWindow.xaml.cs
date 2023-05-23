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
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public int countProducts = 0;
        public ProductListWindow()
        {
            InitializeComponent();
            User currentUser = TradeEntities.getUser();
            string fullName = currentUser.UserSurname + " " + currentUser.UserName + " " + currentUser.UserPatronymic;
            FIO.Text = fullName;

            var currentProducts = TradeEntities.getContext().Product.ToList();
            LViewProducts.ItemsSource = currentProducts;
            countProducts = currentProducts.Count();
            //sex
            SizeOfDB.Text = countProducts + " из " + countProducts;

            currentProducts.Insert(0, new Product
            {
                ProductManufacturer = "Все производители"
            });

            ComboManuf.ItemsSource = currentProducts;
            ComboManuf.SelectedIndex = 0;
        }

        private void UpdateProducts()
        {
            var currentProducts = TradeEntities.getContext().Product.ToList();
            if(ComboManuf.SelectedIndex > 0)
            {
                currentProducts = currentProducts.Where(p => p == ComboManuf.SelectedItem).ToList();
            }
            if(ComboSortPrice.SelectedIndex > 0)
            {
                if(ComboSortPrice.SelectedIndex == 1)
                {
                    currentProducts.Sort(delegate (Product x, Product y) {
                        return x.ProductCost.CompareTo(y.ProductCost);
                    });
                }
                if (ComboSortPrice.SelectedIndex == 2)
                {
                    currentProducts.Sort(delegate (Product x, Product y) {
                        return y.ProductCost.CompareTo(x.ProductCost);
                    });
                }
            }
            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            SizeOfDB.Text = currentProducts.Count() + " из " + countProducts;
            LViewProducts.ItemsSource = currentProducts;

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void ComboManuf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void ComboSortPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }
    }
}
