using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface IVideoService
    {
        DataTable GetAllVideo(string strStatus);
        string VideoCRUD(List<IFormFile> file,Video Cy);
        string StatusDeleteMR(string tag, int id);
        string RemoveChange(string tag, int id);
        DataTable GetEditVideo(string id);
    }
}
