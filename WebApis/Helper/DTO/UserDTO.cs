namespace WebApis.Helper.DTO
{
    public class UserDTO
    {
        public IEnumerable<UserColumnDTO> column { get; set; }
        public IEnumerable<UserDataDTO> data { get; set; }
    }
    public class UserColumnDTO
    {
        public string key { get; set; }
        public string name { get; set; }
    }
    public class UserDataDTO
    {
        public string user_id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime date_of_birth { get; set; }
    }
}
