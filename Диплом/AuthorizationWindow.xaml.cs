using System;
using System.Linq;
using System.Windows;


namespace Диплом
{
    public partial class AuthorizationWindow : Window
    {
        private SecretariesDB1Entities dbContext;

        public AuthorizationWindow()
        {
            InitializeComponent();

            // Инициализация объекта контекста базы данных
            dbContext = new SecretariesDB1Entities();

        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBox_Login.Text;
            string password = passwordBox_Password.Password;

            // Поиск пользователя по логину и паролю в базе данных
            Сотрудники user = dbContext.Сотрудники.FirstOrDefault(u => u.Логин == login && u.Пароль == password);

            if (user != null)
            {
                MessageBox.Show($"Авторизация успешна для пользователя: {user.ПолноеИмя}");
                // Здесь можно открыть новое окно или выполнить другие действия после успешной авторизации


                MainWindow mainWindow = new MainWindow(user);


                mainWindow.Initialize(user.ПолноеИмя); // Передаем полное имя в MainWindow
                // Открываем MainWindow
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.");
            }
        }
    }
}