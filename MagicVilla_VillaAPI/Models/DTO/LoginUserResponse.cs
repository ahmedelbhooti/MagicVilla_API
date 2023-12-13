namespace MagicVilla_VillaAPI.Models.DTO
{
    public class LoginUserResponse
    {
        public UserDTO User { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
