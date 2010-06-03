using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class TypeBLL
    {
        TypeDAL dal = new TypeDAL();
        public List<TypeModel> GetType(string typeCode)
        {
            return dal.GetType(typeCode);
        }
        public List<TypeModel> GetTypeByID(int typeID)
        {
            return dal.GetTypeByID(typeID);
        }
        public List<TypeModel> GetCodeTable()
        {
            return dal.GetCodeTable();
        }
        public bool InsertType(TypeModel type)
        {
            int result = dal.InsertType(type);
            return result == 0 ? false : true;
        }
    }
}
