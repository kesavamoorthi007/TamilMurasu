using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class News
    {
        public News()
        {
            this.Categorylst = new List<SelectListItem>();

        }
        public List<SelectListItem> Categorylst;

        public string ID { get; set; }
        public string Category { get; set; }
        public string NewsHead { get; set; }
        public bool Editor { get; set; }
        public bool Highlights { get; set; }
        public bool Banner { get; set; }
        public string NewsDetail { get; set; }
        public string PictureThumbnail { get; set; }
        public string PictureLarge { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
        public string KeyWords { get; set; }
    }
    public class Newsgrid
    {
        public long id { get; set; }
        public string cname { get; set; }
        public string newshead { get; set; }
        public string des { get; set; }
        public string keyword { get; set; }
        public string editrow { get; set; }

    }
}
