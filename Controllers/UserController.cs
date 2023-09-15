using DemoApi.Dto;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using models;
using DemoApi;
using Microsoft.AspNetCore.Authorization;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("students")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo repository;

        public UserController(IUserRepo repository)
        {
            this.repository = repository;
        }


    }
}
