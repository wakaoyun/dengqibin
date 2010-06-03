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
    public class HomeFreeDAL
    {
        public List<HomeFreeModel> GetHomeFreeByCode(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 20);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeFreeByCode", CommandType.StoredProcedure, param);
            List<HomeFreeModel> list = new List<HomeFreeModel>();
            while (dr.Read())
            {
                HomeFreeModel homefree = new HomeFreeModel();
                homefree.TypeName = dr[0].ToString();
                homefree.Number = Convert.ToDouble(dr[1]);
                homefree.Price = Convert.ToDouble(dr[2]);
                homefree.Payment = Convert.ToDouble(dr[3]);
                homefree.NotPayment = Convert.ToDouble(dr[4]);
                homefree.StartDate = Convert.ToDateTime(dr[5]);
                homefree.PayDate = Convert.ToDateTime(dr[6]);
                homefree.AddName = dr[7].ToString();
                list.Add(homefree);
            }
            dr.Close();
            return list;
        }
        public List<HomeFreeModel> GetHomeFree()
        {            
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeFree", CommandType.StoredProcedure);
            List<HomeFreeModel> list = new List<HomeFreeModel>();
            while (dr.Read())
            {
                HomeFreeModel homefree = new HomeFreeModel();
                homefree.ID = Convert.ToInt32(dr[0]);
                homefree.TypeName = dr[1].ToString();
                homefree.Payment = Convert.ToDouble(dr[2]);
                homefree.NotPayment = Convert.ToDouble(dr[3]);
                homefree.StartDate = Convert.ToDateTime(dr[4]);
                homefree.AddName = dr[5].ToString();
                homefree.HandleName = dr[6].ToString();
                homefree.RoomID = dr[7].ToString();
                list.Add(homefree);
            }
            dr.Close();
            return list;
        }
        public int UpdateHomeFreeForBargian(HomeFreeModel homeFree)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.Int), new SqlParameter("@FactPayMent", SqlDbType.Money),
                                     new SqlParameter("@HandleName",SqlDbType.VarChar,20)
                                   };
            param[0].Value = homeFree.ID;
            param[1].Value = homeFree.FactPayment;
            param[2].Value = homeFree.HandleName;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeFreeForBargian", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertHomeFree(HomeFreeModel homeFree)
        {
            SqlParameter[] param = { new SqlParameter("@Code", SqlDbType.VarChar, 9), new SqlParameter("@TypeID", SqlDbType.Int),
                                     new SqlParameter("@Number",SqlDbType.Float),new SqlParameter("@StartDate",SqlDbType.DateTime,10),
                                     new SqlParameter("@PayDate",SqlDbType.DateTime,10),new SqlParameter("@AddName",SqlDbType.VarChar,20)
                                   };
            param[0].Value = homeFree.Code;
            param[1].Value = homeFree.TypeID;
            param[2].Value = homeFree.Number;
            param[3].Value = homeFree.StartDate;
            param[4].Value = homeFree.PayDate;
            param[5].Value = homeFree.AddName;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertHomeChargeFee", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteHomeFree(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteHomeFree", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
