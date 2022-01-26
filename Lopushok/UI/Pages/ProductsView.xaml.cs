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
        public List<ProductMaterial> ItemsProductMaterial { get { return Transition.Context.ProductMaterial.ToList(); } }
        public int countItemsProductMaterial { get { return ItemsProductMaterial.Count; } }
        ViewPages vp = new ViewPages(0, 1);
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

            vp.itemsCount = countItemsProductMaterial;
            vp.GetIndex();

            listViewProducts.ItemsSource = ItemsProductMaterial.GetRange(vp.startIndex, vp.rangeItemsCount);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
