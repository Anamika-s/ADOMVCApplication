using ADOApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ADOApplication.Repository
{
    public class EmployeeRepo : IRepository.IRepo
    {
        IConfiguration _config;
        string connectionString;
        public EmployeeRepo(IConfiguration config)
        {
            _config = config;
             connectionString = _config.GetConnectionString("MyConnection");
        }
          SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
           // return new SqlConnection(@"data source=adminvm\SQLEXPRESS;initial catalog=hmDB;user id=sa;password=pass@123");

        }
        public void Delete(int id)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Delete employee where id=@id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Edit(int id, Employee employee)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "update employee set address=@address, salary =@salary where id=@id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", id);

            command.Parameters.AddWithValue("@address", employee.Address);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = null;
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from employee";
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                employees = new List<Employee>();
                while (reader.Read())
                {
                   
                    Employee employee = new Employee()
                    { 
                     
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Address = reader[2].ToString(),
                        Salary = (int)reader[3]
                    };
                    employees.Add(employee);
                }

                reader.Close();
                connection.Close();
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from Employee where id=@id";
            command.Parameters.AddWithValue("@id", id);
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
               employee = new Employee()
                {
                    Id =  (int)reader[0],
                    Name = reader[1].ToString(),
                    Address = reader[2].ToString(),
                    Salary = (int)reader[3]
                };
            
                 reader.Close();
                connection.Close();
                
            }

            return employee;


        }

        public void Insert(Employee employee)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Insert into employee(id, name, address, salary) values(@id, @name,@address,@salary)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", employee.Id);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@address", employee.Address);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
