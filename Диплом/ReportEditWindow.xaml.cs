using System;
using System.Windows;

namespace Диплом
{
    public partial class ReportEditWindow : Window
    {
        private Отчеты report;
        private SecretariesDB1Entities dbContext;
        private EmployeeService employeeService;

        public ReportEditWindow(Отчеты report, SecretariesDB1Entities dbContext)
        {
            InitializeComponent();
            this.report = report;
            this.dbContext = dbContext;
            this.employeeService = new EmployeeService(dbContext);

            if (report == null)
            {
                // Создание нового отчета, если report равен null
                this.report = new Отчеты();
            }

            // Заполнение элементов управления данными из выбранного отчета или нового объекта
            txtTitle.Text = this.report.НазваниеОтчета;
            txt.Text = this.report.СодержаниеОтчета;
            datePickerDate.SelectedDate = this.report.ДатаОтчета;
        }

        public ReportEditWindow(СобытияКалендаря newReport, SecretariesDB1Entities dbContext)
        {
            this.dbContext = dbContext;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновление свойств отчета на основе данных из элементов управления
                this.report.НазваниеОтчета = txtTitle.Text;
                this.report.СодержаниеОтчета = txt.Text;
                this.report.ДатаОтчета = datePickerDate.SelectedDate;

                if (report.КодОтчета == 0)
                {
                    // Находим код сотрудника и устанавливаем его в отчет
                    int currentEmployeeId = GetCurrentEmployeeId(); // Пример: получение ID текущего сотрудника
                    this.report.КодСотрудника = currentEmployeeId;

                    // Добавление нового отчета в базу данных
                    dbContext.Отчеты.Add(this.report);
                }

                // Сохранение изменений в базе данных
                dbContext.SaveChanges();
                MessageBox.Show("Изменения сохранены успешно.");
                this.Close(); // Закрытие окна после сохраненияz
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        private int GetCurrentEmployeeId()
        {
            
            return 1;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие текущего окна
            this.Close();
        }
    }
}
