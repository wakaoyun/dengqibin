using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class HomeHoldBLL
    {
        HomeHoldDAL dal = new HomeHoldDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public HomeHoldModel ShowHomeHoldByID(string id)
        {
            return dal.ShowHomeHoldByID(id);
        }
        public List<HomeHoldModel> GetAllHomeHold()
        {
            return dal.GetAllHomeHold();
        }
        public bool InsertHomeHold(HomeHoldModel homeHold,UserModel user,string code,int state)
        {
            int result;
            try
            {
                result = dal.InsertHomeHold(homeHold, user, code, state);
            }
            catch
            {
                return false;
            }
            return result == 0 ? false : true;
        }
        public bool DeleteHomeHold(int id,string code,int state)
        {
            int result;
            try
            {
                result = dal.DeleteHomeHold(id, code, state);
            }
            catch
            {
                return false;
            }
            return result == 0 ? false : true;
        }
    }
}
