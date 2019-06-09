using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTabanliProje.Models;

namespace WebTabanliProje.ViewModels
{
    public class UserLogin
    {
        [MinLength(5), Required(ErrorMessage = "Username alanı için bilgi giriniz")]
        public String UserName { get; set; }
        [MinLength(5), Required, DataType(DataType.Password)]
        public String UserPassword { get; set; }
    }
    public class UserRegister
    {
        public IList<Role> Roles { get; set; }
        [Required, MaxLength(128)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}