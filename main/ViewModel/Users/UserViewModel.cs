using System;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.ViewModel.Users
{
    public class UserViewModel : IMapFromDomain<User>
    {
        public Guid Sid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}