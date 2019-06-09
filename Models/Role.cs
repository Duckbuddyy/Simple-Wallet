using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebTabanliProje.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string RoleName{ get; set; }
        public class RoleMap : ClassMapping<Role>
        {
            public RoleMap()
            {
                Table("roles");
                Id(x => x.Id, x => x.Generator(Generators.Identity));
                Property(x => x.RoleName, x => x.NotNullable(true));
            }
        }
    }
}