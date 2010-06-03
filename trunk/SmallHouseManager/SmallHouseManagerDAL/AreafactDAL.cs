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
    public class AreafactDAL
    {
        public List<AreaFactModel> ShowAllAreafact()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowAllAreafact", CommandType.StoredProcedure);            
            List<AreaFactModel> list = new List<AreaFactModel>();
            while (dr.Read())
            {
                AreaFactModel arefact = new AreaFactModel();                
                arefact.ID = Convert.ToInt32(dr[0]);
                arefact.FactName = dr[1].ToString();
                arefact.MainHead = dr[2].ToString();
                arefact.Tel = dr[3].ToString();
                arefact.Memo = dr[5].ToString();
                arefact.TypeName = dr[6].ToString();                
                list.Add(arefact);
            }
            dr.Close();
            return list;
        }
        public AreaFactModel GetAreaFactByID(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAreaFactByID", CommandType.StoredProcedure,param);
            AreaFactModel areafact = new AreaFactModel();
            dr.Read();
            if (dr.HasRows)
            {
                areafact.ID = Convert.ToInt32(dr[0]);
                areafact.FactName = dr[1].ToString();
                areafact.MainHead = dr[2].ToString();
                areafact.Tel = dr[3].ToString();
                areafact.Memo = dr[5].ToString();
                areafact.TypeName = dr[6].ToString();
            }
            dr.Close();
            return areafact;
        }
        public List<AreaFactModel> GetAreaFactByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar,255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAreaFactByCondition", CommandType.StoredProcedure, param);
            List<AreaFactModel> list = new List<AreaFactModel>();
            while (dr.Read())
            {
                AreaFactModel arefact = new AreaFactModel();
                arefact.ID = Convert.ToInt32(dr[0]);
                arefact.FactName = dr[1].ToString();
                arefact.MainHead = dr[2].ToString();
                arefact.Tel = dr[3].ToString();
                arefact.Memo = dr[5].ToString();
                arefact.TypeName = dr[6].ToString();
                list.Add(arefact);
            }
            dr.Close();
            return list;
        }
        public int InsertAreaFact(AreaFactModel areafact)
        {
            SqlParameter[] param = { new SqlParameter("@FactName", SqlDbType.VarChar, 50), new SqlParameter("@MainHead", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,13),new SqlParameter("@TypeID",SqlDbType.Int),new SqlParameter("@Memo",SqlDbType.Text)
                                   };
            param[0].Value = areafact.FactName;
            param[1].Value = areafact.MainHead;
            param[2].Value = areafact.Tel;
            param[3].Value = areafact.TypeID;
            param[4].Value = areafact.Memo;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertAreaFact", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateAreaFact(AreaFactModel areafact)
        {
            SqlParameter[] param = { new SqlParameter("@FactName", SqlDbType.VarChar, 50), new SqlParameter("@MainHead", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,13),new SqlParameter("@TypeID",SqlDbType.Int),
                                     new SqlParameter("@Memo",SqlDbType.Text),new SqlParameter("@ID",SqlDbType.Int)
                                   };
            param[0].Value = areafact.FactName;
            param[1].Value = areafact.MainHead;
            param[2].Value = areafact.Tel;
            param[3].Value = areafact.TypeID;
            param[4].Value = areafact.Memo;
            param[5].Value = areafact.ID;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateAreaFact", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteAreaFact(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteAreaFact", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
