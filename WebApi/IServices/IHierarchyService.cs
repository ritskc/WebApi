using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IServices
{
    public interface IHierarchyService
    {
        Task CreateHierarchy(HierarchyContainer hierarchyContainer, GenericSchedulesLog genericSchedulesLog);

        Task CreateOrganizationHierarchy(List<Hierarchy> hierarchies, List<HierarchyVehicle> hierarchyVehicle, GenericSchedulesLog genericSchedulesLog);
    }
}
