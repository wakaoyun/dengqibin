using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class RoomDAL
    {
        public int CheckRooms(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 5);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_CheckRooms", CommandType.StoredProcedure, param);
            dr.Read();
            int result = Convert.ToInt32(dr[0]);
            return result;
        }
        public List<RoomModel> GetAllRoom()
        {
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllRoom", CommandType.StoredProcedure);
            List<RoomModel> list = new List<RoomModel>();
            while (dr.Read())
            {
                RoomModel room = new RoomModel();
                room.Code = dr[0].ToString();
                room.RoomID = dr[1].ToString();
                room.State = Convert.ToInt32(dr[2]);
                room.PaName = dr[3].ToString();
                room.SunnyName = dr[4].ToString();
                room.RoomUseName = dr[5].ToString();
                room.IndoorName = dr[6].ToString();
                room.RoomFormatName = dr[7].ToString();
                room.BuildArea = Convert.ToDouble(dr[8]);
                room.UseArea = Convert.ToDouble(dr[9]);
                list.Add(room);
            }
            dr.Close();
            return list;
        }
        public List<RoomModel> GetAllRoomUsed(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 5);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllRoomUsed", CommandType.StoredProcedure,param);
            List<RoomModel> list = new List<RoomModel>();
            while (dr.Read())
            {
                RoomModel room = new RoomModel();
                room.RoomID = dr[0].ToString();
                room.Code = dr[1].ToString();
                list.Add(room);
            }
            dr.Close();
            return list;
        }
        public List<RoomModel> GetAllRoomNotUsed(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 5);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetAllRoomNotUsed", CommandType.StoredProcedure, param);
            List<RoomModel> list = new List<RoomModel>();
            while (dr.Read())
            {
                RoomModel room = new RoomModel();
                room.RoomID = dr[0].ToString();
                room.Code = dr[1].ToString();
                list.Add(room);
            }
            dr.Close();
            return list;
        }
        public RoomModel GetRoomByID(string code)
        {
            SqlParameter param = new SqlParameter("@Code", SqlDbType.VarChar, 9);
            param.Value = code;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetRoomByID", CommandType.StoredProcedure, param);
            dr.Read();
            RoomModel room = new RoomModel();
            if (dr.HasRows)
            {
                room.Code = dr[0].ToString();
                room.RoomID = dr[1].ToString();
                room.State = Convert.ToInt32(dr[2]);
                room.PaName = dr[3].ToString();
                room.SunnyID = Convert.ToInt32(dr[4]);
                room.SunnyName = dr[5].ToString();
                room.RoomUseID = Convert.ToInt32(dr[6]);
                room.RoomUseName = dr[7].ToString();
                room.IndoorID = Convert.ToInt32(dr[8]);
                room.IndoorName = dr[9].ToString();
                room.RoomFormatID = Convert.ToInt32(dr[10]);
                room.RoomFormatName = dr[11].ToString();
                room.BuildArea = Convert.ToDouble(dr[12]);
                room.UseArea = Convert.ToDouble(dr[13]);
            }
            dr.Close();
            return room;
        }
        public List<RoomModel> GetRoomByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar, 255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetRoomByCondition", CommandType.StoredProcedure, param);
            List<RoomModel> list = new List<RoomModel>();
            while (dr.Read())
            {
                RoomModel room = new RoomModel();
                room.Code = dr[0].ToString();
                room.RoomID = dr[1].ToString();
                room.State = Convert.ToInt32(dr[2]);
                room.PaName = dr[3].ToString();
                room.SunnyName = dr[4].ToString();
                room.RoomUseName = dr[5].ToString();
                room.IndoorName = dr[6].ToString();
                room.RoomFormatName = dr[7].ToString();
                room.BuildArea = Convert.ToDouble(dr[8]);
                room.UseArea = Convert.ToDouble(dr[9]);
                list.Add(room);
            }
            dr.Close();
            return list;
        }
        public int UpdateRoom(RoomModel room)
        {
            SqlParameter[] param = { new SqlParameter("@Code", SqlDbType.VarChar, 9),  new SqlParameter("@SunnyID",SqlDbType.Int), 
                                     new SqlParameter("@IndoorID",SqlDbType.Int), new SqlParameter("@RoomUseID",SqlDbType.Int), 
                                     new SqlParameter("@RoomFormatID",SqlDbType.Int), new SqlParameter("@BuildArea",SqlDbType.Float), 
                                     new SqlParameter("@UseArea",SqlDbType.Float)
                                   };
            param[0].Value = room.Code;
            param[1].Value = room.SunnyID;
            param[2].Value = room.IndoorID;
            param[3].Value = room.RoomUseID;
            param[4].Value = room.RoomFormatID;
            param[5].Value = room.BuildArea;
            param[6].Value = room.UseArea;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateRoom", CommandType.StoredProcedure, param);
            return result;
        }
        //public int UpdateRoomForSale(string code,int state)
        //{
        //    SqlParameter[] param = { new SqlParameter("@Code", SqlDbType.VarChar, 9),new SqlParameter("@State",SqlDbType.SmallInt) };
        //    param[0].Value = code;
        //    param[1].Value = state;
        //    int result = SqlHelp.ExecuteNonQuery("prc_UpdateRoomForSale", CommandType.StoredProcedure, param);
        //    return result;
        //}
        public int InsertRoom(RoomModel[] room)
        {
            string[] commandText = new string[room.Length];
            SqlParameter[][] paramArray=new SqlParameter[room.Length][];
            for (int i = 0; i < room.Length; i++)
            {
                RoomModel items = room[i];
                SqlParameter[] param = { new SqlParameter("@Code", SqlDbType.VarChar, 9), new SqlParameter("@RoomID", SqlDbType.VarChar, 6),
                                         new SqlParameter("@PaID",SqlDbType.VarChar,3), new SqlParameter("@CellID",SqlDbType.Int),
                                         new SqlParameter("@SunnyID",SqlDbType.Int), new SqlParameter("@IndoorID",SqlDbType.Int),
                                         new SqlParameter("@RoomUseID",SqlDbType.Int), new SqlParameter("@RoomFormatID",SqlDbType.Int),
                                         new SqlParameter("@BuildArea",SqlDbType.Float), new SqlParameter("@UseArea",SqlDbType.Float)
                                       };
                param[0].Value = items.Code;
                param[1].Value = items.RoomID;
                param[2].Value = items.PaID;
                param[3].Value = items.CellID;
                param[4].Value = items.SunnyID;
                param[5].Value = items.IndoorID;
                param[6].Value = items.RoomUseID;
                param[7].Value = items.RoomFormatID;
                param[8].Value = items.BuildArea;
                param[9].Value = items.UseArea;
                
                paramArray[i] = param;
                commandText[i] = "prc_InsertRoom";
            }
            int result = SqlHelp.ExecuteNonQueryTransaction(commandText, CommandType.StoredProcedure, paramArray);
            return result;
        }
        public int DeleteRoom(string code)
        {
            SqlParameter param = new SqlParameter("@code", SqlDbType.VarChar, 9);
            param.Value = code;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteRoom", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
