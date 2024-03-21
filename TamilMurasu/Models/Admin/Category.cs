using Microsoft.AspNetCore.Mvc.Rendering;

namespace TamilMurasu.Models
{
    public class Category
    {
        public string ID { get; set; }
        public string C_Name { get; set; }
        public string C_NameEN { get; set; }
        public string Title_Eng { get; set; }


    }
    public class Categorygrid
    {
        public long id { get; set; }
        public string cname { get; set; }
        public string cnameeng { get; set; }
        public string tittle { get; set; }
        public string editrow { get; set; }

    }
}
