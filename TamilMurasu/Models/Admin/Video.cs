using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class Video
    {
        public string ID { get; set; }
        public string VideoTittle { get; set; }
        public string VideoImage { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
    }
}
