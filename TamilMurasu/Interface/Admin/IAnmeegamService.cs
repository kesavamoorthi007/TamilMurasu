using System.Data;
using TamilMurasu.Models;

namespace TamilMurasu.Interface.Admin
{
    public interface IAnmeegamService
    {
        string AnmeegamCRUD(Anmeegam Cy);
        DataTable GetAllAnmeegam();
        DataTable GetEditAnmeegam(string id);
    }
}
