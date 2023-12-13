using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
        {
            var loginRespose = await _userRepo.LoginUser(model);
            if(loginRespose.User == null || string.IsNullOrEmpty(loginRespose.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage= new List<string> {"Username or Password incorrect!" };
                return BadRequest(_response);
            }
            _response.Result = loginRespose;
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {


            bool UserIsUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!UserIsUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { "Username already exist!" };
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(model);
            if(user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { "Error while regiser!" };
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.Result = user;

            return Ok(_response);


        }
    }
}
