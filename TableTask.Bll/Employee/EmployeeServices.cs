using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTask.Model.Employee;

namespace TableTask.Bll.Student
{
  public class EmployeeServices : BasicService
    {
        public IEnumerable<EmployeeModel> employee_List()
        {
            return Context.dbSelectStudent().Select(o => new EmployeeModel
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Gender = o.Gender,
               Salary=o.Salary
            }).ToList();
        }
        public EmployeeModel EditEmployee(int id)
        {
            return Context.dbEditStudent(id).Select(o => new EmployeeModel
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                Gender = o.Gender,
                Salary = o.Salary
            }).FirstOrDefault();
        }
        public string UpdateEmployee(EmployeeModel employeeModel)
        {
            Context.dbUpdateEmployee(employeeModel.Id, employeeModel.FirstName, employeeModel.LastName,employeeModel.Gender, employeeModel.Salary);
            return "";

        }
    }
}
