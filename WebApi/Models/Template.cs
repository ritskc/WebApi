using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Template
    {
        public Template(int id,string name="",bool isActive = true)
        {
            this.Id = id;
            this.Name = name;
            this.IsActive = isActive;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
