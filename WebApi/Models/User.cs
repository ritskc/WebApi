using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class User
    {
        public Int32 UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int32? PersonId { get; set; }
        public Int32 OrganizationId { get; set; }
        public string PIN { get; set; }
        public string Description { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string FleetPulseURL { get; set; }
        public string HashPassword { get; set; }
        public DateTime? Datetime { get; set; }
        public string UserStatus { get; set; }
        public string Email { get; set; }
    }
}
