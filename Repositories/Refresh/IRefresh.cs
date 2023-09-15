using DemoApi.Dto;

namespace DemoApi.Repositories.Refresh
{
    public interface IRefresh
    {
        public Task<string> Generate(UserDto user);
        public Task<bool> Validate(string Token);
        public Task<UserDto> UserByToken(string AccessToken);

        public Task<Guid> IdByToken(string token);
        public Task<bool> ValidateRefresh(string Token);
        public Task<string> GetRefreshById(Guid Id, string cookie); //get refresh token from db by Id
    }
}
