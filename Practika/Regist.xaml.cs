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

namespace Practika
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Page
    {
        public Regist()
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
            char[] chars1 = "!@#$%^".ToCharArray();
            char[] chars2 = "1234567890".ToCharArray();
            char[] chars3 = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
            if (Logn.Text != "" & Pass.Text != "" & Logn.Text != "Логин")
            {
                User user = MainWindow.DB.User.Where(u => u.Login == Logn.Text).ToList().FirstOrDefault();
                if (user == null)
                {
                    if (Pass.Text.Length >= 6 & Pass.Text.IndexOfAny(chars1) != -1 & Pass.Text.IndexOfAny(chars2) != -1 & Pass.Text.IndexOfAny(chars3) != -1)
                    {
                        User user1 = new User();
                        user1.Login = Logn.Text;
                        user1.Password = Pass.Text;
                        user1.RoleId = 2;
                        MainWindow.DB.User.Add(user1);
                        try
                        {
                            MainWindow.DB.SaveChanges();
                            MessageBox.Show("Информация успешно сохранена!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                        NavigationService.GoBack();
                    }
                    else InfoXaml.Text = "Пароль не соотвествует требованиям";
                }
                else InfoXaml.Text = "Такой логин уже есть!";
            }
            else InfoXaml.Text = "Заполните все поля!";
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}