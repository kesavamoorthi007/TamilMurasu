using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface IRelexService
    {
        DataTable GetAllRelex(string strStatus);
        DataTable GetEditRelex(string id);
        string RelexCRUD(Relex Cy);
         string StatusDeleteMR(string tag, int id);
        string RemoveChange(string tag, int id);
    }
}
