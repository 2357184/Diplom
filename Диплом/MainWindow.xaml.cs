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

namespace Диплом
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        // Объявляем переменную _currentUser как член класса
        private Сотрудники _currentUser;

        public MainWindow(Сотрудники user)
        {
            InitializeComponent();
            _currentUser = user;
        }


        // Свойство для хранения имени пользователя
        public string UserName { get; set; }

        public void Initialize(string userName)
        {
            UserName = userName;
            textBlockWelcome.Text = $"Welcome, {UserName}!";
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

           
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }


        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно MainWindow и передаем _currentUser в конструктор
            MainWindow MWindow = new MainWindow(_currentUser);
            MWindow.Show();


        }
        // В классе MainWindow
        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно TableWindow и передаем _currentUser в конструктор
            TableWindow TBWindow = new TableWindow(_currentUser);
            TBWindow.Show();
            this.Close();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Используем переменную _currentUser здесь
            ReportsWindow RPWindow = new ReportsWindow(_currentUser);
            RPWindow.Show();
            this.Close();
        }
    }
}
