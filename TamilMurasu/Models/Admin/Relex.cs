using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class Relex
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string ddlStatus { get; set; }

    }
    public class Relexgrid
    {
        public long id { get; set; }
        public string relex { get; set; }
        public string delrow { get; set; }
        public string editrow { get; set; }
    }
}
