using Microsoft.AspNetCore.Mvc.Rendering;

namespace TamilMurasu.Models
{
    public class NewImage
    {
        public string ID { get; set; }
        public string FootNote  { get; set; }
        public string PictureThumbnail { get; set; }
        public string PictureLarge { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
    }
}
