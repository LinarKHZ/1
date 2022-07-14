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

namespace Practika.Prov
{
    public partial class ListP : Page
    {
        int userId1;
        List<Product> ProductList;
        public ListP(int userId)
        {
            InitializeComponent();
            listProduct.ItemsSource = MainWindow.DB.Product.OrderBy(i => i.Id).ToList();
            this.userId1 = userId;
            TypeUnitBox.ItemsSource = MainWindow.DB.TypeUnit.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Prov.AddP(null, userId1));
        }

        private void FilterSearch()
        {
            var itemsInfo = ProductList;

            if (Search.Text != "Название, комментарий..." && Search.Text != "")
            {
                itemsInfo = itemsInfo.Where(i => i.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.Comment.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            if (CurrentMonth.IsChecked == true)
                itemsInfo = itemsInfo.Where(p => p.DataAdd.Month == DateTime.Now.Month).OrderBy(p => p.Name).OrderBy(p => p.DataAdd).ToList();
            if (TypeUnitBox.SelectedItem != null)
                itemsInfo = itemsInfo.Where(p => p.TypeUnitId == (TypeUnitBox.SelectedItem as TypeUnit).Id).ToList();
            listProduct.ItemsSource = itemsInfo;
            if (itemsInfo == null)
                MessageBox.Show("Информация не найдена!");
            CountList.Content = $"{itemsInfo.Count()} из {ProductList.Count()}";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            FilterSearch();
        }

        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "Название, комментарий...")
            {
                Search.Text = "";
                Search.Foreground = Brushes.Black;
            }
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            if (Search.Text == "")
                Search.Text = "Название, комментарий...";
            else
            {
                Refresh();
            }
        }

        private void AllInfo_Click(object sender, RoutedEventArgs e)
        {
            listProduct.ItemsSource = MainWindow.DB.Product.OrderBy(i => i.Id).ToList();
            TypeUnitBox.SelectedItem = null;
            CountList.Content = $"{ProductList.Count()} из {ProductList.Count()}";
            Search.Text = "Название, комментарий...";
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) //редакт элемента
        {
            NavigationService.Navigate(new Prov.EditP((sender as ListViewItem).DataContext as Product, userId1));
        }

        private void InfoInMonth_Click(object sender, RoutedEventArgs e)
        {
            FilterSearch();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteProduct = listProduct.SelectedItems.Cast<Product>().ToList();
            if (listProduct.SelectedItem as Product != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    object p1 = MainWindow.DB.Product.RemoveRange(deleteProduct);
                    try
                    {
                        MainWindow.DB.SaveChanges();
                        MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                        ProductList = MainWindow.DB.Product.OrderBy(p => p.Name).OrderBy(p => p.DataAdd).ToList();
                        listProduct.ItemsSource = ProductList;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else MessageBox.Show("Выберите элемент для удаления!");
        }

        private void TypeUnitBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSearch();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductList = MainWindow.DB.Product.OrderBy(p => p.Name).OrderBy(p => p.DataAdd).ToList();
                MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listProduct.ItemsSource = ProductList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (listProduct != null)
            {

                CollectionViewSource.GetDefaultView(listProduct.ItemsSource).Refresh();
            }
        }

        private void Refresh()
        {
            var filtered = MainWindow.DB.Product.ToList();
            var text = (Search as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                filtered = filtered.Where(i => i.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.Comment.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            if (listProduct != null)
            {
                listProduct.ItemsSource = filtered;
                CountList.Content = $"{filtered.Count()} из {ProductList.Count()}";
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
