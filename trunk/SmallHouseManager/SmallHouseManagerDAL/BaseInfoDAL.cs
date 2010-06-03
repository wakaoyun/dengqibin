using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class BaseInfoDAL
    {
        public List<BaseInfoModel> ShowBaseInfo()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowBaseInfo", CommandType.StoredProcedure);            
            dr.Read();
            List<BaseInfoModel> list = new List<BaseInfoModel>();
            if (dr.HasRows)
            {
                BaseInfoModel baseInfo = new BaseInfoModel();                
                baseInfo.ID = Convert.ToInt32(dr[0]);
                baseInfo.HomeName = dr[1].ToString();
                baseInfo.MainHead = dr[2].ToString();
                baseInfo.BuildDate = Convert.ToDateTime(dr[3]);
                baseInfo.BuildArea = Convert.ToDouble(dr[4]);
                baseInfo.Amount = Convert.ToInt32(dr[5]);
                baseInfo.Address = dr[6].ToString();
                baseInfo.GreenArea = Convert.ToDouble(dr[7]);
                baseInfo.RoadArea = Convert.ToDouble(dr[8]);
                baseInfo.Tel = dr[9].ToString();
                baseInfo.ParkingArea = Convert.ToDouble(dr[10]);
                baseInfo.Memo = dr[11].ToString();                
                list.Add(baseInfo);
            }
            dr.Close();
            return list;
        }
        public int InsertBaseInfo(BaseInfoModel baseinfo)
        {
            SqlParameter[] param = { new SqlParameter("@HomeName", SqlDbType.VarChar, 20), new SqlParameter("@MainHead", SqlDbType.VarChar, 20),
                                     new SqlParameter("@BuildDate",SqlDbType.DateTime,10),new SqlParameter("@BuildArea",SqlDbType.Float),
                                     new SqlParameter("@Amount",SqlDbType.Int,4),new SqlParameter("@Address",SqlDbType.VarChar,255),
                                     new SqlParameter("@GreenArea",SqlDbType.Float),new SqlParameter("@RoadArea",SqlDbType.Float),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,18),new SqlParameter("@ParkingArea",SqlDbType.Float),
                                     new SqlParameter("@Memo",SqlDbType.Text)
                                   };
            param[0].Value = baseinfo.HomeName;
            param[1].Value = baseinfo.MainHead;
            param[2].Value = baseinfo.BuildDate;
            param[3].Value = baseinfo.BuildArea;
            param[4].Value = baseinfo.Amount;
            param[5].Value = baseinfo.Address;
            param[6].Value = baseinfo.GreenArea;
            param[7].Value = baseinfo.RoadArea;
            param[8].Value = baseinfo.Tel;
            param[9].Value = baseinfo.ParkingArea;
            param[10].Value = baseinfo.Memo;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertBaseInfo", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateBaseInfo(BaseInfoModel baseinfo)
        {
            SqlParameter[] param = { new SqlParameter("@HomeName", SqlDbType.VarChar, 20), new SqlParameter("@MainHead", SqlDbType.VarChar, 20),
                                     new SqlParameter("@BuildDate",SqlDbType.DateTime,10),new SqlParameter("@BuildArea",SqlDbType.Float),
                                     new SqlParameter("@Amount",SqlDbType.Int,4),new SqlParameter("@Address",SqlDbType.VarChar,255),
                                     new SqlParameter("@GreenArea",SqlDbType.Float),new SqlParameter("@RoadArea",SqlDbType.Float),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,18),new SqlParameter("@ParkingArea",SqlDbType.Float),
                                     new SqlParameter("@Memo",SqlDbType.Text),new SqlParameter("@ID",SqlDbType.Int,4)
                                   };
            param[0].Value = baseinfo.HomeName;
            param[1].Value = baseinfo.MainHead;
            param[2].Value = baseinfo.BuildDate;
            param[3].Value = baseinfo.BuildArea;
            param[4].Value = baseinfo.Amount;
            param[5].Value = baseinfo.Address;
            param[6].Value = baseinfo.GreenArea;
            param[7].Value = baseinfo.RoadArea;
            param[8].Value = baseinfo.Tel;
            param[9].Value = baseinfo.ParkingArea;
            param[10].Value = baseinfo.Memo;
            param[11].Value = baseinfo.ID;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateBaseInfo", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
