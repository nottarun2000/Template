using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Data.Data
{
    public interface IApplicationDBContext
    {
        public DbSet<DBModel.EmployeeDetail> Employees{get;set;}
        public int SaveChanges();
        public void UpdateRange(IEnumerable<Object> entities);
        public EntityEntry Update(object entity);
        public IApplicationDBContext CreateInstance();

    }
}
