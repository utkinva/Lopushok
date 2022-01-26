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
using Lopushok.Entities;
using Lopushok.Utilities;

namespace Lopushok.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Product currentProduct;
        public AddEditPage(Product product)
        {
            InitializeComponent();
            currentProduct = product ?? new Product();
            productTypeCmbx.ItemsSource = Transition.Context.ProductType.ToList();
            DataContext = currentProduct;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.mainFrame.GoBack();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (titleTxtb.Text.Length == 0)
                errors.AppendLine("Укажите наименование");
            if (articleNumberTxtb.Text.Length == 0)
                errors.AppendLine("Укажите артикул");
            if (minCostForAgentTxtb.Text.Length == 0)
                errors.AppendLine("Укажите минимальную стоимость для агента");
            if (imageTxtb.Text.Length == 0)
                imageTxtb.Text = "";
            if (descriptionTxtb.Text.Length == 0)
                descriptionTxtb.Text = "";
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (currentProduct.ID == 0)
            {
                Transition.Context.Product.Add(currentProduct);
            }

            try
            {
                Transition.Context.SaveChanges();
                MessageBox.Show("Инфорация сохранена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Transition.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
