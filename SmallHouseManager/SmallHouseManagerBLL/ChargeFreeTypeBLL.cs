using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class ChargeFreeTypeBLL
    {
        ChargeFreeTypeDAL dal = new ChargeFreeTypeDAL();
        public List<ChargeFreeTypeModel> GetChargeFreeType()
        {
            return dal.GetChargeFreeType();
        }
        public ChargeFreeTypeModel GetChargeFreeTypeByID(int typeID)
        {
            return dal.GetChargeFreeTypeByID(typeID);
        }
        public bool UpdateChargeFreeType(ChargeFreeTypeModel chargeFeeType)
        {
            int result = dal.UpdateChargeFreeType(chargeFeeType);
            return result == 0 ? false : true;
        }
        public bool InsertChargeFeeType(ChargeFreeTypeModel chargeFreeType)
        {
            int result = dal.InsertChargeFeeType(chargeFreeType);
            return result == 0 ? false : true;
        }
    }
}
