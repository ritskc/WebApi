using Bsm.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Bsm.WebApi.IRepositories;
using Microsoft.Extensions.Logging;

namespace Bsm.WebApi.Services
{
    public class HierarchyUserService : IHierarchyUserService
    {
        private readonly IHierarchyUserRepository _hierarchyUserRepository;
        private readonly ILogger<HierarchyUserService> _logger;
        public HierarchyUserService(IHierarchyUserRepository hierarchyUserRepository, ILogger<HierarchyUserService> logger)
        {
            _hierarchyUserRepository = hierarchyUserRepository;
            _logger = logger;
    }
        //public async Task SaveOrganizationHierarchyUserAssignment(List<HierarchyUser> hierarchyUsers, GenericSchedulesLog genericSchedulesLog)
        //{
        //    var organizationId = hierarchyUsers.FirstOrDefault().OrganizationId;

        //    //deleting existing users and assigning high level users to root
        //   // _hierarchyUserRepository.DeleteOrganizationHierarchyUserAssgignment(organizationId);

        //    _hierarchyUserRepository.AssignHgiUserToOrganizationHierarchy(organizationId);
        //    foreach (HierarchyUser hierarchyUser in hierarchyUsers)
        //    {
        //        await Task.Delay(0);
        //        _hierarchyUserRepository.SaveOrganizationHierarchyUserAssignment(organizationId, hierarchyUser);
        //    }
        //    _hierarchyUserRepository.AssignEmailToHieararchyFleet(organizationId);
        //}

        public async Task SaveOrganizationHierarchyUserAssignment(List<Hierarchy> hierarchyUsers, GenericSchedulesLog genericSchedulesLog)
        {
            var organizationId = hierarchyUsers.FirstOrDefault().OrganizationId;

            //deleting existing users and assigning high level users to root
            //_hierarchyUserRepository.DeleteOrganizationHierarchyUserAssgignment(organizationId);

            _hierarchyUserRepository.AssignHgiUserToOrganizationHierarchy(organizationId);
            foreach (Hierarchy hierarchy in hierarchyUsers)
            {
                try
                { 
                await Task.Delay(0);
                _hierarchyUserRepository.SaveOrganizationHierarchyUserAssignment(organizationId, hierarchy);               
                }

               
                catch (Exception ex)
                {
                    //_logger.LogInformation("Hierarchy Actual Exception :{0}", ex.ToString());
                   // continue;
                }
            }

            _hierarchyUserRepository.AssignEmailToHieararchyFleet(organizationId);

        }
    }
}
