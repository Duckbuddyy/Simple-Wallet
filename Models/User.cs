using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;

namespace WebTabanliProje.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserPassword_hash { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual IList<Record> Records { get; set; }
        public virtual void SetPassword(string userPassword) { UserPassword_hash = BCrypt.Net.BCrypt.HashPassword(userPassword, 13); }
        public virtual void FakeHash(string userPassword) { BCrypt.Net.BCrypt.HashPassword("", 13); }
        public virtual bool CheckPassword(string userPassword) { return BCrypt.Net.BCrypt.Verify(userPassword, UserPassword_hash); }
        public User() { Roles = new List<Role>(); Records = new List<Record>(); }
    }
    

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.UserName, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.UserPassword_hash, x => {
                x.Column("UserPassword_hash");
                x.NotNullable(true);
            });

            Bag(x => x.Roles, x => {
                x.Table("roles_users");
                x.Key(k => k.Column("User_Id"));
            }, x => x.ManyToMany(k => k.Column("Role_Id")));
        }
    }
}