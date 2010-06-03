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
    public class PavilionDAL
    {
        public int GetMaxID()
        {
            int id = -1;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetPavilionMaxID", CommandType.StoredProcedure);
            dr.Read();
            if (dr[0].ToString() != "")
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }
        public int CheckRoom(string paID)
        {
            SqlParameter param = new SqlParameter("@PaID", SqlDbType.VarChar, 3);
            param.Value = paID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_CheckRoomForHold", CommandType.StoredProcedure, param);
            dr.Read();
            int result = Convert.ToInt32(dr[0]);
            return result;
        }
        public List<PavilionModel> GetAllPavilion()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllPavilion", CommandType.StoredProcedure);
            List<PavilionModel> list = new List<PavilionModel>();
            while (dr.Read())
            {
                PavilionModel pavilion = new PavilionModel();
                pavilion.PaID = dr[0].ToString();
                pavilion.Name = dr[1].ToString();
                pavilion.Layer = Convert.ToInt32(dr[2]);
                pavilion.Height = Convert.ToDouble(dr[3]);
                pavilion.Area = Convert.ToDouble(dr[4]);
                pavilion.BuildDate = Convert.ToDateTime(dr[5]);
                pavilion.Memo = dr[6].ToString();
                pavilion.TypeName = dr[7].ToString();
                list.Add(pavilion);
            }
            dr.Close();
            return list;
        }
        public PavilionModel GetPavilionByID(string paID)
        {
            SqlParameter param = new SqlParameter("@PaID", SqlDbType.VarChar, 3);
            param.Value = paID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetPavilionByID", CommandType.StoredProcedure, param);
            dr.Read();
            PavilionModel pavilion = new PavilionModel();
            if (dr.HasRows)
            {
                pavilion.PaID = dr[0].ToString();
                pavilion.Name = dr[1].ToString();
                pavilion.Layer = Convert.ToInt32(dr[2]);
                pavilion.Height = Convert.ToDouble(dr[3]);
                pavilion.Area = Convert.ToDouble(dr[4]);
                pavilion.BuildDate = Convert.ToDateTime(dr[5]);
                pavilion.Memo = dr[6].ToString();
                pavilion.TypeName = dr[7].ToString();
                pavilion.TypeID = Convert.ToInt32(dr[8]);
            }
            dr.Close();
            return pavilion;
        }
        public List<PavilionModel> GetPavilionByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar, 255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetPavilionByCondition", CommandType.StoredProcedure,param);
            List<PavilionModel> list = new List<PavilionModel>();
            while (dr.Read())
            {
                PavilionModel pavilion = new PavilionModel();
                pavilion.PaID = dr[0].ToString();
                pavilion.Name = dr[1].ToString();
                pavilion.Layer = Convert.ToInt32(dr[2]);
                pavilion.Height = Convert.ToDouble(dr[3]);
                pavilion.Area = Convert.ToDouble(dr[4]);
                pavilion.BuildDate = Convert.ToDateTime(dr[5]);
                pavilion.Memo = dr[6].ToString();
                pavilion.TypeName = dr[7].ToString();
                list.Add(pavilion);
            }
            dr.Close();
            return list;
        }
        public int UpdatePavilion(PavilionModel pavilion)
        {
            SqlParameter[] param = { new SqlParameter("@PaID", SqlDbType.VarChar, 3), new SqlParameter("@Name", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Layer",SqlDbType.SmallInt),new SqlParameter("@Height",SqlDbType.Float),
                                     new SqlParameter("@Area",SqlDbType.Float),new SqlParameter("@BuildDate",SqlDbType.DateTime,10),
                                     new SqlParameter("@Memo",SqlDbType.Text),new SqlParameter("@TypeID",SqlDbType.Int)
                                   };
            param[0].Value = pavilion.PaID;
            param[1].Value = pavilion.Name;
            param[2].Value = pavilion.Layer;
            param[3].Value = pavilion.Height;
            param[4].Value = pavilion.Area;
            param[5].Value = pavilion.BuildDate;
            param[6].Value = pavilion.Memo;
            param[7].Value = pavilion.TypeID;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdatePavilion", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertPavilion(PavilionModel pavilion)
        {
            SqlParameter[] param = { new SqlParameter("@PaID", SqlDbType.VarChar, 3), new SqlParameter("@Name", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Layer",SqlDbType.SmallInt),new SqlParameter("@Height",SqlDbType.Float),
                                     new SqlParameter("@Area",SqlDbType.Float),new SqlParameter("@BuildDate",SqlDbType.DateTime,10),
                                     new SqlParameter("@Memo",SqlDbType.Text),new SqlParameter("@TypeID",SqlDbType.Int)
                                   };
            param[0].Value = pavilion.PaID;
            param[1].Value = pavilion.Name;
            param[2].Value = pavilion.Layer;
            param[3].Value = pavilion.Height;
            param[4].Value = pavilion.Area;
            param[5].Value = pavilion.BuildDate;
            param[6].Value = pavilion.Memo;
            param[7].Value = pavilion.TypeID;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertPavilion", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeletePavilion(string paID)
        {
            SqlParameter param = new SqlParameter("@PaID", SqlDbType.VarChar,3);
            param.Value = paID;
            int result = SqlHelp.ExecuteNonQuery("prc_DeletePavilion", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
