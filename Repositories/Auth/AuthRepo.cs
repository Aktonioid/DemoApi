using models;
using DemoApi.Dto;
using DemoApi.Repositories.DB;
using Microsoft.IdentityModel.Tokens;
using dotenv.net;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using DemoApi.Repositories.Refresh;

namespace DemoApi.Repositories.Logi
{
    public class AuthRepo: IAuthRepo
    {
        private readonly Mongo client = new Mongo("Users");
        private readonly Mongo RefClient = new Mongo("Refresh");

        private static IDictionary<string, string> EnvDict()
        {
            return DotEnv.Read(options: new DotEnvOptions(envFilePaths: new[] { "settings.env" }));
        }

        public async Task<UserDto> Authenticate(UserLogin userLogin)
        {
            var user = await client.FindByLoginAsync<User>(userLogin.Login);

            var pasw = userLogin.Password;
            using (SHA512Managed sha = new())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pasw));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                pasw = sb.ToString();
            }

            
            if (user == null)
            {
            return null;
            }

            if ((userLogin.Login != user.Login) || (user.Password != user.Password)) return null;
            return user.AsDto();
        }

        public async Task<string> Generate(UserDto user)
        {
            var role = user.Kind.ToString();
            var guid = user.Id.ToString();
            
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvDict()["Key"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("First_Name", user.first_name),
                new Claim("Last_Name", user.last_Name),
                new Claim("Login", user.Login),
                new Claim("UserId", guid),
                new Claim(ClaimTypes.Role, role)
            };
            
            var token = new JwtSecurityToken(
                issuer: EnvDict()["Issuer"], 
                audience: EnvDict()["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
        public async Task  LogOut(Guid Id, string refresh) 
        {
            var Find = await RefClient.FindByIdAsync<RefreshModel>(Id); ;
            var list = Find.TokenList;
            
            list.Remove(refresh);

            Find = Find with { TokenList = list };
            
            await RefClient.UpdateAsync<RefreshModel>(Find, Id);

        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
