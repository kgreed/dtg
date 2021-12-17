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

        //todo add unique name key
        public DbSet<Guru> Gurus { get; set; }

        //todo users should only be able to edit or delete the records they added
        public DbSet<RatingHeader> RatingHeaders { get; set; }
        public DbSet<RatingEntry> RatingEntries { get; set; }
        public DbSet<Rater> Raters{ get; set; }
        public DbSet<Metric> Metrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
        }
    }
}