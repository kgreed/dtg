using DevExpress.ExpressApp.EFCore.DesignTime;
using Microsoft.EntityFrameworkCore;
namespace Dtg.Module.BusinessObjects
{
    public class DtgContextInitializer : DbContextTypesInfoInitializerBase
    {
        protected override DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>()
                .UseSqlServer(@";");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
        }
    }
}