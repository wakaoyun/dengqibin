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
    public class UsersDAL
    {
        public List<UserModel> UserLogin(string userName, string password)
        {
            SqlParameter[] param = { new SqlParameter("@UID", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.VarChar, 20) };
            param[0].Value = userName;
            param[1].Value = password;           
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_Login", CommandType.StoredProcedure,param);            
            dr.Read();
            List<UserModel> list=new List<UserModel>();
            if (dr.HasRows)
            {
                UserModel user = new UserModel();
                user.ID = Convert.ToInt32(dr[0]);
                user.SubID = Convert.ToInt32(dr[1]);
                user.UID = dr[2].ToString();
                user.Password = dr[3].ToString();
                user.Name = dr[4].ToString();
                user.UserType = Convert.ToInt32(dr[5]);
                user.Code = dr[6].ToString();
                list.Add(user);
            }
            dr.Close();
            return list;
        }
        public bool CheckUserByUID(string uid)
        {
            bool flag = false;
            SqlParameter param = new SqlParameter("@UID", SqlDbType.VarChar, 20);
            param.Value = uid;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_CheckUserByUID", CommandType.StoredProcedure, param);            
            dr.Read();
            if (dr.HasRows)
            {
                flag = true;
            }
            dr.Close();
            return flag;
        }
        public bool CheckUser(string uid,string password)
        {
            bool flag = false;
            SqlParameter[] param = { new SqlParameter("@UID", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.VarChar, 20) };
            param[0].Value = uid;
            param[1].Value = password;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_CheckUser", CommandType.StoredProcedure, param);            
            dr.Read();
            if (dr.HasRows)
            {
                flag = true;
            }
            dr.Close();
            return flag;
        }
        public int UpdateUID(int id, string uid)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@UID", SqlDbType.VarChar, 20) };
            param[0].Value = id;
            param[1].Value = uid;           
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateUID", CommandType.StoredProcedure, param);            
            return result;
        }
        public int UpdatePassword(int id, string password)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@password", SqlDbType.VarChar, 20) };
            param[0].Value = id;
            param[1].Value = password;           
            int result = SqlHelp.ExecuteNonQuery("prc_UpdatePassword", CommandType.StoredProcedure, param);            
            return result;
        }
        //public int InsertUser(UserModel user)
        //{
        //    SqlParameter[] param = { new SqlParameter("@UID", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.VarChar,20),
        //                             new SqlParameter("@UserType",SqlDbType.SmallInt),new SqlParameter("@SubID",SqlDbType.Int)
        //                           };
        //    param[0].Value = user.UID;
        //    param[1].Value = user.Password;
        //    param[2].Value = user.UserType;
        //    param[3].Value = user.SubID;
        //    int result = SqlHelp.ExecuteNonQuery("prc_InsertUser", CommandType.StoredProcedure, param);
        //    return result;
        //}
    }
}
