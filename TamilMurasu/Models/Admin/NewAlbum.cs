using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class NewAlbum
    {
        public string ID { get; set; }
        public string Album { get; set; }
        public string EnglishAlbum { get; set; }
        public string PictureThumbnail { get; set; }
       
    }
}
