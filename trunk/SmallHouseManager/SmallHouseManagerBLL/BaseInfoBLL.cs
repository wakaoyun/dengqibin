using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class BaseInfoBLL
    {
        BaseInfoDAL dal = new BaseInfoDAL();
        public List<BaseInfoModel> ShowBaseInfo()
        {
            return dal.ShowBaseInfo();
        }
        public bool InsertBaseInfo(BaseInfoModel baseinfo)
        {
            int result = dal.InsertBaseInfo(baseinfo);
            return result == 0 ? false : true;
        }
        public bool UpdateBaseInfo(BaseInfoModel baseinfo)
        {
            int result = dal.UpdateBaseInfo(baseinfo);
            return result == 0 ? false : true;
        }
    }
}
