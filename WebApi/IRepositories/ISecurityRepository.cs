using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface ISecurityRepository
    {
        Task<User> ValidateUser(string userName, string password);       
    }
}
