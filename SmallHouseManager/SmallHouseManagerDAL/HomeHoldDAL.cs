using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class HomeHoldDAL
    {
        public int GetMaxID()
        {
            int id = 0;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeHoldMaxID", CommandType.StoredProcedure);
            dr.Read();
            if (dr[0].ToString() != "")
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return id;
        }
        public HomeHoldModel ShowHomeHoldByID(string id)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.NVarChar, 20);
            param.Value = id;            
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_ShowHomeHoldByID", CommandType.StoredProcedure, param);            
            dr.Read();
            HomeHoldModel homeHold = new HomeHoldModel();
            if (dr.HasRows)
            {                
                homeHold.UserName = dr[0].ToString();
                homeHold.Tel = dr[1].ToString();
                homeHold.Contact = dr[2].ToString();
                homeHold.Email = dr[3].ToString();
                homeHold.OwnerID = dr[4].ToString();
                homeHold.Unit = dr[5].ToString();
                homeHold.Mobile = dr[6].ToString();
                homeHold.CardID = dr[7].ToString();
                homeHold.Memo = dr[8].ToString();               
            }
            dr.Close();
            return homeHold;
        }
        public List<HomeHoldModel> GetAllHomeHold()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetHomeHold", CommandType.StoredProcedure);
            List<HomeHoldModel> list = new List<HomeHoldModel>();
            while (dr.Read())
            {
                HomeHoldModel homeHold = new HomeHoldModel();
                homeHold.ID = Convert.ToInt32(dr[0]);
                homeHold.Code = dr[1].ToString();
                homeHold.UserName = dr[2].ToString();
                homeHold.UID = dr[3].ToString();
                homeHold.Tel = dr[4].ToString();
                homeHold.Contact = dr[5].ToString();
                homeHold.Mobile = dr[6].ToString();
                homeHold.Email = dr[7].ToString();
                homeHold.CardID = dr[8].ToString();
                homeHold.OwnerID = dr[9].ToString();
                homeHold.Unit = dr[10].ToString();
                homeHold.RoomID = dr[11].ToString();
                list.Add(homeHold);
            }
            dr.Close();
            return list;
        }
        public int InsertHomeHold(HomeHoldModel homeHold,UserModel user,string code,int state)
        {
            SqlParameter[] param = { new SqlParameter("@Code", SqlDbType.VarChar, 9), new SqlParameter("@Name", SqlDbType.VarChar, 20),
                                     new SqlParameter("@Tel",SqlDbType.VarChar,13),new SqlParameter("@Contact",SqlDbType.VarChar,50),
                                     new SqlParameter("@Mobile",SqlDbType.VarChar,11),new SqlParameter("@Email",SqlDbType.VarChar,40),
                                     new SqlParameter("@CardID",SqlDbType.VarChar,19),new SqlParameter("@OwnerID",SqlDbType.VarChar,20),
                                     new SqlParameter("@Unit",SqlDbType.VarChar,20),new SqlParameter("@Memo",SqlDbType.Text)
                                   };
            param[0].Value = homeHold.Code;
            param[1].Value = homeHold.UserName;
            param[2].Value = homeHold.Tel;
            param[3].Value = homeHold.Contact;
            param[4].Value = homeHold.Mobile;
            param[5].Value = homeHold.Email;
            param[6].Value = homeHold.CardID;
            param[7].Value = homeHold.OwnerID;
            param[8].Value = homeHold.Unit;
            param[9].Value = homeHold.Memo;

            SqlParameter[] param1 = { new SqlParameter("@UID", SqlDbType.VarChar, 20), new SqlParameter("@Password", SqlDbType.VarChar,20),
                                     new SqlParameter("@UserType",SqlDbType.SmallInt),new SqlParameter("@SubID",SqlDbType.Int)
                                   };
            param1[0].Value = user.UID;
            param1[1].Value = user.Password;
            param1[2].Value = user.UserType;
            param1[3].Value = user.SubID;

            SqlParameter[] param2 = { new SqlParameter("@Code", SqlDbType.VarChar, 9), new SqlParameter("@State", SqlDbType.Int) };
            param2[0].Value = code;
            param2[1].Value = state;
            string[] commandText = { "prc_InsertHomeHold", "prc_InsertUser", "prc_UpdateRoomForSale" };
            SqlParameter[][] paramArray = { param, param1, param2 };
            int result = SqlHelp.ExecuteNonQueryTransaction(commandText, CommandType.StoredProcedure, paramArray);
            return result;
        }
        public int DeleteHomeHold(int id,string code,int state)
        {
            SqlParameter[] param = {new SqlParameter("@ID", SqlDbType.Int)};
            SqlParameter[] param1 = { new SqlParameter("@Code", SqlDbType.VarChar,9),new SqlParameter("@State",SqlDbType.Int) };
            param[0].Value = id;
            param1[0].Value = code;
            param1[1].Value=state;
            SqlParameter[][] paramArray={param,param1};
            string[] commandText = { "prc_DeleteHomeHold", "prc_UpdateRoomForSale" };
            int result = SqlHelp.ExecuteNonQueryTransaction(commandText, CommandType.StoredProcedure, paramArray);
            return result;
        }
    }
}
