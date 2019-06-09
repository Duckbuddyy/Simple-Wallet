using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTabanliProje.Models;

namespace WebTabanliProje.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }
    public class UsersCreate
    {
        public IList<RoleCheckBox> Roles { get; set; }
        [Required,MaxLength(128)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
    public class UsersEdit
    {
        public IList<RoleCheckBox> Roles { get; set; }
        [Required, MaxLength(128)]
        public string UserName { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
    public class UsersResetPassword
    {
        [Required, MaxLength(128)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
    public class RoleCheckBox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string RoleName { get; set; }
    }
}