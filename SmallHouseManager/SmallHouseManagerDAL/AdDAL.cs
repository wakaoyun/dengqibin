using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class AdDAL
    {
        public List<AdModel> ShowAd()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_ShowAd", CommandType.StoredProcedure);            
            List<AdModel> list = new List<AdModel>();
            while (dr.Read())
            {
                AdModel ad = new AdModel();                
                ad.ID = Convert.ToInt32(dr[0]);
                ad.AdName = dr[1].ToString();
                ad.PhotoPath = dr[2].ToString();
                ad.Url = dr[3].ToString();
                ad.AddDate = Convert.ToDateTime(dr[4]);               
                list.Add(ad);
            }
            dr.Close();
            return list;
        }
        public List<AdModel> ShowAllAd()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_ShowAllAd", CommandType.StoredProcedure);
            List<AdModel> list = new List<AdModel>();
            while (dr.Read())
            {
                AdModel ad = new AdModel();
                ad.ID = Convert.ToInt32(dr[0]);
                ad.AdName = dr[1].ToString();
                ad.PhotoPath = @"~\" + dr[2].ToString();
                ad.Url = dr[3].ToString();
                ad.AddDate = Convert.ToDateTime(dr[4]);
                list.Add(ad);
            }
            dr.Close();
            return list;
        }
        public int InsertAd(AdModel ads)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@PhotoPath", SqlDbType.VarChar,255),
                                     new SqlParameter("@Url",SqlDbType.VarChar,255)
                                   };
            param[0].Value = ads.AdName;
            param[1].Value = ads.PhotoPath;
            param[2].Value = ads.Url;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertAd", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteAd(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteAd", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
