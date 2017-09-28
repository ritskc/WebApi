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
    public class HierarchyVehicleService : IHierarchyVehicleService
    {
        private readonly IHierarchyVehicleRepository _hierarchyVehicleRepository;
        private readonly ILogger<HierarchyVehicleService> _logger;
        private readonly IDbLoggerService _dbLoggerService;
        public HierarchyVehicleService(IHierarchyVehicleRepository hierarchyVehicleRepository, ILogger<HierarchyVehicleService> logger, IDbLoggerService dbLoggerService)
        {
            _hierarchyVehicleRepository = hierarchyVehicleRepository;
            _logger = logger;
            _dbLoggerService = dbLoggerService;
        }
        public async Task SaveOrganizationHierarchyVehicleAssignment(List<HierarchyVehicle> hierarchyVehicles, GenericSchedulesLog genericSchedulesLog)
        {
            try
            {
                _logger.LogInformation("Vehicle Assignment started");
                await Task.Delay(0);
                var organizationId = hierarchyVehicles.FirstOrDefault().OrganizationId;
                //DateTime? _lastfrom = _hierarchyVehicleRepository.GetLastEffectiveFrom(organizationId);

                try
                {
                    _hierarchyVehicleRepository.SetOrganizationHierarchyAssignmentExpireDateTime(organizationId, DateTime.Now);
                }

                catch (Exception ex)
                {
                    _logger.LogInformation("Vehicle Assignment abrupted due to error SetOrganizationHierarchyAssignmentExpireDateTime : {0} ", ex.ToString());
                }

                try
                {
                    _hierarchyVehicleRepository.DeleteOrganizationHierarchyFleetVehicles(organizationId);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Vehicle Assignment abrupted due to error DeleteOrganizationHierarchyFleetVehicles : {0} ", ex.ToString());
                }
                foreach (HierarchyVehicle hierarchyVehicle in hierarchyVehicles)
                {
                    //_oh.SaveOrganizationHierarchy(organizationId, row);
                    try
                    {
                        _hierarchyVehicleRepository.SaveOrganizationHierarchyAssignmentByVehicleDescription(organizationId, hierarchyVehicle);
                    }

                    catch (Exception ex)
                    {
                        genericSchedulesLog.Log = "Module : Vehicle Assignment Function : SaveOrganizationHierarchyVehicleAssignment  Error : " + ex.ToString() + " Object No : " + ex.ToString() + " Node Code: " + hierarchyVehicle.NodeCode + " vehicle description: " + hierarchyVehicle.Description;
                        _dbLoggerService.LogInfo(genericSchedulesLog);
                        //_logger.LogInformation("Vehicle Assignment abrupted due to error SaveOrganizationHierarchyAssignmentByVehicleDescription : {0} ", ex.ToString());
                    }
                }

                _logger.LogInformation("Vehicle Assignment ended");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Vehicle Assignment abrupted due to error : {0} ", ex.ToString());
            }


        }
    }
}
