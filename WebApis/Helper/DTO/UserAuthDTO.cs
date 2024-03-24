namespace WebApis.Helper.DTO
{
    [Serializable]
    public class UserAuthDTO : UserRoleMenuDTO
    { 
        public string Token { get; set; }
    }
}
