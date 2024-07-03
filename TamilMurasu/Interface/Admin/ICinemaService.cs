
using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface ICinemaService
    {
        string CinemaCRUD(List<IFormFile> file, Cinema Cy);
        DataTable GetAllCinema(string strStatus);
        DataTable GetEditCinema(string id);
        string StatusDeleteMR(string tag, int id);
        string RemoveChange(string tag, int id);

    }
}
