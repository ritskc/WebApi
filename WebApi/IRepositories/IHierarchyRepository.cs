using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface IHierarchyRepository
    {
        void SaveOrganizationHierarchy(int organizationId, Hierarchy hierarchy);

        void SetOrganizationHierarchyParentIdMinus(int organizationId, string nodeCode);

        void SetHieararchyParent(int organizationId);

        void UpdateOrganizationHierarchyFlat(int organizationId);

        void AssignHgiUserToOrganizationHierarchy(int organizationId);

        int GetOrganizationHierarchyByNodecode(int organizationId, string nodecode);

        void UpdateOrganizationHierarchy(int organizationId, Hierarchy hierarchy);

        List<string> GetOrganizationHierarchyBySql(string sql);

        void DeleteOrganizationHierarchySingleNode(int organizationId, string nodeCode);
    }
}
