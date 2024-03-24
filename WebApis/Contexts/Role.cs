using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApis.Contexts
{
    public class Role
    {
        [Key]
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
    }
}
