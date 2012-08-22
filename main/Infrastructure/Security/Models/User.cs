using System.Collections.Generic;
using UIT.iDeal.Common.Interfaces;

namespace UIT.iDeal.Infrastructure.Security.Models
{
    public class User : IUser
	{
		public User()
		{
			Roles = new List<UserRole>();
		}

		public IEnumerable<UserRole> Roles { get; set; }
	}
}