using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;


namespace TamilMurasu.Interface.Admin
{
    public interface IAdangapaService
    {
        string AdangapaCRUD(List<IFormFile> file, Adangapa Cy);
        DataTable GetAllAdangapa(string strStatus);
        DataTable GetEditAdangapa(string id);
        string StatusDeleteMR(string tag, int id);
    }
}
