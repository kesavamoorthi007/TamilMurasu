using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class Adangapa
    {
        public string ID { get; set; }
        public string Original { get; set; }
        public string Comedy { get; set; }
        public string Picture { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }
        public string ddlStatus { get; set; }
    }
    public class Adangapagrid
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
