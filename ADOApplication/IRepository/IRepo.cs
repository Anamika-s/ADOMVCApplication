using ADOApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADOApplication.IRepository
{
    public interface IRepo
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        void Insert(Employee employee);

        void Edit(int id, Employee employee);
        void Delete(int id);

    }
}
