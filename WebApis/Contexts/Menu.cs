using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApis.Contexts
{
    public class Menu
    {
        [Key]
        public int Menu_Id { get; set; }
        public int? Sub_Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string? Menu_Shot_Name { get; set; }

    }
}
