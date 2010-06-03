using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmallHouseManagerModel;

namespace SmallHouseManagerDAL
{
    public class NewsDAL
    {
        public List<NewsModel> ShowNews()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowNews", CommandType.StoredProcedure);            
            List<NewsModel> list = new List<NewsModel>();
            while (dr.Read())
            {
                NewsModel news = new NewsModel();
                news.ID = Convert.ToInt32(dr[0]);
                news.Title = dr[1].ToString();
                news.NoticeDate = Convert.ToDateTime(dr[2]);
                news.NoticeContent = dr[3].ToString();
                list.Add(news);
            }
            dr.Close();
            return list;
        }
        public NewsModel ShowNewsByID(int id)
        {
            SqlParameter param=new SqlParameter("@ID",SqlDbType.Int,5);
            param.Value=id;            
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_ShowNewsByID", CommandType.StoredProcedure,param);                  
            dr.Read();
            NewsModel news = new NewsModel();
            if (dr.HasRows)
            {
                news.Title = dr[0].ToString();
                news.NoticeDate = Convert.ToDateTime(dr[1]);
                news.NoticeContent = dr[2].ToString();
            }
            dr.Close();
            return news;
        }
        public List<NewsModel> ShowAllNews()
        {
            SqlDataReader dr= SqlHelp.ExecuteReader("prc_ShowAllNews", CommandType.StoredProcedure);           
            List<NewsModel> list = new List<NewsModel>();
            while (dr.Read())
            {
                NewsModel news = new NewsModel();
                news.ID = Convert.ToInt32(dr[0]);
                news.Title = dr[1].ToString();
                news.NoticeDate = Convert.ToDateTime(dr[2]);
                news.NoticeContent = dr[3].ToString();
                news.NoticePerson = dr[4].ToString();
                list.Add(news);
            }
            dr.Close();
            return list;
        }
        public List<NewsModel> GetNewsByCondition(string condition)
        {
            SqlParameter param = new SqlParameter("@Condition", SqlDbType.VarChar, 255);
            param.Value = condition;
            SqlDataReader dr = SqlHelp.ExecuteReader("prc_GetNewsByCondition", CommandType.StoredProcedure,param);
            List<NewsModel> list = new List<NewsModel>();
            while (dr.Read())
            {
                NewsModel news = new NewsModel();
                news.ID = Convert.ToInt32(dr[0]);
                news.Title = dr[1].ToString();
                news.NoticeDate = Convert.ToDateTime(dr[2]);
                news.NoticeContent = dr[3].ToString();
                news.NoticePerson = dr[4].ToString();
                list.Add(news);
            }
            dr.Close();
            return list;
        }
        public int InsertNotice(NewsModel news)
        {
            SqlParameter[] param = { new SqlParameter("@Title", SqlDbType.VarChar, 50), new SqlParameter("@NoticeContent", SqlDbType.Text),
                                     new SqlParameter("@NoticePerSon",SqlDbType.VarChar,20)
                                   };
            param[0].Value = news.Title;
            param[1].Value = news.NoticeContent;
            param[2].Value = news.NoticePerson;
            int result = SqlHelp.ExecuteNonQuery("prc_InsertNews", CommandType.StoredProcedure, param);
            return result;
        }
        public int DeleteNews(int id)
        {
            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Value = id;
            int result = SqlHelp.ExecuteNonQuery("prc_DeleteNews", CommandType.StoredProcedure, param);
            return result;
        }
        public int UpdateNews(NewsModel news)
        {
            SqlParameter[] param = { new SqlParameter("@Title", SqlDbType.VarChar, 50), new SqlParameter("@NoticeContent", SqlDbType.Text),
                                     new SqlParameter("@NoticePerSon",SqlDbType.VarChar,20),new SqlParameter("@ID",SqlDbType.Int)
                                   };
            param[0].Value = news.Title;
            param[1].Value = news.NoticeContent;
            param[2].Value = news.NoticePerson;
            param[3].Value = news.ID;
            int result = SqlHelp.ExecuteNonQuery("prc_UpdateNews", CommandType.StoredProcedure, param);
            return result;
        }
    }
}
