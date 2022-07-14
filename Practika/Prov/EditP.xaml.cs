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
using Microsoft.Win32;

namespace Practika.Prov
{
    /// <summary>
    /// Логика взаимодействия для AddEditProduct.xaml
    /// </summary>
    public partial class EditP : Page
    {
        Product newProduct;
        //string photoProduct;
        int userId2;
        string photoProduct;
        public EditP(Product product, int userId)
        {
            InitializeComponent();
            newProduct = null;
            newProduct = product;
            this.userId2 = userId;
            DataContext = newProduct;
            TypeUnitBox.ItemsSource = MainWindow.DB.TypeUnit.ToList();
            Name1.Text = newProduct.Name;
            Photo1.Text = newProduct.Photo;
            ProductPhoto.Source = new BitmapImage(new Uri(newProduct.Photo));
            dataAdd.SelectedDate = newProduct.DataAdd;
            Comment.Text = newProduct.Comment;
            CountryBox.ItemsSource = MainWindow.DB.Country.ToList();


        }

        private void typeUnit_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TypeUnitBox.Text == "Единица измерения")
                TypeUnitBox.Text = "";
        }

        private void typeUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TypeUnitBox.Text == "")
                TypeUnitBox.Text = "Единица измерения";
        }

        private void Photo_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {


            //prod.Id = MainWindow.DB.Product.Count() + 10;
            newProduct.Name = Name1.Text;
            newProduct.Photo = Photo1.Text;
            newProduct.Comment = Comment.Text;
            newProduct.DataAdd = Convert.ToDateTime(dataAdd.Text);
            newProduct.TypeUnitId = TypeUnitBox.SelectedIndex + 1;
            newProduct.UserId = userId2;
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

        private void Esc(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Prov.ListP(userId2));
        }

        private void photoclick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите картинку";
            openFileDialog.Filter = "Image file (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                ProductPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            photoProduct = openFileDialog.FileName;
            Photo1.Text = photoProduct;
            Photo1.Opacity = 1;
        }

        private void CountryBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CountryBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
