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
    public class FixDAL
    {
        public int GetMaxID()
        {
            int id = -1;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetFixMaxID", CommandType.StoredProcedure);
            dr.Read();
            if (dr[0].ToString() != "")
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }       
        public List<FixModel> GetAllFix()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllFix", CommandType.StoredProcedure);
            List<FixModel> list = new List<FixModel>();
            while (dr.Read())
            {
                FixModel fix = new FixModel();
                fix.FixID = dr[0].ToString();
                fix.Name = dr[1].ToString();
                fix.Amount = Convert.ToInt32(dr[2]);
                fix.Factory = dr[3].ToString();
                fix.FactoryDate = Convert.ToDateTime(dr[4]);
                list.Add(fix);
            }
            dr.Close();
            return list;
        }
        public FixModel GetFixByID(string fixID)
        {
            SqlParameter param = new SqlParameter("@FixID", SqlDbType.VarChar, 18);
            param.Value = fixID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetFixByID", CommandType.StoredProcedure, param);
            List<FixModel> list = new List<FixModel>();
            dr.Read();
            FixModel fix = new FixModel();
            if (dr.HasRows)
            {
                fix.FixID = dr[0].ToString();
                fix.Name = dr[1].ToString();
                fix.Amount = Convert.ToInt32(dr[2]);
                fix.Factory = dr[3].ToString();
                fix.FactoryDate = Convert.ToDateTime(dr[4]);
            }
            dr.Close();
            return fix;
        }
        public List<FixModel> GetFixByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar, 255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetFixByCondition", CommandType.StoredProcedure,param);
            List<FixModel> list = new List<FixModel>();
            while (dr.Read())
            {
                FixModel fix = new FixModel();
                fix.FixID = dr[0].ToString();
                fix.Name = dr[1].ToString();
                fix.Amount = Convert.ToInt32(dr[2]);
                fix.Factory = dr[3].ToString();
                fix.FactoryDate = Convert.ToDateTime(dr[4]);
                list.Add(fix);
            }
            dr.Close();
            return list;
        }
        public int InsertFix(FixModel fix)
        {
            SqlParameter[] param = { new SqlParameter("@FixID", SqlDbType.VarChar, 18), new SqlParameter("@Name", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Amount",SqlDbType.Int),new SqlParameter("@Factory",SqlDbType.VarChar,20),
                                     new SqlParameter("@FactoryDate",SqlDbType.DateTime,10)
                                   };
            param[0].Value = fix.FixID;
            param[1].Value = fix.Name;
            param[2].Value = fix.Amount;
            param[3].Value = fix.Factory;
            param[4].Value = fix.FactoryDate;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertFix", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateFix(FixModel fix)
        {
            SqlParameter[] param = { new SqlParameter("@FixID", SqlDbType.VarChar, 18), new SqlParameter("@Name", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Amount",SqlDbType.Int),new SqlParameter("@Factory",SqlDbType.VarChar,20),
                                     new SqlParameter("@FactoryDate",SqlDbType.DateTime,10)
                                   };
            param[0].Value = fix.FixID;
            param[1].Value = fix.Name;
            param[2].Value = fix.Amount;
            param[3].Value = fix.Factory;
            param[4].Value = fix.FactoryDate;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateFix", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteFix(string fixID)
        {
            SqlParameter param = new SqlParameter("@FixID", SqlDbType.VarChar,18);
            param.Value = fixID;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteFix", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
