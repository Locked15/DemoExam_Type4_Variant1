using DemoExam_Type4_Variant1.Application.Common;
using DemoExam_Type4_Variant1.Application.Models.Entities;
using System.Windows.Controls;

namespace DemoExam_Type4_Variant1.Application.Views.Controls
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml.
    /// </summary>
    public partial class ProductItem : UserControl
    {
        public Product Model { get; set; }

        public ProductItem(Product model)
        {
            Model = model;

            InitializeComponent();
            DataContext = Model;
        }

        private void OnAddToOrderActionClick(object sender, System.Windows.RoutedEventArgs e) =>
                SessionData.CurrentOrder?.AddNewProductToOrder(Model);
    }
}
