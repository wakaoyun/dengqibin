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
    public class TypeDAL
    {
        public List<TypeModel> GetType(string typeCode)
        {
            SqlParameter param = new SqlParameter("@TableName", SqlDbType.VarChar, 10);
            param.Value = typeCode;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetType", CommandType.StoredProcedure,param);
            List<TypeModel> list = new List<TypeModel>();
            while (dr.Read())
            {
                TypeModel type = new TypeModel();
                type.ID = Convert.ToInt32(dr[0]);
                type.Name = dr[1].ToString();
                list.Add(type);
            }
            dr.Close();
            return list;
        }
        public List<TypeModel> GetTypeByID(int typeID)
        {
            SqlParameter param = new SqlParameter("@TypeID", SqlDbType.Int);
            param.Value = typeID;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetTypeByID", CommandType.StoredProcedure, param);
            List<TypeModel> list = new List<TypeModel>();
            while (dr.Read())
            {
                TypeModel type = new TypeModel();
                type.ID = Convert.ToInt32(dr[0]);
                type.Name = dr[1].ToString();
                type.TypeName = dr[2].ToString();
                list.Add(type);
            }
            dr.Close();
            return list;
        }
        public List<TypeModel> GetCodeTable()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetCodeTable", CommandType.StoredProcedure);
            List<TypeModel> list = new List<TypeModel>();
            while (dr.Read())
            {
                TypeModel type = new TypeModel();
                type.ID = Convert.ToInt32(dr[0]);
                type.TypeName = dr[1].ToString();
                list.Add(type);
            }
            dr.Close();
            return list;
        }
        public int InsertType(TypeModel type)
        {
            SqlParameter[] param = { new SqlParameter("@TypeID", SqlDbType.Int), new SqlParameter("@Name", SqlDbType.VarChar, 20) };
            param[0].Value = type.TypeID;
            param[1].Value = type.Name;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertType", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
