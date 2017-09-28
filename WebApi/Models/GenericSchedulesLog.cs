using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class GenericSchedulesLog
    {
        public int OrganizationId { get; set; }
        public int GenericScheduleId { get; set; }
        public DateTime GenericScheduleDateTime { get; set; }
        public string GenericDateTime { get; set; }
        public DateTime? LogDateTime { get; set; }
        public int? LogType { get; set; }
        public string Log { get; set; }
    }
}
