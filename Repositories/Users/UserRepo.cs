using DemoApi.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using models;
using DemoApi.Dto;
using System.Security.Claims;
using DemoApi.Repositories.DB;
using System.Security.Cryptography;
using System.Text;

namespace DemoApi.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly Mongo DB = new("Users");

        public async Task<UserDto> CreateUserAsync(UserDto userdto)
        {
            string password = userdto.Password;

            using (SHA512Managed sha = new())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                password = sb.ToString();
            }

            // using (SHA256 hashE = new SHA256Managed())
            // {
            // var hash =hashE.ComputeHash(Encoding.UTF8.GetBytes(password));
            // password = hash.ToString();
            // }

            User user = new() 
            {
                Id = new Guid(),
                first_name = userdto.first_name,
                Password = password,
                Kind = 0,
                last_Name = userdto.last_Name,
                Login = userdto.Login
            };

            await DB.CreateAsync<User>(user);
            return user.AsDto();
        }

        public async Task DeleteUserAsync(Guid user_id)
        {
            await DB.DeleteAsync<User>(user_id);
        }

        public async Task EditUserAsync(Guid user_id, UserDto userdto)
        {
            User user = new()
            {
                Id = user_id,
                Kind = userdto.Kind,
                Login = userdto.Login,
                first_name=userdto.first_name,
                last_Name=userdto.last_Name,
                Password = userdto.Password
            };

            await DB.UpdateAsync<User>(user,user_id);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid user_id)
        {
            var obj = await DB.FindByIdAsync<User>(user_id);

            return obj.AsDto();

        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        { 
            var Users = await DB.FindAsync<User>();
            
            return Users.Select(user => user.AsDto());
        }
    }
}
