using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class CustomerDTO
    {

    }
    public class CustomerRegisterDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
    public class CustomerLoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CustomerLoginByGoogleDTO
    {
        [Required]
        public string Token { get; set; }
    }
    public class CustomerResponseDTO
    {
        public string JwtToken { get; set; }
    }
}
