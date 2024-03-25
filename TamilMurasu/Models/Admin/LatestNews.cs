using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class LatestNews
    {
        public string ID { get; set; }
        public string NewsHead { get; set; }
        public string NewsDetail { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
    }
    public class LatestNewsgrid
    {
        public long id { get; set; }
        public string cname { get; set; }
        public string newshead { get; set; }
        public string des { get; set; }
        public string keyword { get; set; }
        public string editrow { get; set; }
    }
}
