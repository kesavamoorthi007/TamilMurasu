using Microsoft.AspNetCore.Mvc.Rendering;


namespace TamilMurasu.Models
{
    public class Anmeegam
    {
        public Anmeegam()
        {
            this.Categorylst = new List<SelectListItem>();

        }
        public List<SelectListItem> Categorylst;

        public string ID { get; set; }
        public string Aanmegam { get; set; }
        public string Category { get; set; }
        public string NewsDetail { get; set; }
        public string PublishUp { get; set; }
        public string PublishDown { get; set; }

    }
    public class Anmeegamgrid
    {
        public long id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string editrow { get; set; }
    }
}
