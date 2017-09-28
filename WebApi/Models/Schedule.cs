using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int OrganizationID { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceSubTypeId { get; set; }
        public int SourceId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime LastRun { get; set; }
        public string Params { get; set; }
        public string Email { get; set; }
        public int Active { get; set; }
        public int Frequency { get; set; }
        public int DayToRun { get; set; }
        public string Time { get; set; }
    }
}
