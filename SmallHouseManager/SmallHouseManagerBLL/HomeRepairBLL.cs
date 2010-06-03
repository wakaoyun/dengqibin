using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class HomeRepairBLL
    {
        HomeRepairDAL dal = new HomeRepairDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public bool InsertHomeRepair(HomeRepairModel homerepair)
        {
            int result = dal.InsertHomeRepair(homerepair);
            return result == 0 ? false : true;
        }
        public List<HomeRepairModel> ShowHomeRepairByCode(string code)
        {
            return dal.ShowHomeRepairByCode(code);
        }
        public List<HomeRepairModel> GetHomeRepair()
        {
            return dal.GetHomeRepair();
        }
        public HomeRepairModel ShowHomeRepairByID(string id)
        {
            return dal.ShowHomeRepairByID(id);
        }
        public bool UpdateHomeRepair(HomeRepairModel homerepair)
        {
            int result = dal.UpdateHomeRepair(homerepair);
            return result == 0 ? false : true;
        }
        public bool UpdateHomeRepairForProcess(HomeRepairModel homerepair)
        {
            int result = dal.UpdateHomeRepairForProcess(homerepair);
            return result == 0 ? false : true;
        }
        public bool UpdateHomeRepairCheck(HomeRepairModel homerepair)
        {
            int result = dal.UpdateHomeRepairCheck(homerepair);
            return result == 0 ? false : true;
        }
        public bool DeleteHomeRepair(string id)
        {
            int result = dal.DeleteHomeRepair(id);
            return result == 0 ? false : true;
        }
    }
}
