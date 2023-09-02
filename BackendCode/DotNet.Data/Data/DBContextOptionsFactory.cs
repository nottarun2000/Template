using DotNet.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Data.Data
{
    public class DBContextOptionsFactory
    {
        /// <summary>
        /// Get DbContext options based on the configured database type.
        /// </summary>
        /// <returns>DbContextOptions for the selected database type.</returns>
        public static DbContextOptions<ApplicationDBContext> GetOptions()
        {
            // Create a new DbContextOptionsBuilder for ApplicationDBContext.
            DbContextOptionsBuilder<ApplicationDBContext> builder = new DbContextOptionsBuilder<ApplicationDBContext>();

            // Determine the configured database type and set up options accordingly.
            switch (Constant.DbContextType)
            {
                case DBContextType.SQLite:
                    // Configure SQLite database connection.
                    DBContextConfigurer.ConfigureSQlite(builder, Constant.ConnectionString);
                    break;

                case DBContextType.SQLServer:
                    // Configure SQL Server database connection.
                    DBContextConfigurer.ConfigureSQLServer(builder, Constant.ConnectionString);
                    break;

                default:
                    // Throw an exception for an invalid database type.
                    throw new InvalidOperationException("Database type selected is invalid");
            }

            // Return the configured DbContextOptions.
            return builder.Options;
        }
    }

    public class DBContextConfigurer
    {
        /// <summary>
        /// Configure DbContext for SQLite database.
        /// </summary>
        /// <param name="builder">DbContextOptionsBuilder instance.</param>
        /// <param name="ConnectionString">SQLite connection string.</param>
        public static void ConfigureSQlite(DbContextOptionsBuilder<ApplicationDBContext> builder, string ConnectionString)
        {
            // Configure the DbContext to use SQLite with the provided connection string.
            builder.UseSqlite(ConnectionString);
        }

        /// <summary>
        /// Configure DbContext for SQL Server database.
        /// </summary>
        /// <param name="builder">DbContextOptionsBuilder instance.</param>
        /// <param name="ConnectionString">SQL Server connection string.</param>
        public static void ConfigureSQLServer(DbContextOptionsBuilder<ApplicationDBContext> builder, string ConnectionString)
        {
            // Configure the DbContext to use SQL Server with the provided connection string.
            builder.UseSqlServer(ConnectionString);
        }
    }

}
