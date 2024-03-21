using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface ICategoryService
    {
        string CategoryCRUD(Category cy);
        DataTable GetAllCategory();
    }
}
