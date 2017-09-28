using Bsm.WebApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Microsoft.Extensions.Options;
using Bsm.WebApi.Constants;
using System.Data.SqlClient;
using System.Data;

namespace Bsm.WebApi.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;
        private readonly ISqlHelper _sqlHelper;
        public ScheduleRepository(IOptions<ConnectionStrings> connectionStrings, ISqlHelper sqlHelper)
        {
            _connectionStrings = connectionStrings;
            _sqlHelper = sqlHelper;
        }

        public async Task<IEnumerable<Schedule>> GetAllScheduleTask()
        {
            var schedules = new List<Schedule>();

            using (var conn = new SqlConnection(_connectionStrings.Value.SentinelFmMain))
            {
                conn.Open();
                using (var command = new SqlCommand("SELECT VGS.[ID]      ,[OrganizationID]      ,[ServiceTypeId]      ,[ServiceSubTypeId]      ,[SourceId]      ,[DateFrom]      ,[DateTo]      ,[LastRun]      ,[Params]      ,[Email]      ,[Active], [Frequency] ,[DayToRun],[Time] FROM [vlfGenericSchedules]  VGS INNER JOIN vlfGenericSchedulesTime VGST ON VGST.ScheduleID = VGS.ID  WHERE[Active] = 1 and LastRun < DATEADD(dd, 0, DATEDIFF(dd, 0, GETUTCDATE()))", conn))
                {
                    command.CommandType = CommandType.Text;
                    using (var r = await command.ExecuteReaderAsync())
                    {

                        while (r.Read())
                        {
                            var schedule = new Schedule();
                            schedule.Id = Convert.ToInt32(r["ID"]);
                            schedule.OrganizationID = Convert.ToInt32(r["OrganizationID"]);
                            schedule.ServiceTypeId = Convert.ToInt32(r["ServiceTypeId"]);
                            schedule.ServiceSubTypeId = Convert.ToInt32(r["ServiceSubTypeId"]);
                            schedule.SourceId = Convert.ToInt32(r["SourceId"]);
                            schedule.DateFrom = Convert.ToDateTime(r["DateFrom"]);
                            schedule.DateTo = Convert.ToDateTime(r["DateTo"]);
                            schedule.LastRun = Convert.ToDateTime(r["LastRun"]);
                            schedule.Params = r["Params"].ToString();
                            schedule.Email = r["Email"].ToString();
                            schedule.Active = Convert.ToInt32(r["Active"]);
                            schedule.Frequency = Convert.ToInt32(r["Frequency"]);
                            schedule.DayToRun = Convert.ToInt32(r["DayToRun"]);
                            schedule.Time = r["Time"].ToString();


                            schedules.Add(schedule);
                        }
                    }
                }
            }
            return schedules;
        }

        public IEnumerable<Schedule> GetAllScheduleTasks()
        {
            string sql = "SELECT VGS.[ID]      ,[OrganizationID]      ,[ServiceTypeId]      ,[ServiceSubTypeId]      ,[SourceId]      ,[DateFrom]      ,[DateTo]      ,[LastRun]      ,[Params]      ,[Email]      ,[Active], [Frequency] ,[DayToRun],[Time] FROM [vlfGenericSchedules]  VGS INNER JOIN vlfGenericSchedulesTime VGST ON VGST.ScheduleID = VGS.ID  WHERE[Active] = 1 and LastRun < DATEADD(dd, 0, DATEDIFF(dd, 0, GETUTCDATE()))";
            
            var rows = _sqlHelper.ExecuteReader<Schedule>(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);

            return rows;
           
        }

        public void UpdateScheduleTask(int scheduleId)
        {
            //await Task.Delay(0);
            string sql = "UPDATE [vlfGenericSchedules] SET LastRun = DATEADD(dd, 0, DATEDIFF(dd, 0, GETUTCDATE())) WHERE ID = " + scheduleId.ToString();

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);
        }
    }
}
