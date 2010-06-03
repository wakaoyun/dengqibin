using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class PavilionBLL
    {
        PavilionDAL dal = new PavilionDAL();
        public int GetMaxID()
        {
            return dal.GetMaxID();
        }
        public bool CheckRoom(string paID)
        {
            int result = dal.CheckRoom(paID);
            return result == 0 ? false : true;
        }
        public List<PavilionModel> GetAllPavilion()
        {
            return dal.GetAllPavilion();
        }
        public PavilionModel GetPavilionByID(string paID)
        {
            return dal.GetPavilionByID(paID);
        }
        public List<PavilionModel> GetPavilionByCondition(string condition)
        {
            return dal.GetPavilionByCondition(condition);
        }
        public bool UpdatePavilion(PavilionModel pavilion)
        {
            int result = dal.UpdatePavilion(pavilion);
            return result == 0 ? false : true;
        }
        public bool InsertPavilion(PavilionModel pavilion)
        {
            int result = dal.InsertPavilion(pavilion);
            return result == 0 ? false : true;
        }
        public int DeletePavilion(string paID)
        {
            return dal.DeletePavilion(paID);
        }
    }
}
