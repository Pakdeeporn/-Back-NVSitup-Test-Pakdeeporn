using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApis.Contexts
{
    public class User_Role_Menu
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User_Role")]
        public int Role_Id { get; set; }
        [ForeignKey("User_Menu")]
        public int Menu_Id { get; set; }

        public virtual Role User_Role { get; set; }
        public virtual Menu User_Menu { get; set; }

    }
}
