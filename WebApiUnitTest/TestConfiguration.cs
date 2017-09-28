using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsm.WebApiUnitTest
{
    public static class TestConfiguration
    {
        ////Dictionary that stores all config key value pair that is being used
        //public static Dictionary<string, string> configValues = new Dictionary<string, string>();

        ////Returns all available connection strings from the config file
        ////Maybe a better way if we can get around security issues
        //private static ConnectionStringsSection GetConnectionStrings(string fileName)
        //{
        //    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap()
        //    {
        //        ExeConfigFilename = fileName
        //    };

        //    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

        //    List<string> settings = config.Sections.Get("connectionStrings").CurrentConfiguration.ConnectionStrings.ToList();
        //    return settings;
        //}

        //public static List<string> SentinelWebConfigValue(string key)
        //{
        //    var fileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Bsm.WebApi\web.config"));
        //    return GetConnectionStrings(fileName, key);
        //}
    }
}
