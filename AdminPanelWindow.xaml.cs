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
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
            User currentUser = InitializingUser();
            var currentProducts = TradeEntities.getContext().Product.ToList();
            DGridProducts.ItemsSource = currentProducts;
        }

        private User InitializingUser()
        {
            User currentUser = TradeEntities.getUser();
            string fullName = currentUser.UserSurname + " " + currentUser.UserName + " " + currentUser.UserPatronymic;
            FIO.Text = fullName;
            return currentUser;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }
        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TradeEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProducts.ItemsSource = TradeEntities.getContext().Product.ToList();
            }
        }

        private void addEditButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditProduct ad = new AddEditProduct(null);
            ad.Show();
            this.Close();
        }

        private void BtnEgit_Click(object sender, RoutedEventArgs e)
        {
            AddEditProduct ad = new AddEditProduct((sender as Button).DataContext as Product);
            ad.Show();
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var productForRemoving = DGridProducts.SelectedItems.Cast<Product>().ToList();
            if (MessageBox.Show($"Вы точно хотете удалить следующие {productForRemoving.Count()} элеменов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeEntities.getContext().Product.RemoveRange(productForRemoving);
                    TradeEntities.getContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridProducts.ItemsSource = TradeEntities.getContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ListProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow win = new ProductListWindow();
            win.Show();
            this.Close();
        }
    }
}
