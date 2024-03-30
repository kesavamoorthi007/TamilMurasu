using Microsoft.AspNetCore.Mvc.Rendering;

namespace TamilMurasu.Models
{
    public class NewImage
    {
        public NewImage()
        {
            this.Categorylst = new List<SelectListItem>();

        }
        public List<SelectListItem> Categorylst;
        public string Category { get; set; }
        public string ID { get; set; }
        public string FootNote  { get; set; }
        public string PictureThumbnail { get; set; }
        public string PictureLarge { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
        public string ddlStatus { get; set; }
    }
    public class NewImagegrid
    {
        public long id { get; set; }
        public string footnote { get; set; }
        public string Thumb { get; set; }
        public string large { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
