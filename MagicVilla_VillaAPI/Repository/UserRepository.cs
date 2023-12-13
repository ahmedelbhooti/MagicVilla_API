using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretkey;
        public UserRepository(ApplicationDbContext db, IMapper mapper, 
            IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string Username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == Username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginUserResponse> LoginUser(LoginUserDTO loginUserDTO)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginUserDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
            if (user == null || isValid == false)
            {
                return new LoginUserResponse
                {
                    Token = "",
                    User = null,
                };
            }

            //Genrate Token for login user
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginUserResponse loginResponseDTO = new LoginUserResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user),
                Role = roles.FirstOrDefault(),

            };
            return loginResponseDTO;
        }

        public async Task<UserDTO> Register(RegistrationDTO registrationDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationDTO.UserName,
                Email = registrationDTO.UserName,
                NormalizedEmail = registrationDTO.UserName.ToUpper(),
                Name = registrationDTO.Name,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationDTO.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("cutomer"));

                    }

                    await _userManager.AddToRoleAsync(user, "customer");
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationDTO.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);
                }
            }
            catch(Exception ex)
            {

            }

            
            return new UserDTO();
        }
    }
}
