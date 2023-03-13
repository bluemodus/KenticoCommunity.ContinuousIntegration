using CMS;
using CMS.Base;
using CMS.DataEngine;
using System;
using System.Data.SqlClient;

[assembly: AssemblyDiscoverable]
[assembly: RegisterModule(typeof(KenticoCommunity.ConnectionStringModule.EnvironmentVariableConnectionStringModule))]

namespace KenticoCommunity.ConnectionStringModule
{
    public class EnvironmentVariableConnectionStringModule : Module
    {
        public EnvironmentVariableConnectionStringModule()
            : base(nameof(EnvironmentVariableConnectionStringModule))
        {
        }

        protected override void OnPreInit()
        {
            base.OnPreInit();

            var isContinuousIntegration = AppDomain.CurrentDomain.FriendlyName.Equals("ContinuousIntegration.exe", StringComparison.OrdinalIgnoreCase);

            if (!isContinuousIntegration)
            {
                return;
            }

            SetupConnectionStringFromEnvironment("SQLAZURECONNSTR_", "CMSConnectionString");
            SetupConnectionStringFromEnvironment("SQLAZURECONNSTR_", "CMSOMConnectionString");
        }

        private static void SetupConnectionStringFromEnvironment(string prefix, string connectionStringName)
        {
            var envVarName = prefix + connectionStringName;

            var azureConnectionString = Environment.GetEnvironmentVariable(envVarName);

            if (string.IsNullOrWhiteSpace(azureConnectionString))
            {
                Console.WriteLine($"{Environment.NewLine}{envVarName} environment variable is not set. Nothing to do.{Environment.NewLine}");
                return;
            }

            var displayConnectionString = new SqlConnectionStringBuilder(azureConnectionString)
            {
                Password = "******"
            }
                .ToString();

            Console.WriteLine($"{Environment.NewLine}Overriding {connectionStringName} using environment variables{Environment.NewLine}{envVarName}={displayConnectionString}{Environment.NewLine}");

            SettingsHelper.ConnectionStrings.SetConnectionString(connectionStringName, azureConnectionString);
        }
    }
}
