using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface ILatestNewsService
    {
        DataTable GetAllLatestNews(string strStatus);
        DataTable GetEditLatestNews(string id);
        string LatestNewsCRUD(LatestNews Cy);
        string StatusDeleteMR(string tag, int id);
    }
}
