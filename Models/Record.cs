using NHibernate.Mapping.ByCode.Conformist;

namespace WebTabanliProje.Models
{
    public class Record
    {
        public virtual int Record_Id { get; set; }
        public virtual float Amount { get; set; }
        public virtual bool Type { get; set; }
        public virtual int Category { get; set; }
        public virtual string Note { get; set; }
    }
    public class RecordMap: ClassMapping<Record>
    {
        public RecordMap()
        {
            Table("Records");
            Id(x => x.Record_Id);
            Property(x => x.Amount, x => x.NotNullable(true));
            Property(x => x.Type, x => x.NotNullable(true));
            Property(x => x.Category, x => x.NotNullable(true));
            Property(x => x.Note, x => x.NotNullable(false));
        }
    }
}