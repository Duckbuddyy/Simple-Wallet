using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTabanliProje.Infrastructure;
using WebTabanliProje.Models;

namespace WebTabanliProje
{
    public class Auth
    {
        private const string UserKey = "WebTabanli.Auth.UserKey";

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;
                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
                    if (user == null)
                        return null;
                    HttpContext.Current.Items[UserKey] = user;
                }
                return user;
            }
        }
    }
}