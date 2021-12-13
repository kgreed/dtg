using Microsoft.EntityFrameworkCore;
using DevExpress.ExpressApp.EFCore.DesignTime;
namespace Dtg.Module.BusinessObjects {
    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
	public class DtgContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			var optionsBuilder = new DbContextOptionsBuilder<DtgEFCoreDbContext>()
                .UseSqlServer(@";");
            return new DtgEFCoreDbContext(optionsBuilder.Options);
		}
	}
	//This factory creates DbContext for design-time services. For example, it is required for database migration.
}
