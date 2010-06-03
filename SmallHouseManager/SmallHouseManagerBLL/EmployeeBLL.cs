using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class EmployeeBLL
    {
        EmployeeDAL dal = new EmployeeDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public List<EmployeeModel> ShowEmployee()
        {
            return dal.ShowEmployee();
        }
        public List<EmployeeModel> ShowAllEmployee()
        {
            return dal.ShowAllEmployee();
        }
        public EmployeeModel GetEmployeeByID(int id)
        {
            return dal.GetEmployeeByID(id);
        }
        public bool InsertEmployee(EmployeeModel employee, UserModel user)
        {
            int result;
            try
            {
                result = dal.InsertEmployee(employee, user);
            }
            catch
            {
                return false;
            }
            return result == 0 ? false : true;
        }
        public bool UpdateEmployee(EmployeeModel employee)
        {
            int result = dal.UpdateEmployee(employee);
            return result == 0 ? false : true;
        }
        public bool DeleteEmployee(int id)
        {
            int result = dal.DeleteEmployee(id);
            return result == 0 ? false : true;
        }
    }
}
