using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class HomeReportBLL
    {
        HomeReportDAL dal = new HomeReportDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public bool InsertHomeReport(HomeReportModel homereport)
        {
            int result = dal.InsertHomeReport(homereport);
            return result == 0 ? false : true;
        }
        public List<HomeReportModel> ShowHomeReportByCode(string code)
        {
            return dal.ShowHomeReportByCode(code);
        }
        public List<HomeReportModel> GetHomeReport()
        {
            return dal.GetHomeReport();
        }
        public HomeReportModel ShowHomeReportByID(string id)
        {
            return dal.ShowHomeReportByID(id);
        }
        public bool UpdateHomeReport(HomeReportModel homereport)
        {
            int result = dal.UpdateHomeReport(homereport);
            return result == 0 ? false : true;
        }
        public bool UpdateHomeReportForProcess(HomeReportModel homereport)
        {
            int result = dal.UpdateHomeReportForProcess(homereport);
            return result == 0 ? false : true;
        }       
        public bool DeleteHomeReport(string id)
        {
            int result = dal.DeleteHomeReport(id);
            return result == 0 ? false : true;
        }
    }
}
