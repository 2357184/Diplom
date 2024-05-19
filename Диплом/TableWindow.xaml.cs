using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Диплом
{
    public partial class TableWindow : Window
    {
        private SecretariesDB1Entities dbContext;
        private Сотрудники _currentUser; // Объявляем переменную _currentUser как член класса

        // Добавляем конструктор, который принимает параметр _currentUser
        public TableWindow(Сотрудники currentUser)
        {
            InitializeComponent();
            dbContext = new SecretariesDB1Entities();
            _currentUser = currentUser; // Сохраняем переданный объект _currentUser
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dbContext.СобытияКалендаря.Load(); // Загрузка данных из таблицы СобытияКалендаря
                dataGridReports.ItemsSource = dbContext.СобытияКалендаря.Local.ToBindingList(); // Привязка данных к элементу управления DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Открытие окна для добавления нового отчета
                СобытияКалендаря newReport = new СобытияКалендаря();
                ReportEditWindow addWindow = new ReportEditWindow(newReport, dbContext);
                if (addWindow.ShowDialog() == true)
                {
                    dbContext.СобытияКалендаря.Add(newReport);
                    dbContext.SaveChanges(); // Сохранение изменений в базе данных
                    LoadData(); // Перезагрузка данных после добавления
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Редактирование выбранного отчета
                СобытияКалендаря selectedReport = dataGridReports.SelectedItem as СобытияКалендаря;
                if (selectedReport != null)
                {
                    ReportEditWindow editWindow = new ReportEditWindow(selectedReport, dbContext);
                    if (editWindow.ShowDialog() == true)
                    {
                        dbContext.SaveChanges(); // Сохранение изменений в базе данных
                        LoadData(); // Перезагрузка данных после редактирования
                    }
                }
                else
                {
                    MessageBox.Show("Выберите пункт для редактирования.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании : {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Удаление выбранного отчета
                СобытияКалендаря selectedReport = dataGridReports.SelectedItem as СобытияКалендаря;
                if (selectedReport != null)
                {
                    dbContext.СобытияКалендаря.Remove(selectedReport);
                    dbContext.SaveChanges(); // Сохранение изменений в базе данных
                    LoadData(); // Перезагрузка данных после удаления
                    MessageBox.Show("успешно удален.");
                }
                else
                {
                    MessageBox.Show("Выберите пукт для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новое окно MainWindow и передаем _currentUser в конструктор
            MainWindow MWindow = new MainWindow(_currentUser);
            MWindow.Show();

            // Закрытие текущего окна выбора пользователя
            this.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновление данных в DataGrid
            LoadData();
        }
    }
}
