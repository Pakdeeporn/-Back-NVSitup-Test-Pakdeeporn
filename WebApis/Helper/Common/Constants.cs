namespace WebApis.Models
{
    public class Constants
    {
        public static ConstantModel Constant { get; set; }
    }
    public class ConstantModel
    {
        public string User_Auth { get; set; }
        public string Pass_Auth { get; set; }
        public string Secret { get; set; }
    }
    public static class SysEnum
    {
        public const string Claim_User_Id = "User_Id";
        public const string Claim_Name = "Name";
        public const string Claim_Gender = "Gender";
        public const string Claim_Email = "Email";
        public const string Claim_Date_Of_Birth = "Date_Of_Birth";
        public const string Claim_Role_Id = "Role_Id";
        public const string Claim_Password = "Password";

    }
}
