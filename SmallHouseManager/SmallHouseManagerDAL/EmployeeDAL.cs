using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class EmployeeDAL
    {
        public int GetMaxID()
        {
            int id = 0;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetEmployeeMaxID", CommandType.StoredProcedure);
            dr.Read();
            if (dr[0].ToString() != "")
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }
        public List<EmployeeModel> ShowEmployee()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowEmployee", CommandType.StoredProcedure);            
            List<EmployeeModel> list = new List<EmployeeModel>();
            while (dr.Read())
            {
                EmployeeModel employee = new EmployeeModel();                
                employee.EmlpoyeeName = dr[0].ToString();
                employee.Sex = Convert.ToInt32(dr[1]);
                employee.Arrage = dr[2].ToString();
                employee.Tel = dr[3].ToString();
                employee.PhotoPath = dr[4].ToString();               
                list.Add(employee);
            }
            dr.Close();
            return list;
        }
        public List<EmployeeModel> ShowAllEmployee()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowAllEmployees", CommandType.StoredProcedure);            
            List<EmployeeModel> list = new List<EmployeeModel>();
            while (dr.Read())
            {
                EmployeeModel employee = new EmployeeModel();               
                employee.EmlpoyeeName = dr[0].ToString();
                employee.Sex = Convert.ToInt32(dr[1]);
                employee.Arrage = dr[2].ToString();
                employee.Tel = dr[3].ToString();
                employee.Mobile = dr[4].ToString();
                employee.PhotoPath = dr[5].ToString();
                employee.ID = Convert.ToInt32(dr[6]);
                list.Add(employee);
            }
            dr.Close();
            return list;
        }
        public EmployeeModel GetEmployeeByID(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetEmployeeByID", CommandType.StoredProcedure,param);
            dr.Read();
            EmployeeModel employee = new EmployeeModel();
            if(dr.HasRows)
            {                
                employee.EmlpoyeeName = dr[0].ToString();
                employee.Sex = Convert.ToInt32(dr[1]);
                employee.Arrage = dr[2].ToString();
                employee.Tel = dr[3].ToString();
                employee.Mobile = dr[4].ToString();
                employee.PhotoPath = dr[5].ToString();
                employee.ID = Convert.ToInt32(dr[6]);
                employee.Address = dr[7].ToString();
                employee.CardID = dr[8].ToString();
            }
            dr.Close();
            return employee;
        }
        public int InsertEmployee(EmployeeModel employee, UserModel user)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@Sex", SqlDbType.SmallInt),
                                     new SqlParameter("@Arrage",SqlDbType.VarChar,20),new SqlParameter("@Address",SqlDbType.VarChar,30),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,12),new SqlParameter("@Mobile",SqlDbType.VarChar,12),
                                     new SqlParameter("@CardID",SqlDbType.VarChar,18),new SqlParameter("@PhotoPath",SqlDbType.VarChar,255)
                                   };
            param[0].Value = employee.EmlpoyeeName;
            param[1].Value = employee.Sex;
            param[2].Value = employee.Arrage;
            param[3].Value = employee.Address;
            param[4].Value = employee.Tel;
            param[5].Value = employee.Mobile;
            param[6].Value = employee.CardID;
            param[7].Value = employee.PhotoPath;      

            SqlParameter[] param1 = { new SqlParameter("@UID", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.VarChar,20),
                                     new SqlParameter("@UserType",SqlDbType.SmallInt),new SqlParameter("@SubID",SqlDbType.Int)
                                   };
            param1[0].Value = user.UID;
            param1[1].Value = user.Password;
            param1[2].Value = user.UserType;
            param1[3].Value = user.SubID;
           
            string[] commandText = { "prc_InsertEmployee", "prc_InsertUser" };
            SqlParameter[][] paramArray = { param, param1 };
            int result = SqlHelp.ExecuteNonQueryTransaction(commandText, CommandType.StoredProcedure, paramArray);
            return result;
        }
        public int UpdateEmployee(EmployeeModel employee)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@ID", SqlDbType.Int),
                                     new SqlParameter("@Arrage",SqlDbType.VarChar,20),new SqlParameter("@Address",SqlDbType.VarChar,30),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,12),new SqlParameter("@Mobile",SqlDbType.VarChar,12),
                                     new SqlParameter("@PhotoPath",SqlDbType.VarChar,255)
                                   };
            param[0].Value = employee.EmlpoyeeName;
            param[1].Value = employee.ID;
            param[2].Value = employee.Arrage;
            param[3].Value = employee.Address;
            param[4].Value = employee.Tel;
            param[5].Value = employee.Mobile;
            param[6].Value = employee.PhotoPath;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateEmployee", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteEmployee(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteEmployee", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
