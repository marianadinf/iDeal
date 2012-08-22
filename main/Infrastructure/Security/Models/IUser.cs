using System.Collections.Generic;
using UIT.iDeal.Common.Interfaces;

namespace UIT.iDeal.Infrastructure.Security.Models
{
    public interface IUser
	{
		IEnumerable<UserRole> Roles { get; set; }
	}
}