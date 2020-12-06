using Employees.Infrastructure;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Repository
{
    public class EmployeeRepository
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee() { Id = Guid.NewGuid(), Name = "Jose", Surname = "Alvarez", Position = "Ayudant", Salary = 1200},
            new Employee() { Id = Guid.NewGuid(), Name = "Javier", Surname = "Garcia", Position = "Secretari", Salary = 920},
            new Employee() { Id = Guid.NewGuid(), Name = "Marc", Surname = "Sanchez", Position = "Director", Salary = 1420}
        };

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }
        
        public Employee GetEmployeeById(Guid id)
        {
            Employee emp = employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return null;
            }
            else
                return emp;
        }

        public ValidationResult<Employee> Create(Employee entity)
        {
            ValidationResult<Employee> output = new ValidationResult<Employee>()
            {
                IsSuccess = true,
            };

            if (entity.Id == default(Guid))
                entity.Id = Guid.NewGuid();

            if(employees.Find(x => x.Id == entity.Id) != null)
            {
                output.IsSuccess = false;
                output.Messages.Add("The ID already exist!.");
            }

            if (output.IsSuccess)
            {
                employees.Add(entity);
                output.ValidatedResult = entity;
            }
            
            return output;
        }

        public ValidationResult<Employee> Update(Guid id, Employee entity)
        {
            ValidationResult<Employee> output = new ValidationResult<Employee>()
            {
                IsSuccess = true,
            };

            if (employees.Find(x => x.Id == id) == null)
            {
                output.IsSuccess = false;
                output.Messages.Add("ID not exist");
            }

            if (output.IsSuccess)
            {
                var oldEmployee = employees.FirstOrDefault(x => x.Id == id);
                oldEmployee.Name = entity.Name;
                oldEmployee.Surname = entity.Surname;
                oldEmployee.Position = entity.Position;
                oldEmployee.Salary = entity.Salary;
                output.ValidatedResult = oldEmployee;
            }

            return output;
        }

        public ValidationResult<Employee> Delete(Guid id)
        {
            ValidationResult<Employee> output = new ValidationResult<Employee>()
            {
                IsSuccess = true,
            };

            if (employees.Find(x => x.Id == id) == null)
            {
                output.IsSuccess = false;
                output.Messages.Add("ID not exist");
            }

            if (output.IsSuccess)
            {
                var employee = employees.FirstOrDefault(x => x.Id == id);
                employees.Remove(employee);
            }

            return output;
        }
    }
}
