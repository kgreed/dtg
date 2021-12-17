using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Dtg.Module.BusinessObjects
{
    public class DtgDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DtgEFCoreDbContext> {
        public DtgEFCoreDbContext CreateDbContext(string[] args) {
            // throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
            var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>();
            optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Dtg");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
        }
    }
}