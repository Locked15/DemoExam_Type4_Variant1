using DemoExam_Type4_Variant1.Application.Common;
using System.Windows;

namespace DemoExam_Type4_Variant1.Application.Views.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml.
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void OnWindowKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                OnLogInButtonClick(sender, e);
        }

        private void OnLogInButtonClick(object sender, RoutedEventArgs e)
        {
            var login = userLoginInput.Text;
            var password = userPasswordInput.Password;

            if (SessionData.TryToContinueAsUser(login, password))
                OpenProductsWindow();
            else
                MessageBox.Show("Аккаунт с указанными данными не найден.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnContinueAsGuestButtonClick(object sender, RoutedEventArgs e)
        {
            SessionData.ContinueAsGuest();
            OpenProductsWindow();
        }

        private void OpenProductsWindow()
        {
            new ProductsWindow().Show();
            Close();
        }
    }
}
