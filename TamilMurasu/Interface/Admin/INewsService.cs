using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface INewsService
    {
        DataTable GetAllNews();
        DataTable GetCategory();
        string StatusDeleteMR(string tag, int id);

        string NewsCRUD(List<IFormFile> file, News Cy);
      
    }
}
