using Microsoft.AspNetCore.Mvc.Rendering;

namespace TamilMurasu.Models
{
    public class Cinema
    {

        public string ID { get; set; }
        public string Album { get; set; }
        public string EnglishAlbum { get; set; }
        public string Tag { get; set; }
        public string ddlStatus { get; set; }
        

    }
    public class MyCinemagrid
    {
        public long id { get; set; }
        public string footnote { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
