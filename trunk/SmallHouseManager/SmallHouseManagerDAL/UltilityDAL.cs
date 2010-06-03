using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SmallHouseManagerDAL
{
    public class UltilityDAL
    {
        public bool BackupDataBase(string path)
        {
            SqlParameter param = new SqlParameter("@Path", SqlDbType.VarChar, 255);
            param.Value = path;
            try
            {
                SqlHelp.ExecuteNonQuery("prc_BackupDataBase", CommandType.StoredProcedure, param);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void Killspid(string dbname)
        {
            SqlParameter param = new SqlParameter("@dbname", SqlDbType.VarChar, 255);
            param.Value = dbname;
            SqlHelp.ExecuteNonQueryForMaster("prc_Killspid", CommandType.StoredProcedure, param);
        }
        public bool RestoreDataBase(string path)
        {
            SqlParameter param = new SqlParameter("@Path", SqlDbType.VarChar, 255);
            param.Value = path;
            try
            {
                SqlHelp.ExecuteNonQueryForMaster("restore database MyHouse from disk=@Path", CommandType.Text, param);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
