using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class FixRepairBLL
    {
        FixRepairDAL dal=new FixRepairDAL();
        public List<FixRepairModel> GetAllFixRepair()
        {
            return dal.GetAllFixRepair();
        }
        public FixRepairModel GetFixRepairByID(int id)
        {
            return dal.GetFixRepairByID(id);
        }
        public List<FixRepairModel> GetFixRepairByCondition(string condition)
        {
            return dal.GetFixRepairByCondition(condition);
        }
        public bool UpdateFixRepair(FixRepairModel fixRepair)
        {
            int result = dal.UpdateFixRepair(fixRepair);
            return result == 0 ? false : true;
        }
        public bool UpdateFixRepairForSign(FixRepairModel fixRepair)
        {
            int result = dal.UpdateFixRepairForSign(fixRepair);
            return result == 0 ? false : true;
        }
        public bool InsertFixRepair(FixRepairModel fixRepair)
        {
            int result = dal.InsertFixRepair(fixRepair);
            return result == 0 ? false : true;
        }
        public int DeleteFixRepair(int id)
        {
            return dal.DeleteFixRepair(id);
        }
    }
}
