﻿using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IServices
{
    public interface IHierarchyVehicleService
    {
        Task SaveOrganizationHierarchyVehicleAssignment(List<HierarchyVehicle> hierarchyVehicle, GenericSchedulesLog genericSchedulesLog);
    }
}
