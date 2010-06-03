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
    public class FixRepairDAL
    {
        public List<FixRepairModel> GetAllFixRepair()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllFixRepair", CommandType.StoredProcedure);
            List<FixRepairModel> list = new List<FixRepairModel>();
            while (dr.Read())
            {
                FixRepairModel fixRepair = new FixRepairModel();
                fixRepair.ID = Convert.ToInt32(dr[0]);
                fixRepair.FixID = dr[1].ToString();
                fixRepair.FixName = dr[2].ToString();
                fixRepair.RepairDate = Convert.ToDateTime(dr[3]);
                fixRepair.EndDate = Convert.ToDateTime(dr[4]);
                fixRepair.MainHead = dr[5].ToString();
                fixRepair.ServiceFee = Convert.ToDouble(dr[6]);
                fixRepair.MaterielFee = Convert.ToDouble(dr[7]);
                fixRepair.RepairSum = Convert.ToDouble(dr[8]);
                fixRepair.RepairMemo = dr[9].ToString();
                fixRepair.Sign = Convert.ToInt32(dr[10]);
                fixRepair.RepairUnit = dr[11].ToString();
                list.Add(fixRepair);
            }
            dr.Close();
            return list;
        }
        public FixRepairModel GetFixRepairByID(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetFixRepairByID", CommandType.StoredProcedure);
            dr.Read();
            FixRepairModel fixRepair = new FixRepairModel();
            if (dr.HasRows)
            {
                fixRepair.ID = Convert.ToInt32(dr[0]);
                fixRepair.FixID = dr[1].ToString();
                fixRepair.FixName = dr[2].ToString();
                fixRepair.RepairDate = Convert.ToDateTime(dr[3]);
                fixRepair.EndDate = Convert.ToDateTime(dr[4]);
                fixRepair.MainHead = dr[5].ToString();
                fixRepair.ServiceFee = Convert.ToDouble(dr[6]);
                fixRepair.MaterielFee = Convert.ToDouble(dr[7]);
                fixRepair.RepairSum = Convert.ToDouble(dr[8]);
                fixRepair.RepairMemo = dr[9].ToString();
                fixRepair.Sign = Convert.ToInt32(dr[10]);
                fixRepair.RepairUnit = dr[11].ToString();
            }
            dr.Close();
            return fixRepair;
        }
        public List<FixRepairModel> GetFixRepairByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar,255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetFixRepairByCondition", CommandType.StoredProcedure,param);
            List<FixRepairModel> list = new List<FixRepairModel>();
            while (dr.Read())
            {
                FixRepairModel fixRepair = new FixRepairModel();
                fixRepair.ID = Convert.ToInt32(dr[0]);
                fixRepair.FixID = dr[1].ToString();
                fixRepair.FixName = dr[2].ToString();
                fixRepair.RepairDate = Convert.ToDateTime(dr[3]);
                fixRepair.EndDate = Convert.ToDateTime(dr[4]);
                fixRepair.MainHead = dr[5].ToString();
                fixRepair.ServiceFee = Convert.ToDouble(dr[6]);
                fixRepair.MaterielFee = Convert.ToDouble(dr[7]);
                fixRepair.RepairSum = Convert.ToDouble(dr[8]);
                fixRepair.RepairMemo = dr[9].ToString();
                fixRepair.Sign = Convert.ToInt32(dr[10]);
                fixRepair.RepairUnit = dr[11].ToString();
                list.Add(fixRepair);
            }
            dr.Close();
            return list;
        }
        public int UpdateFixRepair(FixRepairModel fixRepair)
        {
            SqlParameter[] param = { new SqlParameter("@FixID", SqlDbType.VarChar, 18), new SqlParameter("@RepairDate", SqlDbType.DateTime, 10),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime,10),new SqlParameter("@MainHead",SqlDbType.VarChar,20),
                                     new SqlParameter("@ServiceFee",SqlDbType.Money),new SqlParameter("@MaterielFee",SqlDbType.Money),
                                     new SqlParameter("@RepairSum",SqlDbType.Money),new SqlParameter("@RepairMemo",SqlDbType.Text),
                                     new SqlParameter("@Sign",SqlDbType.Int),new SqlParameter("@RepairUnit",SqlDbType.VarChar,50)
                                   };
            param[0].Value = fixRepair.FixID;
            param[1].Value = fixRepair.RepairDate;
            param[2].Value = fixRepair.EndDate;
            param[3].Value = fixRepair.MainHead;
            param[4].Value = fixRepair.ServiceFee;
            param[5].Value = fixRepair.MaterielFee;
            param[6].Value = fixRepair.RepairSum;
            param[7].Value = fixRepair.RepairMemo;
            param[8].Value = fixRepair.Sign;
            param[9].Value = fixRepair.RepairUnit;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateFixRepair", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateFixRepairForSign(FixRepairModel fixRepair)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18),new SqlParameter("@Sign",SqlDbType.Int)};
            param[0].Value = fixRepair.ID;           
            param[1].Value = fixRepair.Sign;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateFixRepairForSign", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertFixRepair(FixRepairModel fixRepair)
        {
            SqlParameter[] param = { new SqlParameter("@FixID", SqlDbType.VarChar, 18), new SqlParameter("@RepairDate", SqlDbType.DateTime, 10),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime,10),new SqlParameter("@MainHead",SqlDbType.VarChar,20),
                                     new SqlParameter("@ServiceFee",SqlDbType.Money),new SqlParameter("@MaterielFee",SqlDbType.Money),
                                     new SqlParameter("@RepairSum",SqlDbType.Money),new SqlParameter("@RepairMemo",SqlDbType.Text),
                                     new SqlParameter("@Sign",SqlDbType.Int),new SqlParameter("@RepairUnit",SqlDbType.VarChar,50)
                                   };
            param[0].Value = fixRepair.FixID;
            param[1].Value = fixRepair.RepairDate;
            param[2].Value = fixRepair.EndDate;
            param[3].Value = fixRepair.MainHead;
            param[4].Value = fixRepair.ServiceFee;
            param[5].Value = fixRepair.MaterielFee;
            param[6].Value = fixRepair.RepairSum;
            param[7].Value = fixRepair.RepairMemo;
            param[8].Value = fixRepair.Sign;
            param[9].Value = fixRepair.RepairUnit;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertFixRepair", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteFixRepair(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteFixRepair", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
