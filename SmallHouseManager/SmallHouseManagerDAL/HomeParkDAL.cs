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
    public class HomeParkDAL
    {
        public List<HomeParkModel> GetAllHomePark()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomePark", CommandType.StoredProcedure);
            List<HomeParkModel> list = new List<HomeParkModel>();
            while (dr.Read())
            {
                HomeParkModel homePark = new HomeParkModel();
                homePark.ID = Convert.ToInt32(dr[0]);
                homePark.ParkID = Convert.ToInt32(dr[1]);
                homePark.Code = dr[2].ToString();
                homePark.CarID = dr[3].ToString();
                homePark.Type = dr[4].ToString();
                homePark.BuyDate = Convert.ToDateTime(dr[5]);
                homePark.Color = dr[6].ToString();
                homePark.ParkName = dr[7].ToString();
                homePark.RoomID = dr[8].ToString();
                list.Add(homePark);
            }
            dr.Close();
            return list;
        }
        public HomeParkModel GetHomeParkByID(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeParkByID", CommandType.StoredProcedure,param);
            dr.Read();
            HomeParkModel homePark = new HomeParkModel();
            if(dr.HasRows)
            {                
                homePark.ID = Convert.ToInt32(dr[0]);
                homePark.ParkID = Convert.ToInt32(dr[1]);
                homePark.CarID = dr[2].ToString();
                homePark.Type = dr[3].ToString();
                homePark.BuyDate = Convert.ToDateTime(dr[4]);
                homePark.Color = dr[5].ToString();
                homePark.RoomID = dr[6].ToString();
            }
            dr.Close();
            return homePark;
        }
        public int UpdateHomePark(HomeParkModel homePark)
        {
            SqlParameter[] param = { new SqlParameter("@ParkID", SqlDbType.Int), new SqlParameter("@ID", SqlDbType.Int),
                                     new SqlParameter("@CarID",SqlDbType.VarChar,20),new SqlParameter("@Type",SqlDbType.VarChar,20),
                                     new SqlParameter("@BuyDate",SqlDbType.DateTime,10),new SqlParameter("@Color",SqlDbType.VarChar,20)
                                   };
            param[0].Value = homePark.ParkID;
            param[1].Value = homePark.ID;
            param[2].Value = homePark.CarID;
            param[3].Value = homePark.Type;
            param[4].Value = homePark.BuyDate;
            param[5].Value = homePark.Color;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomePark", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertHomePark(HomeParkModel homePark)
        {
            SqlParameter[] param = { new SqlParameter("@ParkID", SqlDbType.Int), new SqlParameter("@Code", SqlDbType.VarChar, 9),
                                     new SqlParameter("@CarID",SqlDbType.VarChar,20),new SqlParameter("@Type",SqlDbType.VarChar,20),
                                     new SqlParameter("@BuyDate",SqlDbType.DateTime,10),new SqlParameter("@Color",SqlDbType.VarChar,20)
                                   };
            param[0].Value = homePark.ParkID;
            param[1].Value = homePark.Code;
            param[2].Value = homePark.CarID;
            param[3].Value = homePark.Type;
            param[4].Value = homePark.BuyDate;
            param[5].Value = homePark.Color;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertHomePark", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteHomePark(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteHomePark", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
