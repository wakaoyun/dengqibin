using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class BaseParkDAL
    {
        public int CheckHomePark(int ParkID)
        {
            SqlParameter param = new SqlParameter("@ParkID", SqlDbType.Int);
            param.Value = ParkID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_CheckHomePark", CommandType.StoredProcedure, param);
            dr.Read();
            int result = Convert.ToInt32(dr[0]);
            return result;
        }
        public List<BaseParkModel> GetBasePark()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetBasePark", CommandType.StoredProcedure);
            List<BaseParkModel> list = new List<BaseParkModel>();
            while (dr.Read())
            {
                BaseParkModel basePark = new BaseParkModel();
                basePark.ParkID = Convert.ToInt32(dr[0]);
                basePark.Name = dr[1].ToString();
                basePark.Amount = Convert.ToInt32(dr[2]);
                basePark.Memo = dr[3].ToString();
                list.Add(basePark);
            }
            dr.Close();
            return list;
        }
        public BaseParkModel GetBaseParkByID(int parkID)
        {
            SqlParameter param = new SqlParameter("@ParkID", SqlDbType.Int);
            param.Value = parkID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetBaseParkByID", CommandType.StoredProcedure, param);
            dr.Read();
            BaseParkModel basePark = new BaseParkModel();
            if (dr.HasRows)
            {
                basePark.ParkID = Convert.ToInt32(dr[0]);
                basePark.Name = dr[1].ToString();
                basePark.Amount = Convert.ToInt32(dr[2]);
                basePark.Memo = dr[3].ToString();
            }
            dr.Close();
            return basePark;
        }
        public int UpdateBasePark(BaseParkModel basePark)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@Amount", SqlDbType.Int),
                                     new SqlParameter("@Memo",SqlDbType.VarChar,50),new SqlParameter("@ParkID",SqlDbType.Int)
                                   };
            param[0].Value = basePark.Name;
            param[1].Value = basePark.Amount;
            param[2].Value = basePark.Memo;
            param[3].Value = basePark.ParkID;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateBasePark", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertBasePark(BaseParkModel basePark)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@Amount", SqlDbType.Int),
                                     new SqlParameter("@Memo",SqlDbType.VarChar,50)
                                   };
            param[0].Value = basePark.Name;
            param[1].Value = basePark.Amount;
            param[2].Value = basePark.Memo;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertBasePark", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteBasePark(int parkID)
        {
            SqlParameter param = new SqlParameter("@ParkID", SqlDbType.Int);
            param.Value = parkID;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteBasePark", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
