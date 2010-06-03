using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class ChargeFreeTypeDAL
    {
        public List<ChargeFreeTypeModel> GetChargeFreeType()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetChargeFreeType", CommandType.StoredProcedure);
            List<ChargeFreeTypeModel> list = new List<ChargeFreeTypeModel>();
            while (dr.Read())
            {
                ChargeFreeTypeModel chargefree = new ChargeFreeTypeModel();
                chargefree.TypeID = Convert.ToInt32(dr[0]);
                chargefree.TypeName = dr[1].ToString();
                chargefree.Price = Convert.ToDouble(dr[2]);
                chargefree.Format = dr[3].ToString();
                list.Add(chargefree);
            }
            dr.Close();
            return list;
        }
        public ChargeFreeTypeModel GetChargeFreeTypeByID(int typeID)
        {
            SqlParameter param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Value = typeID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetChargeFreeTypeByID", CommandType.StoredProcedure, param);
            dr.Read();
            ChargeFreeTypeModel chargefree = new ChargeFreeTypeModel();
            if (dr.HasRows)
            {
                chargefree.TypeName = dr[0].ToString();
                chargefree.Price = Convert.ToDouble(dr[1]);
                chargefree.Format = dr[2].ToString();
            }
            dr.Close();
            return chargefree;
        }
        public int UpdateChargeFreeType(ChargeFreeTypeModel chargeFeeType)
        {
            SqlParameter[] param = { new SqlParameter("@TypeID", SqlDbType.Int), new SqlParameter("@Price", SqlDbType.Float) };
            param[0].Value = chargeFeeType.TypeID;
            param[1].Value = chargeFeeType.Price;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateChargeFreeType", CommandType.StoredProcedure, param);
            return result;
        }
        public int InsertChargeFeeType(ChargeFreeTypeModel chargeFeeType)
        {
            SqlParameter[] param = { new SqlParameter("@Name", SqlDbType.VarChar, 20), new SqlParameter("@Price", SqlDbType.Float),
                                     new SqlParameter("@Format",SqlDbType.VarChar,20)
                                   };
            param[0].Value = chargeFeeType.TypeName;
            param[1].Value = chargeFeeType.Price;
            param[2].Value = chargeFeeType.Format;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertChargeFeeType", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
