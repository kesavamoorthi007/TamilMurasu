using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface INewImageService
    {
        string NewImageCRUD(List<IFormFile> file, List<IFormFile> file1, NewImage Cy);
        DataTable GetCategory();
        string StatusDeleteMR(string tag, int id);
        string RemoveChange(string tag, int id);
        DataTable GetAllNewImage(string strStatus);
        DataTable GetEditNewImage(string id);
    }
}
