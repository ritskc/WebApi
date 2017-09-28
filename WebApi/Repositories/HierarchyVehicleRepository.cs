using Bsm.WebApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using System.Data.SqlClient;
using Bsm.WebApi.Constants;
using Microsoft.Extensions.Options;
using System.Data;

namespace Bsm.WebApi.Repositories
{
    public class HierarchyVehicleRepository : IHierarchyVehicleRepository
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        private readonly ISqlHelper _sqlHelper;
        public HierarchyVehicleRepository(IOptions<ConnectionStrings> connectionStrings, ISqlHelper sqlHelper)
        {

            _connectionStrings = connectionStrings;
            _sqlHelper = sqlHelper;
        }

        public void DeleteOrganizationHierarchyFleetVehicles(int organizationId)
        {
            string sql = "DELETE FROM vlfFleetVehicles WHERE FleetId IN ( SELECT FleetId FROM vlfFleet WHERE OrganizationId=" + organizationId.ToString() + " AND FleetType='oh') AND IsAuto = 1";

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);
        }

        public void SaveOrganizationHierarchyAssignmentByVehicleDescription(int organizationId, HierarchyVehicle hierarchyVehicle)
        {
            SqlParameter[] parameters = new SqlParameter[8];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", organizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@Description", hierarchyVehicle.Description);
            parameters[1] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@UserId", 0);
            parameters[2] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@LicensePlate", hierarchyVehicle.LicensePlate);
            parameters[3] = parameter3;

            SqlParameter parameter4 = new SqlParameter("@VIN", hierarchyVehicle.VinNum);
            parameters[4] = parameter4;

            SqlParameter parameter5 = new SqlParameter("@Email", hierarchyVehicle.Email);
            parameters[5] = parameter5;

            SqlParameter parameter6 = new SqlParameter("@ActualNodeCode", hierarchyVehicle.NodeCode);
            parameters[6] = parameter6;

            SqlParameter parameter7 = new SqlParameter("@EffectiveFrom", DateTime.Now);
            parameters[7] = parameter7;

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyVehicleAssignment", CommandType.StoredProcedure, parameters);
        }

        public void SetOrganizationHierarchyAssignmentExpireDateTime(int organizationId, DateTime exd)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", organizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@expdt", exd);
            parameters[1] = parameter1;

            _sqlHelper.ExecuteNonQueryAsync(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyAssignmentBatchSetExpireDateTime", CommandType.StoredProcedure, parameters);
        }
    }
}
