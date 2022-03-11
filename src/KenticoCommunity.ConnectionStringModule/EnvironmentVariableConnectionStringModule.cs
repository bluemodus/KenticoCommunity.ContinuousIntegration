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

            var azureConnectionString = Environment.GetEnvironmentVariable("CMSConnectionString");
            
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(azureConnectionString))
            {
                Console.WriteLine($"CMSConnectionString environment variable is not set. Nothing to do.{Environment.NewLine}");

                return;
            }
          
            var connectionStringBuilder = new SqlConnectionStringBuilder(azureConnectionString);
            connectionStringBuilder.Password = "******";
            var displayConnectionString = connectionStringBuilder.ToString();
            Console.WriteLine($"Overriding CMSConnectionString using environment variables{Environment.NewLine}CMSConnectionString={displayConnectionString}{Environment.NewLine}");


            SettingsHelper.ConnectionStrings.SetConnectionString("CMSConnectionString", azureConnectionString);
        }
    }
}
