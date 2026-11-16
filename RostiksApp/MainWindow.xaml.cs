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

namespace RostiksApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppConnect.ConnectToDatabase();

            // Показываем страницу входа
            MainFrame.Navigate(new Pages.AuthorizationPage());
        }
        public void UpdateUserRole(string roleName)
        {
            // Находим TextBlock с ролью на текущей странице
            // (будем вызывать его после навигации)
        }
    }
}
