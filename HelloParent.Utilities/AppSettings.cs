using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Utilities
{
    public sealed class AppSettings
    {
        // <summary>
        /// Config Object 
        /// </summary>
        public static IConfiguration config { get; set; }

        /// <summary>
        ///  Mongo db connection string 
        /// </summary>
        public static string DbUrl { get; set; }

        /// <summary>
        /// Database for beta or prod
        /// </summary>
        public static string DbEnv { get; set; }

        /// <summary>
        /// SQL Connection string
        /// </summary>
        public static string SQLConnestionString { get; set; }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static AppSettings Instance { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="AppSettings"/> class from being created.
        /// </summary>
        private AppSettings()
        {
            FillAppSettings();
        }
        /// <summary>
        /// Fills the application settings.
        /// </summary>
        private static void FillAppSettings()
        {

            //Todo : Check for null value
            DbUrl = "mongodb://helloparent_db:L0OJKBlSxOUqXESr@beta-shard-00-00-xdc7b.azure.mongodb.net:27017,beta-shard-00-01-xdc7b.azure.mongodb.net:27017,beta-shard-00-02-xdc7b.azure.mongodb.net:27017/test?ssl=true&replicaSet=beta-shard-0&authSource=admin&retryWrites=true&w=majority";

            DbEnv = config.GetSection("AppSettings:DbEnv") != null ? config.GetSection("AppSettings:DbEnv").Value : "";

            SQLConnestionString = config.GetSection("AppSettings:SQLConnestionString") != null ? config.GetSection("AppSettings:SQLConnestionString").Value : "";
        }

        /// <summary>
        /// Initializes the specified env.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <returns>AppSettings.</returns>
        public static AppSettings Initialize(IHostingEnvironment env)
        {
            if (Instance == null)
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();
                config = builder.Build();
                Instance = new AppSettings();
            }
            return Instance;
        }
    }
}
