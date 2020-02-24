using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PointsOfInterest.Logger
{
    public static class UserLogger
    {
        private static readonly ILog _log = LogManager
            .GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //public UserLogger()
        //{
        //    XmlConfigurator.Configure();
        //}

        public static void Save(string email,string username)
        {
            XmlConfigurator.Configure();
            _log.Info($"email: {email}, username: {username}");
            
        }
    }
}
