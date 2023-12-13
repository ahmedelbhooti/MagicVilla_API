using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string Username);

        Task<LoginUserResponse> LoginUser(LoginUserDTO loginUserDTO);

        Task<UserDTO> Register(RegistrationDTO registrationDTO);
    }
}
