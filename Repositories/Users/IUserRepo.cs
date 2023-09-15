using models;
using DemoApi.Dto;
namespace DemoApi.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        public Task<UserDto> CreateUserAsync(UserDto user);
        public Task DeleteUserAsync(Guid user_id);
        public Task EditUserAsync(Guid user_id, UserDto user);
        public Task<UserDto> GetUserByIdAsync(Guid user_id);

    }
}
