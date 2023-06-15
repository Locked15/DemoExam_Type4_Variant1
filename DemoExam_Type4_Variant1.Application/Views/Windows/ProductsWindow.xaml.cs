using DemoExam_Type4_Variant1.Application.Common;
using DemoExam_Type4_Variant1.Application.Models.Entities;
using DemoExam_Type4_Variant1.Application.Views.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DemoExam_Type4_Variant1.Application.Views.Windows
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml.
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            UpdateProductsList();
            UpdateOrderNavigationVisibility();

            MessageBox.Show("Чтобы добавить товар к заказу, нажмите на него дважды.\nЛибо используйте контекстное меню.", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateProductsList()
        {
            var products = DemoExamDataContext.Instance.Products.ToList();
            foreach (var item in products)
            {
                var listItem = new ProductItem(this, item)
                {
                    Width = GetOptimalItemWidth()
                };
                productsList.Items.Add(listItem);
            }
        }

        private void OnProductsListItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (productsList.SelectedItem is ProductItem item)
            {
                SessionData.CurrentOrder?.AddNewProductToOrder(item.Model);
                UpdateOrderNavigationVisibility();
            }
        }

        public void UpdateOrderNavigationVisibility()
        {
            // Because this condition is going via "Elvis-Operator" we must make explicit compare with 'True'.
            if (SessionData.CurrentOrder?.OrderProducts.Any() == true)
                orderFormationNavigation.Visibility = Visibility.Visible;
            else
                orderFormationNavigation.Visibility = Visibility.Hidden;
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ProductItem item in productsList.Items)
                item.Width = GetOptimalItemWidth();
        }

        private double GetOptimalItemWidth()
        {
            if (WindowState == WindowState.Maximized)
                return RenderSize.Width - 60;
            else
                return Width - 60;
        }

        private void OnOrderNavigationButtonClick(object sender, RoutedEventArgs e)
        {
            // ToDo.
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e) => Close();

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SessionData.LogOutFromAccount();
            new AuthWindow().Show();
        }
    }
}
