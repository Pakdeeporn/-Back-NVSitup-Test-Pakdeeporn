using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApis.Contexts;

namespace WebApis.Models
{
    public class User
    {
        [Key]
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public int Role_Id { get; set; }

    }
}
