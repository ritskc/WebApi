using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.Constants
{
    public class BusinessConstants
    {
        public enum HttpRequestType
        {
            GET = 1,
            POST = 2,
            PUT = 3,
            DELETE = 4
        }

        public enum RequestName
        {
            SECURITY_TOKEN = 1, // CONNROLLER NAME FOLLOWED BY REQUEST
        }

        public enum MessageType
        {
            INFO = 1,
            WARN = 2,
            ERR = 3            
        }

        public enum ErrorType
        {
            INVALID_USER_NAME = 1,
            INVALID_MODEL_STATE = 2,
            INVALID_USERNAME_PASSWORD = 3,
            UNHANDLED_ERROR = 4
        }

        public enum InfoType
        {
            TOKEN_ISSUED = 1            
        }
    }
}
