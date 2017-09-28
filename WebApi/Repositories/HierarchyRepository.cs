using Bsm.WebApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Bsm.WebApi.Constants;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;

namespace Bsm.WebApi.Repositories
{
    public class HierarchyRepository : IHierarchyRepository
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;
        //public HierarchyRepository(IOptions<ConnectionStrings> connectionStrings)
        //{

        //    _connectionStrings = connectionStrings;           
        //}
        private readonly ISqlHelper _sqlHelper;
        public HierarchyRepository(IOptions<ConnectionStrings> connectionStrings, ISqlHelper sqlHelper)
        {

            _connectionStrings = connectionStrings;
            _sqlHelper = sqlHelper;
        }
        public void AssignHgiUserToOrganizationHierarchy(int organizationId)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@organizationId", organizationId);
            parameters[0] = parameter0;

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyAssignHgiUserToTopLevel", CommandType.StoredProcedure, parameters);
        }

        public void DeleteOrganizationHierarchySingleNode(int organizationId, string nodeCode)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", organizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@NodeCode", nodeCode);
            parameters[1] = parameter1;           

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyDeleteNodeCode", CommandType.StoredProcedure, parameters);
        }

        public int GetOrganizationHierarchyByNodecode(int organizationId, string nodecode)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(_connectionStrings.Value.SentinelFmMain);
            var commandText = "SELECT * FROM vlfOrganizationHierarchy WITH(NOLOCK) WHERE OrganizationId=" + organizationId.ToString() + " AND NodeCode='" + nodecode + "'";
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddRange(parameters);

                conn.Open();
               
                var dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dataReader.HasRows)
                    result= 1;
                else
                    result = 0;
                conn.Close();

                

            }
            return result;
            // var data = _sqlHelper.ExecuteReader(_connectionStrings.Value.SentinelFmMain, ,);

        }

        public List<string> GetOrganizationHierarchyBySql(string sql)
        {


            // return _sqlHelper.ExecuteReader(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);
            List<string> nodecodes = new List<string>();
            SqlConnection conn = new SqlConnection(_connectionStrings.Value.SentinelFmMain);
            var commandText = "SELECT * FROM vlfOrganizationHierarchy WITH(NOLOCK) WHERE " + sql;
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddRange(parameters);

                conn.Open();

                var dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    nodecodes.Add(dataReader["NodeCode"].ToString());
                }

                conn.Close();

               
            }
            return nodecodes;
        }

        public void SaveOrganizationHierarchy(int organizationId, Hierarchy hierarchy)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[6];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", hierarchy.OrganizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@NodeCode", hierarchy.NodeCode.Length < 50 ? hierarchy.NodeCode : hierarchy.NodeCode.Substring(0, 49));
            parameters[1] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@NodeName", hierarchy.NodeName.Length < 50 ? hierarchy.NodeName : hierarchy.NodeName.Substring(0, 49));
            parameters[2] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@IsParent", hierarchy.IsParent);
            parameters[3] = parameter3;

            SqlParameter parameter4 = new SqlParameter("@ParentNodeCode", hierarchy.ParentNodeCode.Length < 50 ? hierarchy.ParentNodeCode : hierarchy.ParentNodeCode.Substring(0, 49));
            parameters[4] = parameter4;

            SqlParameter parameter5 = new SqlParameter("@Description", hierarchy.Description.Length < 50 ? hierarchy.Description : hierarchy.Description.Substring(0, 49));
            parameters[5] = parameter5;

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyAdd", CommandType.StoredProcedure, parameters);
        }

        public void SetHieararchyParent(int organizationId)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@OrgId", organizationId);
            parameters[0] = parameter0;

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "HierarchyParentSetup", CommandType.StoredProcedure, parameters);
        }

        public void SetOrganizationHierarchyParentIdMinus(int organizationId, string nodeCode)
        {
            //await Task.Delay(0);
            string sql = "UPDATE vlfOrganizationHierarchy SET ParentId = -1 WHERE OrganizationId=" + organizationId.ToString() + " AND NodeCode='" + nodeCode + "'";

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);
        }

        public void UpdateOrganizationHierarchy(int organizationId, Hierarchy hierarchy)
        {
            SqlParameter[] parameters = new SqlParameter[4];

            SqlParameter parameter0 = new SqlParameter("@OrganizationId", hierarchy.OrganizationId);
            parameters[0] = parameter0;

            SqlParameter parameter1 = new SqlParameter("@NodeCode", hierarchy.NodeCode.Length < 50 ? hierarchy.NodeCode : hierarchy.NodeCode.Substring(0, 49));
            parameters[1] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@ParentNodeCode", hierarchy.ParentNodeCode == hierarchy.NodeCode ? null : (hierarchy.ParentNodeCode.Length < 50 ? hierarchy.ParentNodeCode : hierarchy.ParentNodeCode.Substring(0, 49)));
            parameters[2] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@NodeName", hierarchy.NodeName.Length < 50 ? hierarchy.NodeName : hierarchy.NodeName.Substring(0, 49));
            parameters[3] = parameter3;           

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "OrganizationHierarchyUpdate", CommandType.StoredProcedure, parameters);
        }

        public void UpdateOrganizationHierarchyFlat(int organizationId)
        {
            //await Task.Delay(0);
            SqlParameter[] parameters = new SqlParameter[1];

            SqlParameter parameter0 = new SqlParameter("@OrgId", organizationId);
            parameters[0] = parameter0;

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, "UpdateOrganizationHierarchyFlat", CommandType.StoredProcedure, parameters);
        }
    }
}
