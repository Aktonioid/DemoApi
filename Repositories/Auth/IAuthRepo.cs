using DemoApi.Dto;

namespace DemoApi.Repositories.Logi
{
    public interface IAuthRepo
    {
        public Task<UserDto> Authenticate(UserLogin userlogin);

        public Task<string> Generate(UserDto user);

        public Task LogOut( Guid Id, string refresh);
    }
}
