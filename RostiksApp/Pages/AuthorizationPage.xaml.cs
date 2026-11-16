using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RostiksApp.Pages
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        // Кнопка "Войти" - проверка логина и пароля
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            // Проверяем, что поля не пустые
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ищем сотрудника в БД по логину и паролю
            var employee = AppConnect.DBContext.Employees
                .FirstOrDefault(emp => emp.login == login && emp.password == password);

            if (employee != null)
            {
                // Сохраняем текущего пользователя
                AppConnect.CurrentUser = employee;

                // Обновляем текст роли в шапке
                string roleName = employee.Roles?.name ?? "Сотрудник";
                ((MainWindow)Application.Current.MainWindow).UpdateUserRole(roleName);

                // Переходим на страницу меню
                ((MainWindow)Application.Current.MainWindow).MainFrame
                    .Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Кнопка "Войти как гость"
        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            AppConnect.CurrentUser = null;
            ((MainWindow)Application.Current.MainWindow).UpdateUserRole("Гость");
            ((MainWindow)Application.Current.MainWindow).MainFrame
                .Navigate(new MenuPage());
        }
    }
}