using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class Hierarchy
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string NodeCode { get; set; }
        public string NodeName { get; set; }
        public bool IsParent { get; set; }
        public string ParentNodeCode { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public string Path { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
