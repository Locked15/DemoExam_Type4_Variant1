using DemoExam_Type4_Variant1.Application.Common;
using DemoExam_Type4_Variant1.Application.Models.Entities;
using DemoExam_Type4_Variant1.Application.Views.Windows;
using System.Windows.Controls;

namespace DemoExam_Type4_Variant1.Application.Views.Controls
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml.
    /// </summary>
    public partial class ProductItem : UserControl
    {
        private readonly ProductsWindow _parentWindow;

        public Product Model { get; set; }

        public ProductItem(ProductsWindow parentWindow, Product model)
        {
            _parentWindow = parentWindow;
            Model = model;

            InitializeComponent();
            DataContext = Model;
        }

        private void OnAddToOrderClick(object sender, System.Windows.RoutedEventArgs e)
        {
            SessionData.CurrentOrder?.AddNewProductToOrder(Model);
            _parentWindow.UpdateOrderNavigationVisibility();
        }
    }
}
