using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class FixBLL
    {
        FixDAL dal = new FixDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public List<FixModel> GetAllFix()
        {
            return dal.GetAllFix();
        }
        public FixModel GetFixByID(string fixID)
        {
            return dal.GetFixByID(fixID);
        }
        public List<FixModel> GetFixByCondition(string condition)
        {
            return dal.GetFixByCondition(condition);
        }
        public bool UpdateFix(FixModel fix)
        {
            int result = dal.UpdateFix(fix);
            return result == 0 ? false : true;
        }
        public bool InsertFix(FixModel fix)
        {
            int result = dal.InsertFix(fix);
            return result == 0 ? false : true;
        }        
        public int DeleteFix(string fixID)
        {
            return dal.DeleteFix(fixID);
        }
    }
}
