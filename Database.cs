using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Web;
using WebTabanliProje.Models;
using static WebTabanliProje.Models.Role;

namespace WebTabanliProje
{
    public class Database
    {
        private static ISessionFactory _sessionFactory;
        private const string SessionKey = "WebTabanliProje.Database.SessionKey";

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }
        public static void Configure()
        {
            var config = new Configuration();
            config.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<RecordMap>();
            mapper.AddMapping<CategoryMap>();
            mapper.AddMapping<UserRecordMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            _sessionFactory = config.BuildSessionFactory();
        }
        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }
        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
            {
                session.Close();
            }
            HttpContext.Current.Items.Remove(SessionKey);
        }
    }   
}