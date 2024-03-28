using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface INewImageService
    {
        string NewImageCRUD(NewImage cy);
        DataTable GetCategory();
    }
}
