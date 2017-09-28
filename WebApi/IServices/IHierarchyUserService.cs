using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IServices
{
    public interface IHierarchyUserService
    {
        //Task SaveOrganizationHierarchyUserAssignment(List<HierarchyUser> hierarchyUsers, GenericSchedulesLog genericSchedulesLog);

        Task SaveOrganizationHierarchyUserAssignment(List<Hierarchy> hierarchy, GenericSchedulesLog genericSchedulesLog);
    }
}
