using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class HierarchyContainer
    {
        public List<Hierarchy> hierarchies { get; set; }
        public List<HierarchyVehicle> hierarchiesVehicles { get; set; }
        public Schedule schedule { get; set; }
    }
}
