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
using Lopushok.Utilities;
using Lopushok.Entities;

namespace Lopushok.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : Page
    {
        public List<Product> ItemsProductMaterial { get { return Transition.Context.Product.ToList(); } }
        public ProductsView()
        {
            InitializeComponent();
            var allTypes = Transition.Context.ProductType.ToList();
            allTypes.Insert(0, new ProductType { Title = "Все типы" });
            filterByCombox.ItemsSource = allTypes;
            filterByCombox.SelectedIndex = 0;


            sortByCombox.Items.Insert(0, "По умолчанию");
            sortByCombox.Items.Insert(1, "По цене");
            sortByCombox.Items.Insert(2, "По наименованию");
            sortByCombox.SelectedIndex = 0;


            listViewProducts.ItemsSource = ItemsProductMaterial;
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.mainFrame.Navigate(new AddEditPage(null));
            UpdateData();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.mainFrame.Navigate(new AddEditPage(listViewProducts.SelectedItem as Product));
            UpdateData();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var itemToDelete = listViewProducts.SelectedItem as Product;
            if (itemToDelete != null)
            {
                if (MessageBox.Show($"Удалить продукт №{itemToDelete.ID}", "Удаление продукта", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Transition.Context.Product.Remove(itemToDelete);
                    Transition.Context.SaveChanges();
                    MessageBox.Show("Данные удалены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            UpdateData();

        }
        #region Сортировка
        public void UpdateData()
        {
            var updatedData = Transition.Context.Product.ToList();
            if (searchTxt.Text != "Поиск..." && searchTxt.Text != "")
            {
                updatedData = updatedData.Where(x => x.Title.ToLower().Contains(searchTxt.Text.ToLower()) ||
                                                 x.Description.ToLower().Contains(searchTxt.Text.ToLower())).ToList();
            }
            if (filterByCombox.SelectedIndex > 0)
            {
                updatedData = updatedData.Where(p => p.ProductType.Title == (filterByCombox.SelectedItem as ProductType).Title.ToString()).ToList();
            }
            if (sortByCombox.SelectedIndex > 0)
            {
                switch (sortByCombox.SelectedIndex)
                {
                    case 1:
                        {
                            if (ascDescCheck.IsChecked == false)
                            {
                                updatedData = updatedData.OrderBy(x => x.Cost).ToList();
                            }
                            else
                            {
                                updatedData = updatedData.OrderByDescending(x => x.Cost).ToList();
                            }

                            break;
                        }
                    case 2:
                        {
                            if (ascDescCheck.IsChecked == false)
                            {
                                updatedData = updatedData.OrderBy(x => x.Title).ToList();
                            }
                            else
                            {
                                updatedData = updatedData.OrderByDescending(x => x.Title).ToList();
                            }
                            break;
                        }
                }


            }
            listViewProducts.ItemsSource = updatedData;
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTxt.Text == "Поиск...")
                searchTxt.Text = "";
        }

        private void searchTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchTxt.Text == "")
                searchTxt.Text = "Поиск...";
        }

        private void searchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTxt.Text != "Поиск...")
            {
                UpdateData();
            }
        }

        private void sortByCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void filterByCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
        #endregion
        private void listViewProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editBtn.Visibility = Visibility.Visible;
            deleteBtn.Visibility = Visibility.Visible;
        }
    }
}
