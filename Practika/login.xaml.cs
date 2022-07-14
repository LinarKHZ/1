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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;

namespace Practika
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }
        private void Logn_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Logn.Text == "Логин")
            {
                Logn.Text = "";             
            }
        }

        private void Logn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Logn.Text == "")
                Logn.Text = "Логин";
        }
        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "Пароль")
            {
                Pass.Text = "";
            }
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "")
                Pass.Text = "Пароль";
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            InfoXaml.Text = "";
            if (Logn.Text != "")
            {
                User user = MainWindow.DB.User.Where(u => u.Login == Logn.Text).ToList().FirstOrDefault();
                if (user != null)
                {
                    if (Equals(user.Password, Pass.Text))
                    {
                        InfoXaml.Text = "Правильный!";
                        DataContext = user;
                        if (user.RoleId == 1)
                        {
                            this.NavigationService.Navigate(new Admin.Products(user.Id));
                        }
                        if (user.RoleId == 2)
                        {
                            this.NavigationService.Navigate(new User1.prod(user.Id));
                        }
                        if (user.RoleId == 3)
                        {
                            this.NavigationService.Navigate(new Prov.ListP(user.Id));
                        }

                    }
                    else InfoXaml.Text = "Логин или пароль не правильный!";
                }
                else InfoXaml.Text = "Логин или пароль не правильный!";
            }
            else InfoXaml.Text = "Заполните все поля!";
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Regist());
        }
    }
}
