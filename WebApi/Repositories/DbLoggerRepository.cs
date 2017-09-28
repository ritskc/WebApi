using Bsm.WebApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Microsoft.Extensions.Options;
using Bsm.WebApi.Constants;
using System.Data;
using System.Globalization;

namespace Bsm.WebApi.Repositories
{
    public class DbLoggerRepository : IDbLoggerRepository
    {
        private readonly IOptions<ConnectionStrings> _connectionStrings;
       
        private readonly ISqlHelper _sqlHelper;
        public DbLoggerRepository(IOptions<ConnectionStrings> connectionStrings, ISqlHelper sqlHelper)
        {

            _connectionStrings = connectionStrings;
            _sqlHelper = sqlHelper;
        }

        public void LogInfo(GenericSchedulesLog genericSchedulesLog)
        {
            //genericSchedulesLog.LogDateTime = DateTime.UtcNow;
            genericSchedulesLog.Log = genericSchedulesLog.Log.Replace("'", "");           
            genericSchedulesLog.GenericDateTime = genericSchedulesLog.GenericScheduleDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ;
            string sql = "INSERT INTO [vlfGenericSchedulesLog] ([GenericScheduleID]  ,[OrganizationID] ,[GenericScheduleDateTime] ,[LogDateTime],[LogType],[Log]) VALUES ('" + genericSchedulesLog.GenericScheduleId + "', '" + genericSchedulesLog.OrganizationId + "','" + genericSchedulesLog.GenericDateTime + "','" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "', '"+ genericSchedulesLog.LogType + "','" + genericSchedulesLog.Log + "')";

            Int32 rows = _sqlHelper.ExecuteNonQuery(_connectionStrings.Value.SentinelFmMain, sql, CommandType.Text);

        }
    }
}
