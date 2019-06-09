using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WebTabanliProje.Models
{
    public class UserRecord
    {
        public virtual int User_Id { get; set; }
        public virtual int Record_Id { get; set; }
    }
    public class UserRecordMap : ClassMapping<UserRecord>
    {
        public UserRecordMap()
        {
            Table("users_records");
            Id(x => x.Record_Id);
            Property(x => x.User_Id , x => x.NotNullable(true));
            Property(x => x.Record_Id, x => x.NotNullable(true));
        }   
    }
}