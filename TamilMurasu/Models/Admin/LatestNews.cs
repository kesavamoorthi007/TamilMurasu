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
        public string ddlStatus { get; set; }
    }
    public class LatestNewsgrid
    {
        public long id { get; set; }
        public string footnote { get; set; }
        public string publishup { get; set; }
        public string publishdown { get; set; }
        public string head { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
