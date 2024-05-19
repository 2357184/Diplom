using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Диплом
{
    public partial class ReportsWindow : Window
    {
        private SecretariesDB1Entities dbContext;
        private Сотрудники _currentUser;

        public ReportsWindow(Сотрудники currentUser)
        {
            InitializeComponent();
            dbContext = new SecretariesDB1Entities();
            _currentUser = currentUser;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dbContext.Отчеты.Load();
                var reports = dbContext.Отчеты.Include("Сотрудники"); // Включаем связанную сущность Сотрудники
                dataGridReports.ItemsSource = reports.Select(r => new
                {
                    ReportName = r.НазваниеОтчета,
                    ReportDate = r.ДатаОтчета,
                    EmployeeName = r.Сотрудники.ПолноеИмя // Получаем полное имя сотрудника из связанной сущности
                }).ToList();
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
                // Создание нового отчета
                Отчеты newReport = new Отчеты
                {
                    // Присваивание кода текущего сотрудника новому отчету
                    КодСотрудника = _currentUser.КодСотрудника
                };

                // Открытие окна для добавления отчета
                ReportEditWindow addWindow = new ReportEditWindow(newReport, dbContext);
                if (addWindow.ShowDialog() == true)
                {
                    dbContext.Отчеты.Add(newReport);
                    dbContext.SaveChanges(); // Сохранение изменений в базе данных
                    LoadData(); // Перезагрузка данных после добавления
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении отчета: {ex.Message}");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Редактирование выбранного отчета
                Отчеты selectedReport = dataGridReports.SelectedItem as Отчеты;
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
                    MessageBox.Show("Выберите отчет для редактирования.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании отчета: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Удаление выбранного отчета
                Отчеты selectedReport = dataGridReports.SelectedItem as Отчеты;
                if (selectedReport != null)
                {
                    dbContext.Отчеты.Remove(selectedReport);
                    dbContext.SaveChanges(); // Сохранение изменений в базе данных
                    LoadData(); // Перезагрузка данных после удаления
                    MessageBox.Show("Отчет успешно удален.");
                }
                else
                {
                    MessageBox.Show("Выберите отчет для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении отчета: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на предыдущее окно
            MainWindow MWindow = new MainWindow(_currentUser);
            MWindow.Show();
            this.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Обновление данных в DataGrid
            LoadData();
        }
    }
}
