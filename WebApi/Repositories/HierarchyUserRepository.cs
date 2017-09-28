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
    public class HierarchyUserRepository : IHierarchyUserRepository
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;
       
        private readonly ISqlHelper _sqlHelper;
        public HierarchyUserRepository(IOptions<ConnectionStrings> connectionStrings, ISqlHelper sqlHelper)
        {

            _connectionStrings = connectionStrings;
            _sqlHelper = sqlHelper;
        }
        public  void SaveOrganizationHierarchyUserAssignment(int organizationId, HierarchyUser hierarchyUser)
        {
            SqlParameter[] parameters = new SqlParameter[6];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", hierarchyUser.OrganizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@NodeCode", hierarchyUser.NodeCode);
            parameters[1] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@UserName", hierarchyUser.UserName);
            parameters[2] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@Email", hierarchyUser.Email);
            parameters[3] = parameter3;

            SqlParameter parameter4 = new SqlParameter("@FirstName", hierarchyUser.FirstName);
            parameters[4] = parameter4;

            SqlParameter parameter5 = new SqlParameter("@LastName", hierarchyUser.LastName);
            parameters[5] = parameter5;

            _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "sp_AddUserToHieararchyFleet", CommandType.StoredProcedure, parameters);
        }

        public void SaveOrganizationHierarchyUserAssignment(int organizationId, Hierarchy hierarchy)
        {
            SqlParameter[] parameters = new SqlParameter[6];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", hierarchy.OrganizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@NodeCode", hierarchy.NodeCode);
            parameters[1] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@UserName", hierarchy.UserName);
            parameters[2] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@Email", hierarchy.Email);
            parameters[3] = parameter3;

            SqlParameter parameter4 = new SqlParameter("@FirstName", hierarchy.FirstName);
            parameters[4] = parameter4;

            SqlParameter parameter5 = new SqlParameter("@LastName", hierarchy.LastName);
            parameters[5] = parameter5;

            _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "sp_AddUserToHieararchyFleet", CommandType.StoredProcedure, parameters);
        }

        public  void AssignHgiUserToOrganizationHierarchy(int organizationId)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@organizationId", organizationId);
            parameters[0] = parameter0;

            _sqlHelper.ExecuteNonQueryAsync(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyAssignHgiUserToTopLevel", CommandType.StoredProcedure, parameters);
        }

        public void DeleteOrganizationHierarchyUserAssgignment(int organizationId)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", organizationId);
            parameters[0] = parameter0;

            _sqlHelper.ExecuteNonQueryAsync(_connectionStrings.Value.SentinelFmMain, "DeleteOrganizationHierarchyUserAssignment", CommandType.StoredProcedure, parameters);
        }

        public void AssignEmailToHieararchyFleet(int organizationId)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@organizationId", organizationId);
            parameters[0] = parameter0;

            _sqlHelper.ExecuteNonQueryAsync(_connectionStrings.Value.SentinelFmMain, "sp_AssignEmailToHieararchyFleet", CommandType.StoredProcedure, parameters);
        }
    }
}
