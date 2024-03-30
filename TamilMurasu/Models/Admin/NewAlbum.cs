using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class NewAlbum
    {
        public string ID { get; set; }
        public string Album { get; set; }
        public string EnglishAlbum { get; set; }
        public string PictureThumbnail { get; set; }
        public string ddlStatus { get; set; }
       
    }
    public class NewAlbumgrid
    {
        public long id { get; set; }
        public string footnote { get; set; }
        public string head { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
