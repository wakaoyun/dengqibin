using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class UsersBLL
    {
        UsersDAL dal = new UsersDAL();
        public List<UserModel> UserLogin(string userName, string password)
        {
            return dal.UserLogin(userName, password);
        }
        public bool CheckUserByUID(string uid)
        {
            return dal.CheckUserByUID(uid);
        }
        public bool CheckUser(string uid, string password)
        {
            return dal.CheckUser(uid, password);
        }
        public int UpdateUID(int id, string uid)
        {
            return dal.UpdateUID(id, uid);
        }
        public int UpdatePassword(int id, string password)
        {
            return dal.UpdatePassword(id, password);
        }
        //public bool InsertUser(UserModel user)
        //{
        //    int result = dal.InsertUser(user);
        //    return result == 0 ? false : true;
        //}
    }
}
