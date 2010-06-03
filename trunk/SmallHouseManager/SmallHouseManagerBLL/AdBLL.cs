using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class AdBLL
    {
        AdDAL dal = new AdDAL();
        public List<AdModel> ShowAd()
        {
            return dal.ShowAd();
        }
        public List<AdModel> ShowAllAd()
        {
            return dal.ShowAllAd();
        }
        public bool InsertAd(AdModel ad)
        {
            int result = dal.InsertAd(ad);
            return result == 0 ? false : true;
        }
        public bool DeleteAd(int id)
        {
            int result = dal.DeleteAd(id);
            return result == 0 ? false : true;
        }
    }
}
