using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Shared
{
    public class LoginInputModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginViaCodeInputModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Code { get; set; }

        public bool RememberMe { get; set; }
    }
}
