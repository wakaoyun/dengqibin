using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class BaseParkBLL
    {
        BaseParkDAL dal = new BaseParkDAL();
        public bool CheckHomePark(int parkID)
        {
            int result = dal.CheckHomePark(parkID);
            return result == 0 ? false : true;
        }
        public List<BaseParkModel> GetBasePark()
        {
            return dal.GetBasePark();
        }
        public BaseParkModel GetBaseParkByID(int parkID)
        {
            return dal.GetBaseParkByID(parkID);
        }
        public bool UpdateBasePark(BaseParkModel basePark)
        {
            int result = dal.UpdateBasePark(basePark);
            return result == 0 ? false : true;
        }
        public bool InsertBasePark(BaseParkModel basePark)
        {
            int result = dal.InsertBasePark(basePark);
            return result == 0 ? false : true;
        }
        public int DeleteBasePark(int parkID)
        {
            return dal.DeleteBasePark(parkID);
        }
    }
}
