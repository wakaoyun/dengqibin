using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class RoomBLL
    {
        RoomDAL dal = new RoomDAL();
        public bool CheckRooms(string code)
        {
            int result = dal.CheckRooms(code);
            return result == 0 ? false : true;
        }
        public List<RoomModel> GetAllRoom()
        {
            return dal.GetAllRoom();
        }
        public List<RoomModel> GetAllRoomUsed(string code)
        {
            return dal.GetAllRoomUsed(code);
        }
        public List<RoomModel> GetAllRoomNotUsed(string code)
        {
            return dal.GetAllRoomNotUsed(code);
        }
        public RoomModel GetRoomByID(string code)
        {
            return dal.GetRoomByID(code);
        }
        public List<RoomModel> GetRoomByCondition(string condition)
        {
            return dal.GetRoomByCondition(condition);
        }
        public bool UpdateRoom(RoomModel room)
        {
            int result = dal.UpdateRoom(room);
            return result == 0 ? false : true;
        }
        //public bool UpdateRoomForSale(string code,int state)
        //{
        //    int result = dal.UpdateRoomForSale(code,state);
        //    return result == 0 ? false : true;
        //}
        public bool InsertRoom(RoomModel[] room)
        {
            int result = dal.InsertRoom(room);
            return result == 0 ? false : true;
        }
        public int DeleteRoom(string code)
        {
            return dal.DeleteRoom(code);
        }
    }
}
