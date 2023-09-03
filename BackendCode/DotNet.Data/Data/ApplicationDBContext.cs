using DotNet.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Data.Data
{
    /// <summary>
    /// Represents the application-specific database context, extending DbContext.
    /// </summary>
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDBContext class with the provided DbContextOptions.
        /// </summary>
        /// <param name="options">The DbContextOptions to be used for configuration.</param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        #region DBSet

        // You may define DbSet properties here to represent your database tables.
        // For example:
        // public DbSet<User> Users { get; set; }
        // public DbSet<Order> Orders { get; set; }
        public DbSet<DBModel.Class> Orders { get; set; }


        #endregion

        /// <summary>
        /// Creates a new instance of the ApplicationDBContext connected to the database.
        /// This method is used to ensure thread safety when creating context instances.
        /// </summary>
        /// <returns>An instance of IApplicationDBContext.</returns>
        public IApplicationDBContext CreateInstance()
        {
            // Create and return a new instance of ApplicationDBContext with configured options.
            return new ApplicationDBContext(DBContextOptionsFactory.GetOptions());
        }
    }

}
