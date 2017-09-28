using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class HierarchyVehicle
    {
        // MakeModelId, , StateProvince, ModelYear, , CostPerMile, OrganizationId, Field1
        //						Supervisor Email	Supervisor PIN	Department
        public string NodeCode { get; set; }
        public Int32? VehicleId { get; set; }
        public string VinNum { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Email { get; set; }
        public int OrganizationId { get; set; }
        
    }
}
