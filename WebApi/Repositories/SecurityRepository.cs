using Bsm.WebApi.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Bsm.WebApi.Constants;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Bsm.WebApi.Models;

namespace Bsm.WebApi.Repositories
{
    public class SecurityRepository : ISecurityRepository    {
        
        private readonly IOptions<ConnectionStrings> _connectionStrings;

        public SecurityRepository(IOptions<ConnectionStrings> connectionStrings)
        {            
            _connectionStrings = connectionStrings;
        }        

        public async Task<User> ValidateUser(string userName, string password)
        {
            User user = null;
           
            using (var conn = new SqlConnection(_connectionStrings.Value.SentinelFmMain))
            {
                conn.Open();
                using (var command = new SqlCommand("SELECT [UserId],[UserName],[Password],[PersonId],[OrganizationId],[PIN],[Description],[ExpiredDate],[FleetPulseURL],[HashPassword],[Datetime],[UserStatus],[Email]  FROM [vlfUser]  WHERE  [UserName] = '" + userName+"' AND [Password] = '"+password+"'", conn))
                {
                    command.CommandType = CommandType.Text;
                    //command.Parameters.AddRange(commandParameters.ToArray());
                    using (var r = await command.ExecuteReaderAsync())
                    {

                        while (r.Read())
                        {
                            user = new User();
                            user.UserId = Convert.ToInt32(r["UserId"]);
                            user.UserName = r["UserName"].ToString();                           
                            user.Password = r["Password"].ToString();
                            user.PersonId = (r["PersonId"] == null)  ? -1 : Convert.ToInt32(r["PersonId"]); 
                            user.OrganizationId = Convert.ToInt32(r["OrganizationId"].ToString());
                            user.PIN = r["PIN"].ToString();
                            user.Description = r["Description"].ToString();
                            //user.ExpiredDate = (r["ExpiredDate"] == null) ? : Convert.ToDateTime(r["ExpiredDate"]);
                            user.FleetPulseURL = r["FleetPulseURL"].ToString();
                            user.HashPassword = r["HashPassword"].ToString();
                            //user.Datetime = Convert.ToDateTime(r["Datetime"].ToString());
                            user.UserStatus = r["UserStatus"].ToString();
                            user.Email = r["Email"].ToString();
                        }
                    }
                }
            }
            return user;
        }
    }
}
