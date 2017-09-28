using Bsm.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Bsm.WebApi.IRepositories;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;

namespace Bsm.WebApi.Services
{
    public class HierarchyService : IHierarchyService
    {
        private readonly IHierarchyRepository _hierarchyRepository;
        private readonly ILogger<HierarchyService> _logger;
        private readonly IHierarchyUserService _hierarchyUserService;
        private readonly IHierarchyVehicleService _hierarchyVehicleService;
        private readonly IScheduleService _scheduleService;
        private readonly IDbLoggerService _dbLoggerService;


        public HierarchyService(ILogger<HierarchyService> logger, IHierarchyRepository hierarchyRepository, IHierarchyUserService hierarchyUserService, IHierarchyVehicleService hierarchyVehicleService, IDbLoggerService dbLoggerService, IScheduleService scheduleService)
        {
            _hierarchyRepository = hierarchyRepository;
            _hierarchyUserService = hierarchyUserService;
            _hierarchyVehicleService = hierarchyVehicleService;
            _dbLoggerService = dbLoggerService;
            _logger = logger;
            _scheduleService = scheduleService;
        }
        public async Task CreateHierarchy(HierarchyContainer hierarchyContainer, GenericSchedulesLog genericSchedulesLog)
        {         

            try
            {
                _scheduleService.UpdateScheduleTask(hierarchyContainer.schedule.Id);
                switch (hierarchyContainer.schedule.ServiceSubTypeId)
                {
                    case 1: // Organization Hieararchy
                        await CreateOrganizationHierarchy(hierarchyContainer.hierarchies, hierarchyContainer.hierarchiesVehicles, genericSchedulesLog);
                        break;

                    case 2: //ServiceSubType.Hierarchy
                            //CreateHirarchy(schedule.Params);
                        break;
                    case 3: //ServiceSubType.HierarchyUser
                            //SaveOrganizationHierarchyUserAssignment(schedule.Params);
                        break;
                    case 4: //ServiceSubType.HierarchyVehicle
                            //Console.WriteLine("Case 4");
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            catch (Exception ex)
            {

                _logger.LogInformation("Hierarchy Exception No :{0} ",  ex.ToString());
                genericSchedulesLog.Log = "Hierarchy Exception No: " + ex.ToString();
               
                _dbLoggerService.LogInfo(genericSchedulesLog);
            }


        }

        public async Task CreateOrganizationHierarchy(List<Hierarchy> hierarchies, List<HierarchyVehicle> hierarchyVehicle, GenericSchedulesLog genericSchedulesLog)
        {
            // await CreateHierarchy(hierarchies);
            genericSchedulesLog.Log = MessageType.INFO +  ": Module : Hierarchy Process Started";
            genericSchedulesLog.LogType = 1;
            _dbLoggerService.LogInfo(genericSchedulesLog);
            genericSchedulesLog.LogType = 0;

            var organizationId = 0;       
           

            foreach (Hierarchy hierarchy in hierarchies)
            {
                try
                {
                    organizationId = hierarchy.OrganizationId;
                    if (hierarchy.NodeCode.Trim() == hierarchy.ParentNodeCode.Trim())
                    {
                        genericSchedulesLog.Log = MessageType.WARN +  " :Node : " + hierarchy.NodeCode.Trim() +" and ParentNode Code: "+ hierarchy.ParentNodeCode.Trim()+  " are same. Unable to process this record.";
                        genericSchedulesLog.LogType = 1;
                        _dbLoggerService.LogInfo(genericSchedulesLog);
                        genericSchedulesLog.LogType = 0;
                        continue;
                    }
                        
                    if (_hierarchyRepository.GetOrganizationHierarchyByNodecode(organizationId, hierarchy.NodeCode) > 0)
                    {
                        try
                        {                           

                            _hierarchyRepository.UpdateOrganizationHierarchy(organizationId, hierarchy);                           
                            
                        }
                        catch (Exception ex)
                        {

                            //_logger.LogInformation("Hierarchy Exception No :{0} Actual Exception :{1}", excno, ex.ToString());
                            genericSchedulesLog.Log = MessageType.ERR + " : Node:" + hierarchy.NodeCode.Trim() + " Parent Node:" + hierarchy.ParentNodeCode.Trim() + " update failed";
                            genericSchedulesLog.LogType = 1;
                            _dbLoggerService.LogInfo(genericSchedulesLog);
                            genericSchedulesLog.LogType = 0;
                        }
                    }
                    else
                    {
                        try
                        {  

                            _hierarchyRepository.SaveOrganizationHierarchy(hierarchy.OrganizationId, hierarchy);

                        }
                        catch (Exception ex)
                        {
                            genericSchedulesLog.Log = MessageType.ERR + " : Node:" + hierarchy.NodeCode.Trim() + " Parent Node:" + hierarchy.ParentNodeCode.Trim() + " insert failed: Error :" + ex.ToString();
                            genericSchedulesLog.LogType = 1;
                            _dbLoggerService.LogInfo(genericSchedulesLog);
                            genericSchedulesLog.LogType = 0;

                        }
                    }
                    _hierarchyRepository.SetOrganizationHierarchyParentIdMinus(hierarchy.OrganizationId, hierarchy.NodeCode);
                  
                }
                catch (Exception ex)
                {
                    //_logger.LogInformation("Hierarchy Exception No :{0} Actual Exception :{1}", excno, ex.ToString());
                    genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy  Error : " + ex.ToString()  + " Node Code: " + hierarchy.NodeCode + " Parent Node Code: " + hierarchy.ParentNodeCode;
                    _dbLoggerService.LogInfo(genericSchedulesLog);
                    
                }
            }
           
           

          

            string sql = " OrganizationId=" + organizationId.ToString() + " AND (ParentId <> -1 OR ParentId IS NULL)";
            var hierarchyToDelete = _hierarchyRepository.GetOrganizationHierarchyBySql(sql);
            genericSchedulesLog.Log = "Module : GetOrganizationHierarchyBySql ended ";
            _dbLoggerService.LogInfo(genericSchedulesLog);

            genericSchedulesLog.Log = "Module : DeleteOrganizationHierarchySingleNode started ";
            _dbLoggerService.LogInfo(genericSchedulesLog);
            foreach (string node in hierarchyToDelete)
            {
                try
                {
                    _hierarchyRepository.DeleteOrganizationHierarchySingleNode(organizationId, node);
                }
                catch (Exception ex)
                {
                    //_logger.LogInformation("Hierarchy Exception No :{0} Actual Exception :{1}", excno, ex.ToString());
                    genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy Call:DeleteOrganizationHierarchySingleNode Error : " + ex.ToString() + " node: " + node;
                    _dbLoggerService.LogInfo(genericSchedulesLog);
                }
            }
            genericSchedulesLog.Log = "Module : DeleteOrganizationHierarchySingleNode ended ";
            _dbLoggerService.LogInfo(genericSchedulesLog);

            genericSchedulesLog.Log = "Module : SetHieararchyParent started ";
            _dbLoggerService.LogInfo(genericSchedulesLog);

            try
            {
                _hierarchyRepository.SetHieararchyParent(organizationId);
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Hierarchy Exception No :{0} Actual Exception :{1}", excno, ex.ToString());
                genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy Call:SetHieararchyParent Error : " + ex.ToString();
                _dbLoggerService.LogInfo(genericSchedulesLog);
            }
            genericSchedulesLog.Log = "Module : SetHieararchyParent ended ";
            _dbLoggerService.LogInfo(genericSchedulesLog);

            genericSchedulesLog.Log = "Module : UpdateOrganizationHierarchyFlat started ";
            _dbLoggerService.LogInfo(genericSchedulesLog);
            try
            {
                _hierarchyRepository.UpdateOrganizationHierarchyFlat(organizationId);
            }
            catch (Exception ex)
            {
                genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy Call:UpdateOrganizationHierarchyFlat Error : " + ex.ToString();
                _dbLoggerService.LogInfo(genericSchedulesLog);
            }

          
            genericSchedulesLog.Log = MessageType.INFO + ": Module : Hierarchy Process Ended";
            genericSchedulesLog.LogType = 1;
            _dbLoggerService.LogInfo(genericSchedulesLog);
            genericSchedulesLog.LogType = 0;

            genericSchedulesLog.Log = MessageType.INFO + ": Module : Hierarchy User Assignment Started";
            genericSchedulesLog.LogType = 1;
            _dbLoggerService.LogInfo(genericSchedulesLog);
            genericSchedulesLog.LogType = 0;

          
            try
            {
                _hierarchyRepository.AssignHgiUserToOrganizationHierarchy(organizationId);
            }
            catch (Exception ex)
            {
                genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy Call:AssignHgiUserToOrganizationHierarchy Error : " + ex.ToString();
                _dbLoggerService.LogInfo(genericSchedulesLog);
            }
            genericSchedulesLog.Log = "Module : AssignHgiUserToOrganizationHierarchy ended ";
            _dbLoggerService.LogInfo(genericSchedulesLog);

            genericSchedulesLog.Log = "Module : SaveOrganizationHierarchyUserAssignment started ";
            _dbLoggerService.LogInfo(genericSchedulesLog);
            try
            {
                var response = _hierarchyUserService.SaveOrganizationHierarchyUserAssignment(hierarchies, genericSchedulesLog);
                genericSchedulesLog.Log = MessageType.INFO + ": Module : Hierarchy User Assignment Ended";
                genericSchedulesLog.LogType = 1;
                _dbLoggerService.LogInfo(genericSchedulesLog);
                genericSchedulesLog.LogType = 0;

                
                if (response.IsCompleted)
                {
                    genericSchedulesLog.Log = MessageType.INFO + ": Module : Hierarchy Vehicle Assignment Started";
                    genericSchedulesLog.LogType = 1;
                    _dbLoggerService.LogInfo(genericSchedulesLog);
                    genericSchedulesLog.LogType = 0;

                    foreach (HierarchyVehicle objvehicle in hierarchyVehicle)
                    {
                        if (hierarchies.Where(f => f.Email == objvehicle.Email).FirstOrDefault() != null)
                            objvehicle.NodeCode = hierarchies.Where(f => f.Email == objvehicle.Email).FirstOrDefault().NodeCode;
                    }
                    var vehicleResponse = _hierarchyVehicleService.SaveOrganizationHierarchyVehicleAssignment(hierarchyVehicle, genericSchedulesLog);

                    if (vehicleResponse.IsCompleted)
                    {
                        genericSchedulesLog.Log = MessageType.INFO + ": Module : Hierarchy Vehicle Assignment ended";
                        genericSchedulesLog.LogType = 1;
                        _dbLoggerService.LogInfo(genericSchedulesLog);
                        genericSchedulesLog.LogType = 0;

                        genericSchedulesLog.Log = MessageType.INFO + ": Hierarchy creation ended";
                        genericSchedulesLog.LogType = 1;
                        _dbLoggerService.LogInfo(genericSchedulesLog);
                        genericSchedulesLog.LogType = 0;
                       
                    }

                }
                await Task.Delay(0);
            }

            
            catch (Exception ex)
            {
                _logger.LogInformation("Hierarchy  Actual Exception :{0}", ex.ToString());
                genericSchedulesLog.Log = "Module : HierarchyService Function :CreateOrganizationHierarchy Call:SaveOrganizationHierarchyUserAssignment/SaveOrganizationHierarchyVehicleAssignment Error : " + ex.ToString();
                _dbLoggerService.LogInfo(genericSchedulesLog);
            }
        }
    }
}
