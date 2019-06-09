using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebTabanliProje.Models
{
    public class Category
    {
        public virtual int Category_Id { get; set; }
        public virtual string Category_Name { get; set; }
    }
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("Categories");
            Id(x => x.Category_Id, x => x.Generator(Generators.Identity));
            Property(x => x.Category_Name, x => x.NotNullable(true));
        }
    }
}