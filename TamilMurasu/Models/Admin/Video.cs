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
        public string ddlStatus { get; set; }

    }
    public class Videogrid
    {
        public long id { get; set; }
        public string title { get; set; }
        public string foot { get; set; }
        public string delrow { get; set; }
        public string editrow { get; set; }
    }
}
