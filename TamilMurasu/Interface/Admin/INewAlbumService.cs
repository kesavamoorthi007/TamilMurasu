using TamilMurasu.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace TamilMurasu.Interface.Admin
{
    public interface INewAlbumService
    {
        DataTable GetAllNewAlbum(string strStatus);
        DataTable GetEditNewAlbum(string id);
        string NewAlbumCRUD(List<IFormFile> file, NewAlbum Cy);
        string StatusDeleteMR(string tag, int id);
        string RemoveChange(string tag, int id);

    }
}
