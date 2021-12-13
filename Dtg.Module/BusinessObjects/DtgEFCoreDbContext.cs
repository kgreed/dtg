using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.EntityFrameworkCore;
namespace Dtg.Module.BusinessObjects
{
    [TypesInfoInitializer(typeof(DtgContextInitializer))]
    public class DtgEFCoreDbContext : DbContext {
        public DtgEFCoreDbContext(DbContextOptions<DtgEFCoreDbContext> options) : base(options) {
        }
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
        public DbSet<PermissionPolicyRole> Roles { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }

        public DbSet<Guru> Gurus { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Dtg.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
        }
    }
}