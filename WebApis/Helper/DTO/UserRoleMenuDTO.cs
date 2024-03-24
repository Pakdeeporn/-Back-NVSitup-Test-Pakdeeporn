namespace WebApis.Helper.DTO
{
    public class UserRoleMenuDTO
    {
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public int Role_Id { get; set; }
        public string? Password { get; set; }
        public List<MainMenuDTO> List_Menu { get; set; }
    }
    public class MainMenuDTO : MenuDTO
    {
        public List<MenuDTO> List_Sub_Menu { get; set; }
    }

    public class MenuDTO
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string? Menu_Shot_Name { get; set; }
    }
}
