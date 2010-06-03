using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class HomeFreeBLL
    {
        HomeFreeDAL dal = new HomeFreeDAL();
        public List<HomeFreeModel> GetHomeFreeByCode(string code)
        {
            return dal.GetHomeFreeByCode(code);
        }
        public List<HomeFreeModel> GetHomeFree()
        {
            return dal.GetHomeFree();
        }
        public bool UpdateHomeFreeForBargian(HomeFreeModel homeFree)
        {
            int result = dal.UpdateHomeFreeForBargian(homeFree);
            return result == 0 ? false : true;
        }
        public bool InsertHomeFree(HomeFreeModel homeFree)
        {
            int result = dal.InsertHomeFree(homeFree);
            return result == 0 ? false : true;
        }
        public bool DeleteHomeFree(int id)
        {
            int result = dal.DeleteHomeFree(id);
            return result == 0 ? false : true;
        }
    }
}
