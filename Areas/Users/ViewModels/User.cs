using System.ComponentModel.DataAnnotations;

namespace WebTabanliProje.Areas.Users.ViewModels
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)] public string UserPassword { get; set; }
        [Required, DataType(DataType.Password)] public string NewPassword { get; set; }
    }
    public class ChangeEmail
    {
        public string Email { get; set; }
        [Required, DataType(DataType.EmailAddress)] public string NewEmail { get; set; }
        [Required, DataType(DataType.Password)] public string UserPassword { get; set; }

    }
}