using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface IHierarchyVehicleRepository
    {       

        void SetOrganizationHierarchyAssignmentExpireDateTime(int organizationId, DateTime exd);

        void DeleteOrganizationHierarchyFleetVehicles(int organizationId);

        void SaveOrganizationHierarchyAssignmentByVehicleDescription(int organizationId,HierarchyVehicle hierarchyVehicle);
    }
}
