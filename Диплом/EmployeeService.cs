using System.Linq;
using Диплом;

public class EmployeeService
{
    private SecretariesDB1Entities dbContext;

    public EmployeeService(SecretariesDB1Entities dbContext)
    {
        this.dbContext = dbContext;
    }

    public Сотрудники GetEmployeeByLogin(string login)
    {
        // Найдем сотрудника по заданному логину
        Сотрудники employee = dbContext.Сотрудники.FirstOrDefault(emp => emp.Логин == login);

        return employee; // Вернем найденного сотрудника (или null, если сотрудник не найден)
    }
}
