using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DemoApi;
using dotenv.net;
using System.Text;
using System.Security.Claims;
using DemoApi.Dto;
using DemoApi.Repositories.DB;
using models;
using System.Net;

namespace DemoApi.Repositories.Refresh
{
    public class Refresh : IRefresh
    {
        private readonly string cookieName = "Refresh";
        private readonly Mongo userClient = new("Users");
        private readonly Mongo refreshClient = new("Refresh");
        private static IDictionary<string, string> EnvDict()
        {
            return DotEnv.Read(options: new DotEnvOptions(envFilePaths: new[] { "settings.env" }));

        }

        public async Task<string> Generate(UserDto user)
        {
            //Console.WriteLine("Generate Refresh");


            var issuer = EnvDict()["Issuer"];
            var audience = EnvDict()["Audience"];
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvDict()["Key"]));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
            var id = user.Id;
            
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials);

            var writeToken = new JwtSecurityTokenHandler().WriteToken(token);
            List<string> tokenList = new List<string>();

            try
            {
                var smth = await refreshClient.FindByIdAsync<RefreshModel>(id);
                tokenList = smth.TokenList;
            }
            catch 
            {

            }
            tokenList.Add(writeToken);
            RefreshModel refresh = new RefreshModel
            {
                Id = id,
                TokenList = tokenList
            };
            try
            {
                await refreshClient.CreateAsync<RefreshModel>(refresh);
            }
            catch
            {
                await refreshClient.UpdateAsync<RefreshModel>(refresh, refresh.Id);
            }

            //if (context.Request.Cookies["Refresh"] != null)
            //{
            //    context.Response.Cookies.Delete("Refresh");
            //}

            //context.Response.Cookies.Append(cookieName, writeToken, new CookieOptions { HttpOnly = true });
            //Console.WriteLine(context.Request.Cookies[cookieName]); // Тут куку видит и читает, а в методе GetRefreshById говорит что кука пуста
            return writeToken;
        }

        public async Task<UserDto> UserByToken(string Token)
        {
            //Console.WriteLine("User By token");

            //var tokenElements = Token.Split(".");
            //Console.WriteLine(tokenElements[1]);
            //var payloadEnc = Convert.FromBase64String(tokenElements[1]); //encoded payload
            //string payload = Encoding.UTF8.GetString(payloadEnc);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(Token);
            var token = jsonToken as JwtSecurityToken;
            var jwtClaims = token.Claims;
            var UserId = jwtClaims.First(claim => claim.Type == "UserId");
            var StrId = ((UserId.ToString()).Split(" "))[1];
            var Id = new Guid(StrId);


            return (await userClient.FindByIdAsync<User>(Id)).AsDto();
        }
        public async Task<Guid> IdByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var Token = jsonToken as JwtSecurityToken;
            var jwtClaims = Token.Claims;
            var UserId = jwtClaims.First(claim => claim.Type == "UserId");
            var StrId =((UserId.ToString()).Split(" "))[1];

            return new Guid(StrId);
        }

        public async Task<bool> Validate(string token)
        {
            //Console.WriteLine("Valid JWT");

            var issuer = EnvDict()["Issuer"];
            var audience = EnvDict()["Audience"];
            var Security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvDict()["Key"]));


            var tokenhandler = new JwtSecurityTokenHandler();
            try
            {
                tokenhandler.ValidateToken(token, new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = Security,
                    ValidateLifetime = false,
                }, out SecurityToken ValidatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<string> GetRefreshById(Guid Id, string cookie)
        {
            var refresh = await refreshClient.FindByIdAsync<RefreshModel>(Id);
            var list = refresh.TokenList;
            
            for (int i = 0; i < list.Count; i++)
            { 
                if (list[i] == cookie)
                {
                     list.RemoveAt(i);
                     break;
                } 
            }

            refresh = refresh with { TokenList = list };
            
            await refreshClient.UpdateAsync<RefreshModel>(refresh,Id);

            return cookie;
        }

        public async Task<bool> ValidateRefresh(string Token)
        {

            var issuer = EnvDict()["Issuer"];
            var audience = EnvDict()["Audience"];
            var key = EnvDict()["Key"];
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenhandler = new JwtSecurityTokenHandler();
            
            try
            {
                tokenhandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = security,
                }, out SecurityToken ValidatedToken);
            }
            
            catch 
            {
                return false;
            }
            
            return true;
        }
    }
}
