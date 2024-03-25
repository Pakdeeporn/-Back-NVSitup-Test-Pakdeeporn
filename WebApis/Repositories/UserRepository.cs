using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApis.Contexts;
using WebApis.Helper.DTO;
using WebApis.Helper.Models;
using WebApis.Interface;
using WebApis.Models;

namespace WebApis.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context = new();

        public async Task<ResponseMessage> CreateMockData()
        {
            ResponseMessage result = new() { Is_Success = true };
            #region User
            var passwordByte = Encoding.UTF8.GetBytes("123456789");
            var endcodePassword = Convert.ToBase64String(passwordByte);

            List<User> lstModel = new() {
                new User {
                    User_Id = "0001",
                    Name = "Sara",
                    Gender = "Female",
                    Email = "Sara@gmail.com",
                    Password = endcodePassword,
                    Role_Id = 1,
                    Date_Of_Birth = DateTime.ParseExact("07/05/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                },
                 new User
                 {
                     User_Id = "0002",
                     Name = "Eric",
                     Gender = "Male",
                     Email = "Eric@gmail.com",
                     Password = endcodePassword,
                     Role_Id = 2,
                     Date_Of_Birth = DateTime.ParseExact("08/05/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                 },
                  new User
                  {
                      User_Id = "0003",
                      Name = "Robert",
                      Gender = "Male",
                      Email = "Robert@gmail.com",
                      Password = endcodePassword,
                      Role_Id = 3,
                      Date_Of_Birth = DateTime.ParseExact("09/05/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                  }
                };
            #endregion

            #region User Role
            List<Role> lstRoleModel = new() {
                new Role {
                    Role_Id = 1,
                    Role_Name = "Admin"
                },
                 new Role
                 {
                     Role_Id = 2,
                     Role_Name = "Approver"
                 },
                  new Role
                  {
                     Role_Id = 3,
                     Role_Name = "User"
                  }
                };
            #endregion

            #region User Menu
            List<Menu> lstMenuModel = new() {
                new Menu {
                    Menu_Id = 1,
                    Menu_Name = "Dashboad"
                },
                 new Menu
                 {
                     Menu_Id = 2,
                     Menu_Name = "Work Flow"
                 },
                  new Menu
                  {
                     Menu_Id = 3,
                     Menu_Name = "Setting"
                  },
                  new Menu
                  {
                     Menu_Id = 4,
                     Sub_Menu_Id = 2,
                     Menu_Name = "Create Case"
                  },
                  new Menu
                  {
                     Menu_Id = 5,
                     Sub_Menu_Id = 2,
                     Menu_Name = "To Do List"
                  },
                  new Menu
                  {
                     Menu_Id = 6,
                     Sub_Menu_Id = 2,
                     Menu_Name = "Waiting To Approve"
                  },
                  new Menu
                  {
                     Menu_Id = 7,
                     Sub_Menu_Id = 3,
                     Menu_Name = "Update Approval"
                  }
                };
            #endregion

            #region User Role Menu
            List<User_Role_Menu> lstRoleMenuModel = new() {
                new User_Role_Menu {
                    Id = 1,
                    Role_Id = 1,
                    Menu_Id = 1,
                },
                 new User_Role_Menu
                 {
                     Id = 2,
                     Role_Id = 1,
                     Menu_Id = 2,
                 },
                  new User_Role_Menu
                  {
                      Id = 3,
                      Role_Id = 1,
                      Menu_Id = 3,
                  },
                  new User_Role_Menu
                  {Id = 4,
                      Role_Id = 1,
                      Menu_Id = 4,
                  },
                  new User_Role_Menu
                  {Id = 5,
                      Role_Id = 1,
                      Menu_Id = 5,
                  },
                  new User_Role_Menu
                  {Id = 6,
                      Role_Id = 1,
                      Menu_Id = 6,
                  },
                  new User_Role_Menu
                  {Id = 7,
                      Role_Id = 1,
                      Menu_Id = 7,
                  },
                  new User_Role_Menu
                  {Id = 8,
                      Role_Id = 2,
                      Menu_Id = 1,
                  },
                  new User_Role_Menu
                  {Id = 9,
                      Role_Id = 2,
                      Menu_Id = 2,
                  },
                  new User_Role_Menu
                  {Id = 10,
                      Role_Id = 2,
                      Menu_Id = 6,
                  },
                  new User_Role_Menu
                  {Id = 11,
                      Role_Id = 3,
                      Menu_Id = 1,
                  },
                  new User_Role_Menu
                  {Id = 12,
                      Role_Id = 3,
                      Menu_Id = 4,
                  },
                  new User_Role_Menu
                  {Id = 13,
                      Role_Id = 3,
                      Menu_Id = 5,
                  }
                };
            #endregion

            _context.Users.RemoveRange(_context.Users);
            _context.Roles.RemoveRange(_context.Roles);
            _context.Menus.RemoveRange(_context.Menus);
            _context.User_Role_Menu.RemoveRange(_context.User_Role_Menu);

            await _context.Users.AddRangeAsync(lstModel);
            await _context.Menus.AddRangeAsync(lstMenuModel);
            await _context.Roles.AddRangeAsync(lstRoleModel);
            await _context.User_Role_Menu.AddRangeAsync(lstRoleMenuModel);
            await _context.SaveChangesAsync();
            return result;
        }

        public User GetUserById(string user_id)
        {
            return _context.Users.First(i => i.User_Id.Equals(user_id));
        }

        public IEnumerable<UserRoleMenuDTO> GetListUser()
        {
            var lstMainMenu = _context.Menus.Where(i => i.Sub_Menu_Id.Equals(null)).Select(i => i.Menu_Id);
            var data = _context.Users.Select(u => new UserRoleMenuDTO
            {
                User_Id = u.User_Id,
                Name = u.Name,
                Email = u.Email,
                Gender = u.Gender,
                Date_Of_Birth = u.Date_Of_Birth,
                Role_Id = u.Role_Id,
                Password = u.Password,
                List_Menu = (
                         _context.User_Role_Menu.Where(i => i.Role_Id.Equals(u.Role_Id) && lstMainMenu.Contains(i.Menu_Id)).Select(ur => new MainMenuDTO
                         {
                             Menu_Id = ur.Menu_Id,
                             Menu_Name = ur.User_Menu.Menu_Name,
                             Menu_Shot_Name = ur.User_Menu.Menu_Shot_Name,
                             List_Sub_Menu = _context.Menus.Where(i => i.Sub_Menu_Id.Equals(ur.Menu_Id)).Select(i => new MenuDTO
                             {
                                 Menu_Id = i.Menu_Id,
                                 Menu_Name = i.Menu_Name,
                                 Menu_Shot_Name = i.Menu_Shot_Name
                             }).ToList()
                         }).ToList()
                )
            }).AsEnumerable();
            return data;
        }

        public async Task<ResponseMessage> CreateUser(User model)
        {
            ResponseMessage result = new() { Is_Success = true };
            try
            {
                var passwordByte = Encoding.UTF8.GetBytes(model.Password);
                model.Password = Convert.ToBase64String(passwordByte);
                model.User_Id = (int.Parse(_context.Users.Max(i => i.User_Id) ?? "0") + 1).ToString().PadLeft(4, '0');

                _context.Users.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Is_Success = false;
                result.Message = ex.Message;

            }
            return result;
        }

        public async Task<ResponseMessage> UpdateUser(User model)
        {
            ResponseMessage result = new() { Is_Success = true };
            try
            {
                var passwordByte = Encoding.UTF8.GetBytes(model.Password);
                model.Password = Convert.ToBase64String(passwordByte);

                var data = _context.Users.First(i => i.User_Id.Equals(model.User_Id));
                data.Name = model.Name;
                data.Gender = model.Gender;
                data.Email = model.Email;
                data.Date_Of_Birth = model.Date_Of_Birth;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Is_Success = false;
                result.Message = ex.Message;

            }
            return result;
        }

        public async Task<ResponseMessage> DeleteUser(string user_id)
        {
            ResponseMessage result = new() { Is_Success = true };
            try
            {
                var data = _context.Users.First(i => i.User_Id.Equals(user_id));
                _context.Users.Remove(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Is_Success = false;
                result.Message = ex.Message;

            }
            return result;
        }

        public UserAuthDTO? Authenticate(string username, string password)
        {
            UserAuthDTO? user = null;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            //Init data
            CreateMockData();

            var passwordByte = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordByte);

            var data = _context.Users.FirstOrDefault(i => i.Email.Equals(username) && i.Password.Equals(encodePassword));
            if (data != null)
            {
                user = new UserAuthDTO
                {
                    User_Id = data.User_Id,
                    Name = data.Name,
                    Email = data.Email,
                    Date_Of_Birth = data.Date_Of_Birth,
                    Gender = data.Gender,
                    Role_Id = data.Role_Id,
                    Password = data.Password
                };

                JwtSecurityTokenHandler tokenHandler = new();
                byte[] key = Encoding.ASCII.GetBytes(Constants.Constant.Secret);
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.User_Id),
                        new Claim(SysEnum.Claim_User_Id, user.User_Id),
                        new Claim(SysEnum.Claim_Name, user.Name),
                        new Claim(SysEnum.Claim_Gender, user.Gender),
                        new Claim(SysEnum.Claim_Email, user.Email),
                        new Claim(SysEnum.Claim_Date_Of_Birth, user.Date_Of_Birth.ToString()),
                        new Claim(SysEnum.Claim_Role_Id, user.Role_Id.ToString()),
                        new Claim(SysEnum.Claim_Password, user.Password)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }

            return user;

        }

        public UserDTO GetListUserTable() 
        {

            var userData = _context.Users.Select(i => new UserDataDTO { 
                user_id = i.User_Id,
                name = i.Name,
                gender = i.Gender,
                email = i.Email,
                password = i.Password,
                date_of_birth = i.Date_Of_Birth.ToString("yyyy-MM-dd")
            });

            UserDTO user = new() {
                column = new List<UserColumnDTO>() { 
                    new UserColumnDTO { key = "user_id", name = "User Id" },
                    new UserColumnDTO { key = "name", name = "Name" },
                    new UserColumnDTO { key = "gender", name = "Gender" },
                    new UserColumnDTO { key = "email", name = "E-mail" },
                    new UserColumnDTO { key = "password", name = "Password" },
                    new UserColumnDTO { key = "date_of_birth", name = "Birth Date" },
                },
                data = userData
            };

            return user;
        }

    }
}
