using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallHouseManagerDAL;
using SmallHouseManagerModel;

namespace SmallHouseManagerBLL
{
    public class NewsBLL
    {
        NewsDAL dal = new NewsDAL();
        public List<NewsModel> ShowNews()
        {
            return dal.ShowNews();
        }
        public NewsModel ShowNewsByID(int id)
        {
            return dal.ShowNewsByID(id);
        }
        public List<NewsModel> ShowAllNews()
        {
            return dal.ShowAllNews();
        }
        public List<NewsModel> GetNewsByCondition(string condition)
        {
            return dal.GetNewsByCondition(condition);
        }
        public bool InsertNotice(NewsModel news)
        {
            int result = dal.InsertNotice(news);
            return result == 0 ? false : true;
        }
        public int DeleteNews(int id)
        {
            return dal.DeleteNews(id);
        }
        public bool UpdateNews(NewsModel news)
        {
            int result = dal.UpdateNews(news);
            return result == 0 ? false : true;
        }
    }
}
