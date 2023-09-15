using DemoApi.Dto;
using DemoApi.Repositories;
using DemoApi.Repositories.Logi;
using DemoApi.Repositories.Refresh;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Web;
namespace DemoApi.Controllers
{
    [ApiController]
    [Route("signin")]
    public class SingInController: ControllerBase
    {
        private readonly IAuthRepo AuthRepo;
       
        private readonly IRefresh RefreshRepo;


        private readonly IUserRepo userRepo;
        public SingInController(IAuthRepo AuthRepo, IRefresh RefreshRepo, IUserRepo userRepo)
        {
            this.AuthRepo = AuthRepo;
            
            this.RefreshRepo = RefreshRepo;

            this.userRepo = userRepo;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] UserLogin login) 
        {
            var user = await AuthRepo.Authenticate(login);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = await AuthRepo.Generate(user);
            Response.Cookies.Append("Token", token);
           
            var refresh = await RefreshRepo.Generate(user);
            Response.Cookies.Append("Refresh", refresh, new CookieOptions { HttpOnly = true});
            
            return Ok(token);
            
        }
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            HttpContext context = HttpContext;
            
            var token = Request.Cookies["Refresh"];
            
           // Console.WriteLine((await RefreshRepo.UserByToken(token)).Id);

            var Id = await RefreshRepo.IdByToken(token);

            await AuthRepo.LogOut(Id, token);
            
            Response.Cookies.Delete("Refresh");
            Response.Cookies.Delete("Token");
            
            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var context = HttpContext;
            
            var headers = Request.Headers;
            string auth = headers["Authorization"];
            var berear = auth.Split(" ");
            string accessToken = berear[1];
            var refreshToken = Request.Cookies["Refresh"];
            
            bool validAccess = await RefreshRepo.Validate(accessToken);

            if (validAccess == false) return Forbid();

            var user = await RefreshRepo.UserByToken(accessToken);
            var refresh = await RefreshRepo.GetRefreshById(user.Id, refreshToken); 

            bool validateRefresh = await RefreshRepo.ValidateRefresh(refresh);

            if (validateRefresh == true && validAccess == true) 
            {
                var token = await AuthRepo.Generate(user);
                
                Response.Cookies.Delete("Token");
                Response.Cookies.Append("Token", token);
                
                var refreshT = await RefreshRepo.Generate(user);
                
                Response.Cookies.Delete("Refresh");
                Response.Cookies.Append("Refresh",refreshT, new CookieOptions { HttpOnly = true});
                
                return Ok(token);
            }

            return Forbid();
        }
        
    }
}

