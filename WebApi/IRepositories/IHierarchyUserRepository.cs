using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface IHierarchyUserRepository
    {
        void SaveOrganizationHierarchyUserAssignment(int organizationId, HierarchyUser hierarchyUser);

        void SaveOrganizationHierarchyUserAssignment(int organizationId, Hierarchy hierarchy);

        void AssignHgiUserToOrganizationHierarchy(int organizationId);

        void DeleteOrganizationHierarchyUserAssgignment(int organizationId);

        void AssignEmailToHieararchyFleet(int organizationId);

    }
}
