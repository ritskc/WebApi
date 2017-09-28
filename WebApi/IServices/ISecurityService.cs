using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.IServices
{
    public interface ISecurityService
    {
        Task<User> ValidateUser(string userName, string password);
    }
}
