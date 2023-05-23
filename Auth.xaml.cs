using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; // Для работы с элементом управления Canvas
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DemoEkz2Varik
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>

    enum RoleEnum
    {
        Admin = 1,
        Manager = 2,
        User = 3
    }


    public partial class Auth : Window
    {
        public bool isPasswordFalse = false;

        public Auth()
        {
            InitializeComponent();
        }


        private DispatcherTimer timer;

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10000); // интервал в миллисекундах
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            password.IsEnabled = true;
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = TradeEntities.getContext().User.FirstOrDefault(u => u.UserLogin == login.Text && u.UserPassword == password.Password);
            
            if(currentUser != null && currentUser.UserRole != (int)RoleEnum.Admin)
            {
                TradeEntities.setUser(currentUser);
                ProductListWindow productListWindow = new ProductListWindow();
                productListWindow.Show();
                this.Close();
            }
            else if (currentUser != null && currentUser.UserRole == (int)RoleEnum.Admin)
            {
                TradeEntities.setUser(currentUser);
                AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
                adminPanelWindow.Show();
                this.Close();
            }
            else if(!isPasswordFalse)
            {
                isPasswordFalse = true;
                MessageBox.Show("Логин или пароль неправильные");
            }
            else
            {
                MessageBox.Show("Логин или пароль неправильные вы забанены на 10 секунд");
                StartTimer();
                password.IsEnabled = false;
            }
        }

        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            this.Close();
        }
    }
}
