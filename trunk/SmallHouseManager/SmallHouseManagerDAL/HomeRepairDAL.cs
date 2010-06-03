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
    public class HomeRepairDAL
    {
        public int GetMaxID()
        {
            int id = -1;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_GetHomeRepairMaxID", CommandType.StoredProcedure);            
            dr.Read();
            if (dr[0].ToString() != "")
            {
                id=Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }
        public int InsertHomeRepair(HomeRepairModel homerepair)
        {            
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@Code", SqlDbType.VarChar, 9),
                                     new SqlParameter("@RepairText",SqlDbType.Text),new SqlParameter("@RepairMemo",SqlDbType.Text)
                                   };
            param[0].Value = homerepair.ID;
            param[1].Value = homerepair.Code;
            param[2].Value = homerepair.RepairText;
            param[3].Value = homerepair.RepairMemo;            
            int result = SqlHelp.ExecuteNonQuery("prc_InsertHomeRepair", CommandType.StoredProcedure, param);            
            return result;
        }
        public List<HomeRepairModel> ShowHomeRepairByCode(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 20);
            param.Value = code;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_GetHomeRepairByCode", CommandType.StoredProcedure, param);            
            List<HomeRepairModel> list = new List<HomeRepairModel>();
            while (dr.Read())
            {
                HomeRepairModel homerepair = new HomeRepairModel();
                homerepair.ID = dr[0].ToString();
                homerepair.Code = dr[1].ToString();
                homerepair.RepairDate = Convert.ToDateTime(dr[2]);
                homerepair.RepairText = dr[3].ToString();
                homerepair.Sign = Convert.ToInt32(dr[4]);

                list.Add(homerepair);
            }
            dr.Close();
            return list;
        }
        public List<HomeRepairModel> GetHomeRepair()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeRepair", CommandType.StoredProcedure);
            List<HomeRepairModel> list = new List<HomeRepairModel>();
            while (dr.Read())
            {
                HomeRepairModel homerepair = new HomeRepairModel();
                homerepair.ID = dr[0].ToString();
                homerepair.Code = dr[1].ToString();
                homerepair.RepairDate = Convert.ToDateTime(dr[2]);
                homerepair.RepairText = dr[3].ToString();
                homerepair.Sign = Convert.ToInt32(dr[4]);
                homerepair.RoomID = dr[5].ToString();
                list.Add(homerepair);
            }
            dr.Close();
            return list;
        }
        public HomeRepairModel ShowHomeRepairByID(string id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.VarChar, 18);
            param.Value = id;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_GetHomeRepairByID", CommandType.StoredProcedure, param);           
            HomeRepairModel homerepair= new HomeRepairModel();
            dr.Read();
            if (dr.HasRows)
            {
                homerepair.ID = dr[0].ToString();
                homerepair.RepairText = dr[1].ToString();
                homerepair.RepairMemo = dr[2].ToString();
                homerepair.RepairDate = (DateTime)dr[3];
                homerepair.Varperson = dr[4].ToString();
                homerepair.VarText = dr[5].ToString();
                homerepair.RepairUnit = dr[6].ToString();
                homerepair.OwnerText = dr[7].ToString();
            }
            dr.Close();
            return homerepair;
        }
        public int UpdateHomeRepair(HomeRepairModel homerepair)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@RepairDate", SqlDbType.DateTime, 10),
                                     new SqlParameter("@RepairText",SqlDbType.Text),new SqlParameter("@RepairMemo",SqlDbType.Text)
                                   };
            param[0].Value = homerepair.ID;
            param[1].Value = homerepair.RepairDate;
            param[2].Value = homerepair.RepairText;
            param[3].Value = homerepair.RepairMemo;            
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeRepair", CommandType.StoredProcedure, param);           
            return result;
        }
        public int UpdateHomeRepairForProcess(HomeRepairModel homerepair)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@Varperson", SqlDbType.VarChar, 20),
                                     new SqlParameter("@VarText",SqlDbType.Text),new SqlParameter("@RepairUnit",SqlDbType.VarChar,50)
                                   };
            param[0].Value = homerepair.ID;
            param[1].Value = homerepair.Varperson;
            param[2].Value = homerepair.VarText;
            param[3].Value = homerepair.RepairUnit;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeRepairForProcess", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateHomeRepairCheck(HomeRepairModel homerepair)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@OwnerText", SqlDbType.Text) };
            param[0].Value = homerepair.ID;
            param[1].Value = homerepair.OwnerText;            
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeRepairCheck", CommandType.StoredProcedure, param);           
            return result;
        }
        public int DeleteHomeRepair(string id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.VarChar, 18);
            param.Value = id;            
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteHomeRepair", CommandType.StoredProcedure, param);            
            return result;
        }
    }
}
