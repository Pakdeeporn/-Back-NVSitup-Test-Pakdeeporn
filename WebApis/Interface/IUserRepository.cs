using WebApis.Helper.DTO;
using WebApis.Helper.Models;
using WebApis.Models;

namespace WebApis.Interface
{
    public interface IUserRepository
    {
        Task<ResponseMessage> CreateMockData();
        IEnumerable<UserRoleMenuDTO> GetListUser();
        User GetUserById(string user_id);
        Task<ResponseMessage> CreateUser(User model);
        Task<ResponseMessage> UpdateUser(User model);
        Task<ResponseMessage> DeleteUser(string user_id);

        UserDTO GetListUserTable();

        UserAuthDTO? Authenticate(string username, string password);
    }
}
