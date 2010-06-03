using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class HomeParkBLL
    {
        HomeParkDAL dal = new HomeParkDAL();
        public List<HomeParkModel> GetAllHomePark()
        {
            return dal.GetAllHomePark();
        }
        public HomeParkModel GetHomeParkByID(int id)
        {
            return dal.GetHomeParkByID(id);
        }
        public bool UpdateHomePark(HomeParkModel homePark)
        {
            int result = dal.UpdateHomePark(homePark);
            return result == 0 ? false : true;
        }
        public bool InsertHomePark(HomeParkModel homePark)
        {
            int result = dal.InsertHomePark(homePark);
            return result == 0 ? false : true;
        }
        public bool DeleteHomePark(int id)
        {
            int result = dal.DeleteHomePark(id);
            return result == 0 ? false : true;
        }
    }
}
