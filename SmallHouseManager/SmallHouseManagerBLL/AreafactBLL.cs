using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class AreafactBLL
    {
        AreafactDAL dal = new AreafactDAL();
        public List<AreaFactModel> ShowAllAreafact()
        {
            return dal.ShowAllAreafact();
        }
        public AreaFactModel GetAreaFactByID(int id)
        {
            return dal.GetAreaFactByID(id);
        }
        public List<AreaFactModel> GetAreaFactByCondition(string condition)
        {
            return dal.GetAreaFactByCondition(condition);
        }
        public bool InsertAreaFact(AreaFactModel areafact)
        {
            int result = dal.InsertAreaFact(areafact);
            return result == 0 ? false : true;
        }
        public bool UpdateAreaFact(AreaFactModel areafact)
        {
            int result = dal.UpdateAreaFact(areafact);
            return result == 0 ? false : true;
        }
        public int DeleteAreaFact(int id)
        {
            return dal.DeleteAreaFact(id);
        }
    }
}
