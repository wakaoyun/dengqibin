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
    public class HomeReportDAL
    {
        public int GetMaxID()
        {
            int id = -1;
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_GetHomeReportMaxID", CommandType.StoredProcedure);            
            dr.Read();
            if (dr[0].ToString()!="")
            {
                id=Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }
        public int InsertHomeReport(HomeReportModel homereport)
        {            
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 20), new SqlParameter("@Code", SqlDbType.VarChar, 9),
                                     new SqlParameter("@ReportText",SqlDbType.Text),new SqlParameter("@ReportMemo",SqlDbType.Text)
                                   };
            param[0].Value = homereport.ID;
            param[1].Value = homereport.Code;
            param[2].Value = homereport.ReportText;
            param[3].Value = homereport.ReportMemo;            
            int result = SqlHelp.ExecuteNonQuery("prc_InsertHomeReport", CommandType.StoredProcedure, param);            
            return result;
        }
        public List<HomeReportModel> ShowHomeReportByCode(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 20);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeReportByCode", CommandType.StoredProcedure, param);
            List<HomeReportModel> list = new List<HomeReportModel>();
            while (dr.Read())
            {
                HomeReportModel homereport = new HomeReportModel();
                homereport.ID = dr[0].ToString();
                homereport.Code = dr[1].ToString();
                homereport.ReportDate = Convert.ToDateTime(dr[2]);
                homereport.ReportText = dr[3].ToString();
                homereport.Sign = Convert.ToInt32(dr[4]);

                list.Add(homereport);
            }
            dr.Close();
            return list;
        }
        public List<HomeReportModel> GetHomeReport()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeReport", CommandType.StoredProcedure);
            List<HomeReportModel> list = new List<HomeReportModel>();
            while (dr.Read())
            {
                HomeReportModel homereport = new HomeReportModel();
                homereport.ID = dr[0].ToString();
                homereport.Code = dr[1].ToString();
                homereport.ReportDate = Convert.ToDateTime(dr[2]);
                homereport.ReportText = dr[3].ToString();
                homereport.Sign = Convert.ToInt32(dr[4]);
                homereport.RoomID = dr[5].ToString();
                list.Add(homereport);
            }
            dr.Close();
            return list;
        }
        public HomeReportModel ShowHomeReportByID(string id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            param.Value = id;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeReportByID", CommandType.StoredProcedure, param);
            HomeReportModel homereport = new HomeReportModel();
            dr.Read();
            if (dr.HasRows)
            {
                homereport.ID = dr[0].ToString();
                homereport.ReportText = dr[1].ToString();
                homereport.ReportMemo = dr[2].ToString();
                homereport.ReportDate = (DateTime)dr[3];
                homereport.FinshText = dr[4].ToString();
            }
            dr.Close();
            return homereport;
        }
        public int UpdateHomeReport(HomeReportModel homereport)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@ReportDate", SqlDbType.DateTime, 10),
                                     new SqlParameter("@ReportText",SqlDbType.Text),new SqlParameter("@ReportMemo",SqlDbType.Text)
                                   };
            param[0].Value = homereport.ID;
            param[1].Value = homereport.ReportDate;
            param[2].Value = homereport.ReportText;
            param[3].Value = homereport.ReportMemo;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeReport", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateHomeReportForProcess(HomeReportModel homereport)
        {
            SqlParameter[] param = { new SqlParameter("@ID", SqlDbType.VarChar, 18), new SqlParameter("@FinishText", SqlDbType.Text) };
            param[0].Value = homereport.ID;
            param[1].Value = homereport.FinshText;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateHomeReportForProcess", CommandType.StoredProcedure, param);
            return result;
        }       
        public int DeleteHomeReport(string id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteHomeReport", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
