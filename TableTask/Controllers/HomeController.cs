using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableTask.Bll.Student;
using TableTask.Model.Employee;
using TableTask.Models.ViewModels;

namespace TableTask.Controllers
{
    public class HomeController : Controller
    {
        readonly EmployeeServices _employeeService = new EmployeeServices();

       
        public virtual ActionResult Index(int? page, string employeeName, string salary)
        {
            if (Request.IsAjaxRequest())
            {
                var model = GetEmployeeDetails(employeeName, salary, page);
                return PartialView("_EmployeList", model);
            }
            else
            {
                var employeeModel = GetEmployeeDetails(employeeName, salary, page);
                return View(employeeModel);
            }
        }
        private PagedViewModel<EmployeeModel> GetEmployeeDetails(string employeeName, string salary, int? page = null)
        {
            IEnumerable<EmployeeModel> data;
            if (string.IsNullOrEmpty(employeeName) && string.IsNullOrEmpty(salary))
                data = _employeeService.employee_List().OrderBy(o => o.Salary);
            else
            {
                if (!string.IsNullOrEmpty(employeeName))
                    data = _employeeService.employee_List().OrderBy(o => o.Salary)
                        .Where(o => o.FirstName.Trim().ToLower().Contains(employeeName.Trim().ToLower()) ||
                                    o.Salary.Trim().ToLower() == salary.Trim().ToLower());
                else
                {
                    data = _employeeService.employee_List().OrderBy(o => o.Salary)
                        .Where(o => o.Salary.Trim().ToLower() == salary.Trim().ToLower());
                }
            }
            var model = new PagedViewModel<EmployeeModel>
            {
                Page = page,
                Query = data

            }.Setup();
            return model;
        }

       
      
        public  ActionResult EditEmployee(int id) 
        {
               var model = _employeeService.EditEmployee(id);
                return View(model);
          
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeModel employeeModel)
        {
            var model = _employeeService.UpdateEmployee(employeeModel);
            return View(model);

        }

    }
}