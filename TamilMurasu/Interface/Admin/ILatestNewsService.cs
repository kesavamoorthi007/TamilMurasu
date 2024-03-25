using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface ILatestNewsService
    {
        DataTable GetAllLatestNews();
        string LatestNewsCRUD(LatestNews cy);
    }
}
