using Bsm.WebApi.IRepositories;
using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IServices;

namespace WebApi.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;

        public SecurityService(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }
        public async Task<User> ValidateUser(string userName, string password)
        {
            return await _securityRepository.ValidateUser(userName, password);
        }
    }
}
