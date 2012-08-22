using System.Web;
using UIT.iDeal.Common.Interfaces;
using UIT.iDeal.Infrastructure.Security.Models;

namespace UIT.iDeal.Infrastructure.Security
{
    public static class Current
    {
        public static IUser User
		{
			get
			{
                var user = HttpContext.Current.Session["CurrentUser"] as IUser;

                if (user == null)
                {
                    user = new User();
                    HttpContext.Current.Session["CurrentUser"] = user;
                }

				return user;
			}
			set
			{
				HttpContext.Current.Session["CurrentUser"] = value;
			}
		}
	}
}