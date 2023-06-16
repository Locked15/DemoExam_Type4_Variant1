using DemoExam_Type4_Variant1.Application.Common;
using DemoExam_Type4_Variant1.Application.Models.Entities;
using DemoExam_Type4_Variant1.Application.Views.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DemoExam_Type4_Variant1.Application.Views.Windows
{
    /// <summary>
    /// Interaction logic for OrderFormationWindow.xaml.
    /// </summary>
    public partial class OrderFormationWindow : Window
    {
        private Product? _selectedProduct;

        public OrderFormationWindow()
        {
            InitializeComponent();
            DataContext = SessionData.CurrentOrder;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            pickupPointSelector.ItemsSource = DemoExamDataContext.Instance.PickupPoints.ToList();
            UpdateData();
        }

        public void UpdateData()
        {
            if (SessionData.CurrentOrder?.OrderProducts.Any() == false)
                Close();

            UpdateFields();
            UpdateProductList();
        }

        private void UpdateFields()
        {
            fullOrderDiscountText.Text = SessionData.CurrentOrder!.FullOrderDiscount.ToString("0.00");
            fullOrderCostText.Text = SessionData.CurrentOrder!.FullOrderCost.ToString("0.00");
        }

        private void UpdateProductList()
        {
            orderProductsList.Items.Clear();
            foreach (var item in SessionData.CurrentOrder!.OrderProducts)
            {
                var newItem = new ProductItem(item.Product)
                {
                    Width = GetOptimalWidth()
                };
                newItem.addToOrderAction.Click += (args, e) => UpdateFields();

                orderProductsList.Items.Add(newItem);
            }

            OnOrderProductsListSelectionChanged(default, default);
        }
     
        private static int? GetOrderProductCountByProductId(int productId)
        {
            return SessionData.CurrentOrder?.OrderProducts.FirstOrDefault(op => op.ProductId == productId)?.Count;
        }

        private void OnOrderProductsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderProductsList.SelectedItem is ProductItem item)
            {
                _selectedProduct = item.Model;
                deleteProductButton.Visibility = Visibility.Visible;
                productAmountInput.Text = GetOrderProductCountByProductId(_selectedProduct.Id).ToString();
            }
            else
            {
                deleteProductButton.Visibility = Visibility.Hidden;
                productAmountInput.Text = string.Empty;
            }
        }

        private void OnProductAmountInputTextChanged(object sender, TextChangedEventArgs e)
        {
            if (orderProductsList.SelectedItem is ProductItem item)
            {
                var alteringOrderProduct = SessionData.CurrentOrder!.OrderProducts.First(op => op.ProductId == item.Model.Id);
                if (int.TryParse(productAmountInput.Text, out int newCount))
                {
                    if (newCount <= 0)
                    {
                        var deleteConfirmation = MessageBox.Show("Такое количество приведёт к удалению товара из заказа. Вы уверены?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (deleteConfirmation == MessageBoxResult.Yes)
                        {
                            SessionData.CurrentOrder?.OrderProducts.Remove(alteringOrderProduct);
                            UpdateData();
                        }
                        else
                        {
                            productAmountInput.Text = GetOrderProductCountByProductId(item.Model.Id).ToString();
                            UpdateFields();
                        }
                    }
                    else
                    {
                        alteringOrderProduct.Count = newCount;
                        UpdateFields();
                    }
                }
                else
                {
                    MessageBox.Show("Введено некорректное количество. Сброс...", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    productAmountInput.Text = GetOrderProductCountByProductId(item.Model.Id).ToString();
                }
            }
        }

        private void OnCreateBulletinButtonClick(object sender, RoutedEventArgs e) =>
                MessageBox.Show("К сожалению, данная возможность ещё в разработке.", "Work in Progress...", MessageBoxButton.OK, MessageBoxImage.Information);

        private void OnDeleteProductButtonClick(object sender, RoutedEventArgs e)
        {
            if (orderProductsList.SelectedItem is ProductItem item)
            {
                var alteringOrderProduct = SessionData.CurrentOrder!.OrderProducts.First(op => op.ProductId == item.Model.Id);
                var confirmation = MessageBox.Show("Вы точно хотите удалить товар из заказа?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmation == MessageBoxResult.Yes)
                {
                    SessionData.CurrentOrder?.OrderProducts.Remove(alteringOrderProduct);
                    UpdateData();
                }
            }
        }

        private void OnSaveOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if (SessionData.CurrentOrder!.PickupPoint != null)
            {
                try
                {
                    // This will be executed at first time (order not inserted to DB yet).
                    if (SessionData.CurrentOrder.Id == default)
                    {
                        DemoExamDataContext.Instance.Orders.Add(SessionData.CurrentOrder);
                        DemoExamDataContext.Instance.OrderProducts.AddRange(SessionData.CurrentOrder.OrderProducts);

                        DemoExamDataContext.Instance.SaveChanges();
                        MessageBox.Show("Заказ создан.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    // Further this block will be executed (only update data).
                    else
                    {
                        DemoExamDataContext.Instance.SaveChanges();
                        MessageBox.Show("Заказ обновлён.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка сохранения данных.\n\nДетали:\n{ex.Message}.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Необходимо указать пункт получения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ProductItem item in orderProductsList.Items)
                item.Width = GetOptimalWidth();
        }

        private double GetOptimalWidth()
        {
            if (WindowState == WindowState.Maximized)
                return RenderSize.Width - 65;
            else
                return Width - 65;
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = SessionData.CurrentOrder!.Id != default;
        }
    }
}
