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

namespace RostiksApp.Pages
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            LoadDishes();
            ShowUserRole();
        }

        // Роль текущего пользователя
        private void ShowUserRole()
        {
            if (AppConnect.CurrentUser != null)
            {
                string roleName = AppConnect.CurrentUser.Roles?.name ?? "Сотрудник";
                UserRoleText.Text = roleName;
            }
            else
            {
                UserRoleText.Text = "Гость";
            }
        }

        // Загружаем блюда из БД и отображаем их
        private void LoadDishes()
        {
            // Получаем все блюда из базы данных
            var dishes = AppConnect.DBContext.Dishes.ToList();

            // Очищаем панель
            DishesWrapPanel.Children.Clear();

            // Создаем карточку для каждого блюда
            foreach (var dish in dishes)
            {
                // Основной контейнер карточки
                Border cardBorder = new Border
                {
                    Width = 380,
                    Height = 130,
                    Margin = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(200, 230, 201)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(165, 214, 167)),
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(8)
                };

                StackPanel cardPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(10)
                };

                // Изображение блюда 
                Image dishImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(0, 0, 10, 0)
                };

                if (!string.IsNullOrEmpty(dish.imagePath))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(dish.imagePath, UriKind.RelativeOrAbsolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        dishImage.Source = bitmap;
                    }
                    catch
                    {
                        // Если картинка не загрузилась - оставляем пустой
                    }
                }

                // Информация о блюде
                StackPanel infoPanel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Название блюда
                TextBlock nameText = new TextBlock
                {
                    Text = dish.name,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextWrapping = TextWrapping.Wrap,
                    Width = 240,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                // Категория
                string categoryName = dish.DishCategories?.name ?? "Без категории";
                TextBlock categoryText = new TextBlock
                {
                    Text = $"Категория: {categoryName}",
                    FontSize = 12,
                    Margin = new Thickness(0, 0, 0, 3)
                };

                // Состав
                TextBlock compositionText = new TextBlock
                {
                    Text = $"Состав: {dish.composition}",
                    FontSize = 11,
                    TextWrapping = TextWrapping.Wrap,
                    Width = 240
                };

                // Цена
                TextBlock priceText = new TextBlock
                {
                    Text = $"Цена: {dish.price} ₽",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50)),
                    Margin = new Thickness(0, 5, 0, 0)
                };

                infoPanel.Children.Add(nameText);
                infoPanel.Children.Add(categoryText);
                infoPanel.Children.Add(compositionText);
                infoPanel.Children.Add(priceText);

                cardPanel.Children.Add(dishImage);
                cardPanel.Children.Add(infoPanel);
                cardBorder.Child = cardPanel;

                DishesWrapPanel.Children.Add(cardBorder);
            }
        }

        // Кнопка "Выход" 
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AppConnect.CurrentUser = null;
            ((MainWindow)Application.Current.MainWindow).MainFrame
                .Navigate(new AuthorizationPage());
        }
    }
}